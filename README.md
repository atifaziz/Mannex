[![NuGet version][pkg-badge]][pkg]
[![Build status][ci-badge]][ci]

Mannex is yet another extension library for the Microsoft .NET Framework,
but with a few twists and a unique approach:

- It can be used piecemeal; take only what you need
- You can include it as an external assembly reference or just include its
  source and go
- Just leave out one file and extension methods from Mannex appear
  internal to your own library or application, leaving your public API
  pristine and under your control
- It introduces no new types

In general, extension methods are designed to avoid duplication by
embodying a common and repetitive task on an object (the this and first
parameter) through a combination of other methods on the same object and
methods on other objects passed in as parameters.

As extension methods are meant to be small and simple utilities, Mannex
takes a least intrusive and intuitive approach to adding value to your own
project and code. You can just include the C# files directly into your
project, somewhat akin to [static linking][static-linking] in C/C++. This
means you don't take a hit on yet another assembly for deployment.

While Mannex may grow in size as more and more extension methods are
added, your project does not necessarily take the bloat. Not doing web
development? Fine, just don't include files with extensions methods from
that space.

In the near future, Mannex hopes to provide a sort-of bundling tool (help
wanted) that will enable developers to pick and choose what they wish to
use. Today, you will have to choose to include or exclude files manually.
Meanwhile, you can also try to achieve a similar effect with
[ILMerge][ilmerge] and its `/internalize` switch.

Mannex is also [available as a NuGet package][pkg]!


[pkg]: http://nuget.org/packages/Mannex/
[pkg-badge]: https://badge.fury.io/nu/Mannex.svg
[ci]: https://ci.appveyor.com/project/raboof/mannex
[ci-badge]: https://ci.appveyor.com/api/projects/status/w64gkmfok9fedtxg?svg=true
[static-linking]: http://en.wikipedia.org/wiki/Static_linking
[ilmerge]: http://research.microsoft.com/en-us/people/mbarnett/ilmerge.aspx
