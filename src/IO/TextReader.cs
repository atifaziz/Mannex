#region License, Terms and Author(s)
//
// Mannex - Extension methods for .NET
// Copyright (c) 2009 Atif Aziz. All rights reserved.
//
//  Author(s):
//
//      Atif Aziz, http://www.raboof.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Mannex.IO
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="TextReader"/>.
    /// </summary>

    static partial class TextReaderExtensions
    {
        /// <summary>
        /// Reads all lines from reader using deferred semantics.
        /// </summary>

        public static IEnumerator<string> ReadLines(this TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            return _(); IEnumerator<string> _()
            {
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    yield return line;
            }
        }

        // Concat derived from StackOverflow answer[1] by Rex Morgan[2].
        //
        // [1] http://stackoverflow.com/a/2925722/6682
        // [2] http://www.rexmorgan.net/

        /// <summary>
        /// Returns a new <see cref="TextReader"/> that represents the
        /// concatenated content of one or more supplied
        /// <see cref="TextReader"/> objects.
        /// </summary>
        /// <remarks>
        /// If any of the <see cref="TextReader"/> objects is <c>null</c>
        /// then it is treated as being empty; no exception is thrown.
        /// </remarks>

        public static TextReader Concat(this TextReader first, IEnumerable<TextReader> others)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (others == null) throw new ArgumentNullException(nameof(others));
            return Concat(first, others.ToArray());
        }

        /// <summary>
        /// Returns a new <see cref="TextReader"/> that represents the
        /// concatenated content of one or more supplied
        /// <see cref="TextReader"/> objects.
        /// </summary>
        /// <remarks>
        /// If any of the <see cref="TextReader"/> objects is <c>null</c>
        /// then it is treated as being empty; no exception is thrown.
        /// </remarks>

        public static TextReader Concat(this TextReader first, params TextReader[] others)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (others == null) throw new ArgumentNullException(nameof(others));
            return new ChainedTextReader(new[] { first }.Concat(others));
        }

        sealed class ChainedTextReader : TextReader
        {
            TextReader[] _readers;

            public ChainedTextReader(IEnumerable<TextReader> readers)
            {
                if (readers == null) throw new ArgumentNullException(nameof(readers));

                _readers = readers.Select(r => r ?? Null)
                    /*sentinel */ .Concat(new TextReader[] { null })
                                  .ToArray();
            }

            TextReader GetReader()
            {
                if (_readers == null) throw new ObjectDisposedException(null);
                return _readers[0];
            }

            public override int Peek()
            {
                var reader = GetReader();
                return reader?.Peek() ?? -1;
            }

            public override int Read()
            {
                while (true)
                {
                    var reader = GetReader();
                    if (reader == null)
                        return -1;
                    var ch = reader.Read();
                    if (ch >= 0)
                        return ch;
                    _readers.Rotate();
                }
            }

            public override int Read(char[] buffer, int index, int count)
            {
                while (true)
                {
                    var reader = GetReader();
                    if (reader == null)
                        return 0;
                    var read = reader.Read(buffer, index, count);
                    if (read > 0)
                        return read;
                    _readers.Rotate();
                }
            }

            public override void Close()
            {
                OnDisposeOrClose(r => r.Close());
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                OnDisposeOrClose(r => r.Dispose());
            }

            void OnDisposeOrClose(Action<TextReader> action)
            {
                if (_readers == null)
                    return;
                foreach (var reader in _readers.Where(reader => reader != null))
                    action(reader);
                _readers = null;
            }
        }
    }
}

#if VB

namespace Mannex.IO
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Collections.Generic;
    using TextFieldParser = Microsoft.VisualBasic.FileIO.TextFieldParser;
    using FieldType = Microsoft.VisualBasic.FileIO.FieldType;

    #endregion

    static partial class TextReaderExtensions
    {
        /// <summary>
        /// Parses delimited text like CSV (command-separated values) and
        /// returns each row of data as a sequence of items.
        /// </summary>

        public static IEnumerator<KeyValuePair<string, string>[]> ParseXsv(
            this TextReader reader, string delimiter, bool quoted)
        {
            return reader.ParseXsv(delimiter, quoted, hs => hs, (hs, vs) => Enumerable.Range(0, hs.Length).Select(i => hs[i].AsKeyTo(i < vs.Length ? vs[i] : null)).ToArray());
        }

        /// <summary>
        /// Parses delimited text like CSV (command-separated values) and
        /// returns each row of data as a sequence of items.
        /// </summary>

        public static IEnumerator<TResult> ParseXsv<TResult>(
            this TextReader reader, string delimiter, bool quoted,
            Func<string[], string[], TResult> resultSelector)
        {
            return reader.ParseXsv(delimiter, quoted, hs => hs, resultSelector);
        }

        /// <summary>
        /// Parses delimited text like CSV (command-separated values) and
        /// returns each row of data as a sequence of items.
        /// </summary>

        public static IEnumerator<TResult> ParseXsv<TResult>(
            this TextReader reader, string delimiter, bool quoted,
            Func<long, string[], string[], TResult> resultSelector)
        {
            return reader.ParseXsv(delimiter, quoted, (_, hs) => hs, resultSelector);
        }

        /// <summary>
        /// Parses delimited text like CSV (command-separated values) and
        /// returns each row of data as a sequence of items.
        /// </summary>

        public static IEnumerator<TResult> ParseXsv<THeader, TResult>(
            this TextReader reader, string delimiter, bool quoted,
            Func<string[], THeader> headerSelector,
            Func<THeader, string[], TResult> resultSelector)
        {
            if (headerSelector == null) throw new ArgumentNullException(nameof(headerSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return reader.ParseXsv(delimiter, quoted,
                                   (_, hs) => headerSelector(hs),
                                   (_, hs, vs) => resultSelector(hs, vs));
        }

        /// <summary>
        /// Parses delimited text like CSV (command-separated values) and
        /// returns each row of data as a sequence of items.
        /// </summary>

        public static IEnumerator<TResult> ParseXsv<THeader, TResult>(
            this TextReader reader, string delimiter, bool quoted,
            Func<long, string[], THeader> headerSelector,
            Func<long, THeader, string[], TResult> resultSelector)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (headerSelector == null) throw new ArgumentNullException(nameof(headerSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerator<TResult> _()
            {
                using (var parser = new TextFieldParser(reader)
                {
                    TextFieldType = FieldType.Delimited,
                    Delimiters = new[] { delimiter },
                    HasFieldsEnclosedInQuotes = quoted,
                    TrimWhiteSpace = false,
                })
                {
                    var headerInitialzed = false;
                    var header = default(THeader);
                    while (!parser.EndOfData)
                    {
                        if (!headerInitialzed)
                        {
                            header = headerSelector(parser.LineNumber, parser.ReadFields());
                            headerInitialzed = true;
                        }
                        else
                        {
                            yield return resultSelector(parser.LineNumber, header, parser.ReadFields());
                        }
                    }
                }
            }
        }
    }
}

#endif // VB
