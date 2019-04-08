using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using WebLinq;
using WebLinq.Sys;
using WebLinq.Text;
using static WebLinq.Modules.HttpModule;
using static WebLinq.Modules.SpawnModule;

namespace src
{
    class Program
    {
        const string nugetApiBaseAddress = "https://api.nuget.org/";
        const int packageDownloadCount = 100;

        static void Main(string[] args)
        {
            var tempDirectory = Path.GetTempPath();
            if (!Directory.Exists(tempDirectory))
            {
                Console.WriteLine($"Temp directory '{tempDirectory}' doesn't exist. That's odd. I'm going to bail on this.");
                return;
            }

            var tempPackageDirectory = Path.Combine(tempDirectory, Path.GetRandomFileName());

            Console.CancelKeyPress += (e, ea) =>
            {
                Directory.Delete(tempPackageDirectory, true);
            };

            try
            {
                try
                {
                    Directory.CreateDirectory(tempPackageDirectory);
                    Console.WriteLine($"Downloading the top {packageDownloadCount} most popular community packages (by download count) to '{tempPackageDirectory}'");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Had some trouble creating the temp directory. Going to bail. {e}");
                    return;
                }

                var mostDownloadedPackageIds =
                    GetMostDownloadedPackageIds(tempPackageDirectory, communityPackagesOnly: true);

                int signedCount = 0;
                int totalPackageCount = 0;

                foreach (var (packageId, _, signatures) in
                    mostDownloadedPackageIds.ToEnumerable())
                {
                    Console.Write(packageId.PadRight(43));

                    if (!signatures.Contains("Repository"))
                    {
                        Console.WriteLine("Something went wrong. These packages should be Repository signed by NuGet. Skipping.");
                        continue;
                    }
                    totalPackageCount++;
                    if (signatures.Contains("Author"))
                    {
                        signedCount++;
                        Console.WriteLine("SIGNED!");
                    }
                    else
                    {
                        Console.WriteLine("NOT SIGNED!");
                    }
                }
                Console.WriteLine($"{signedCount} packages were signed out of {totalPackageCount}.");
                Console.WriteLine($"That's {(float)signedCount / totalPackageCount:P1} of the {totalPackageCount} packages");
            }
            finally
            {
                try
                {
                    Directory.Delete(tempPackageDirectory, true);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"I tried to be friendly to the environment but cleaning up the directory '{tempPackageDirectory}' failed. {e}");
                }
            }
            Console.WriteLine("Done. Hit Enter to close.");
            Console.ReadLine();
        }

        static IObservable<(string Id, string Version, ISet<string> Signatures)>
            GetMostDownloadedPackageIds(string tempPackageDirectory,
                                        bool communityPackagesOnly,
                                        int top = packageDownloadCount) =>

            from e in
                Observable.Merge(maxConcurrent: 4, sources:
                    from page in
                        Http.Get(new Uri("https://www.nuget.org/stats/packages"))
                            .Html()
                            .Content()

                    let prototype = new { versions = default(string[]) }

                    from id in
                        Enumerable.Take(count: top, source:
                            from tr in
                                page.QuerySelectorAll("table[data-bind]")
                                    .Single(e => e.GetAttributeValue("data-bind") == "visible: " + (communityPackagesOnly ? "!showAllPackageDownloads()" : "showAllPackageDownloads"))
                                    .QuerySelectorAll("tr")
                                    .Skip(1)
                            where tr.QuerySelector("td") != null
                            select tr.QuerySelectorAll("td")
                                     .Select(td => td.InnerText.Trim())
                                     .ElementAt(1)
                            into id
                            group id by id.Split('.')[0] into g
                            select g.First())

                    select
                        from json in
                            Http.Get(new Uri($"{nugetApiBaseAddress}v3-flatcontainer/{id}/index.json"))
                                .Text()
                                .Content()
                        let version = JsonConvert.DeserializeAnonymousType(json, prototype)
                                                 .versions.LastOrDefault()
                        select new
                        {
                            Id = id,
                            Version = version,
                            NuPkgFileName = $"{id}.{version}.nupkg"
                        })

            let downloadPath = Path.Combine(tempPackageDirectory, e.NuPkgFileName)

            from nupkg in
                Http.Get(new Uri($"{nugetApiBaseAddress}v3-flatcontainer/{e.Id}/{e.Version}/{e.NuPkgFileName}"))
                    .Download(downloadPath)
                    .Content()

            from ms in
                from output in Spawn("nuget", ProgramArguments.Var("verify", "-Signatures", nupkg.Path)).Delimited(Environment.NewLine)
                select
                    from m in Regex.Matches(output, @"(?<=\bSignature +type *: *)(Repository|Author)\b",
                                            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)
                    select m.Value
            select (e.Id, e.Version, (ISet<string>)ms.ToHashSet(StringComparer.OrdinalIgnoreCase));
    }
}
