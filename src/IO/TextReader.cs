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

        public static IEnumerable<string> ReadLines(this TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            return ReadLinesImpl(reader);
        }

        private static IEnumerable<string> ReadLinesImpl(this TextReader reader)
        {
            for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return line;
        }

        // Concat derived from StackOverflow answer[1] by Rex Morgan[2].
        //
        // [1] http://stackoverflow.com/a/2925722/6682
        // [2] http://www.rexmorgan.net/

        /// <summary>
        /// Return a new <see cref="TextReader"/> that represents the
        /// concatenated content of one or more supplied 
        /// <see cref="TextReader"/> objects.
        /// </summary>
        /// <remarks>
        /// If any of the <see cref="TextReader"/> objects is <c>null</c>
        /// then it is treated as being empty; no exception is thrown.
        /// </remarks>

        public static TextReader Concat(this TextReader first, IEnumerable<TextReader> others)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (others == null) throw new ArgumentNullException("others");
            return Concat(first, others.ToArray());
        }

        /// <summary>
        /// Return a new <see cref="TextReader"/> that represents the
        /// concatenated content of one or more supplied 
        /// <see cref="TextReader"/> objects.
        /// </summary>
        /// <remarks>
        /// If any of the <see cref="TextReader"/> objects is <c>null</c>
        /// then it is treated as being empty; no exception is thrown.
        /// </remarks>

        public static TextReader Concat(this TextReader first, params TextReader[] others)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (others == null) throw new ArgumentNullException("others");
            return new ChainedTextReader(new[] { first }.Concat(others));
        }

        sealed class ChainedTextReader : TextReader
        {
            private TextReader[] _readers;

            public ChainedTextReader(IEnumerable<TextReader> readers)
            {
                if (readers == null) throw new ArgumentNullException("readers");

                _readers = readers.Select(r => r ?? Null)
                    /*sentinel */ .Concat(new TextReader[] { null })
                                  .ToArray();
            }

            private TextReader GetReader()
            {
                if (_readers == null) throw new ObjectDisposedException(null);
                return _readers[0];
            }

            public override int Peek()
            {
                var reader = GetReader();
                return reader == null ? -1 : reader.Peek();
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
