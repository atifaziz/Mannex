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
    using Collections.Generic;

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
            return ReadLinesImpl(reader);
        }

        static IEnumerator<string> ReadLinesImpl(this TextReader reader)
        {
            for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return line;
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

        /*
        abstract class DataReader : DbDataReader
        {
            public override int Depth { get { return 0; } }
            public override int RecordsAffected { get { return -1; } }

            public override object this[string name] { get { return GetValue(GetOrdinal(name)); } }
            public override object this[int i] { get { return GetValue(i); } }

            public override string GetDataTypeName(int i) { return GetFieldType(i).Name; }
            public override bool IsDBNull(int i) { return Convert.IsDBNull(GetValue(i)); }
            public override bool GetBoolean(int i) { return (bool) GetValue(i); }
            public override byte GetByte(int i) { return (byte) GetValue(i); }
            public override char GetChar(int i) { return (char) GetValue(i); }
            public override DateTime GetDateTime(int i) { return (DateTime) GetValue(i); }
            public override decimal GetDecimal(int i) { return (decimal) GetValue(i); }
            public override double GetDouble(int i) { return (double) GetValue(i); }
            public override float GetFloat(int i) { return (float) GetValue(i); }
            public override Guid GetGuid(int i) { return (Guid) GetValue(i); }
            public override short GetInt16(int i) { return (short) GetValue(i); }
            public override int GetInt32(int i) { return (int) GetValue(i); }
            public override long GetInt64(int i) { return (long) GetValue(i); }
            public override string GetString(int i) { return (string) GetValue(i); }

            public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
            {
                return GetBuffer(i, dataIndex, buffer, bufferIndex, length);
            }

            public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
            {
                return GetBuffer(i, dataIndex, buffer, bufferIndex, length);
            }

            long GetBuffer<T>(int i, long dataIndex, T[] buffer, int bufferIndex, int length)
            {
                var array = (T[]) GetValue(i);
                var copyLength = Math.Min(array.LongLength - dataIndex, length);
                Array.Copy(array, dataIndex, buffer, bufferIndex, copyLength);
                return copyLength;
            }

            public override int GetValues(object[] values)
            {
                if (values == null) throw new ArgumentNullException("values");
                var length = Math.Min(values.Length, FieldCount);
                for (var i = 0; i < length; i++)
                    values[i] = this[i];
                return length;
            }

            public override IEnumerator GetEnumerator()
            {
                return new DbEnumerator(this, true);
            }

            public override int GetOrdinal(string name)
            {
                var names = Enumerable.Range(0, FieldCount)
                                      .Select(ord => (ord + 1).AsKeyTo(GetName(ord)));

                // ReSharper disable PossibleMultipleEnumeration
                var ordinal = names.FirstOrDefault(e => e.Value.Equals(name, StringComparison.Ordinal)).Key - 1; // ReSharper restore PossibleMultipleEnumeration
                if (ordinal < 0)
                {
                    // ReSharper disable PossibleMultipleEnumeration
                    ordinal = names.FirstOrDefault(e => e.Value.Equals(name, StringComparison.OrdinalIgnoreCase)).Key - 1; // ReSharper restore PossibleMultipleEnumeration
                    if (ordinal < 0)
                        throw new IndexOutOfRangeException();
                }

                return ordinal;
            }
        }

        sealed class TextDataReader : DataReader
        {
            string[] _headers;
            IEnumerator<string[]> _cursor;

            public TextDataReader(string[] headers, IEnumerator<string[]> cursor)
            {
                _headers = headers;
                _cursor = new Enumerator<string[]>(cursor);
            }

            bool IsDisposed() { return _cursor == null; }

            void UndisposedGuard()
            {
                if (IsDisposed())
                    throw new ObjectDisposedException(GetType().FullName);
            }

            IEnumerator<string[]> Cursor { get { UndisposedGuard(); return _cursor; } }
            string[] Headers { get { UndisposedGuard(); return _headers; } }

            public override void Close()
            {
                if (_cursor == null)
                    return;
                _cursor.Dispose();
                _cursor = null;
                _headers = null; // not needed but good for GC
            }

            int ValidatingIndex(int i) { return ValidatingIndex(i, i); }

            // ReSharper disable UnusedParameter.Local
            T ValidatingIndex<T>(int i, T result) // ReSharper restore UnusedParameter.Local
            {
                if (i < 0 || i >= FieldCount) throw new IndexOutOfRangeException();
                return result;
            }

            public override string GetName(int i) { return Headers[ValidatingIndex(i)]; }
            public override Type GetFieldType(int i) { return ValidatingIndex(i, typeof(string)); }
            public override object GetValue(int i) { return GetString(i); }
            public override string GetString(int i) { return _cursor.Current[ValidatingIndex(i)]; }
            public override bool IsDBNull(int i) { return ValidatingIndex(i, false); }
            public override int FieldCount { get { return Headers.Length; } }

            public override bool HasRows
            {
                get { throw new NotSupportedException(); }
            }

            public override object this[int i] { get { return GetValue(i); } }
            public override object this[string name] { get { return this[GetOrdinal(name)]; } }

            public override DataTable GetSchemaTable()
            {
                var table = new DataTable();
                return table;
            }

            public override bool NextResult()
            {
                // TODO close enumerator
                return false;
            }

            public override bool Read()
            {
                return Cursor.MoveNext();
            }

            public override bool IsClosed { get { throw new NotImplementedException(); } }

            sealed class Enumerator<T> : IEnumerator<T>
            {
                bool _started;
                readonly IEnumerator<T> _inner;

                public Enumerator(IEnumerator<T> inner) { _inner = inner; }
                public void Dispose() { _inner.Dispose(); }

                public bool MoveNext()
                {
                    _started = true;
                    return _inner.MoveNext();
                }

                public void Reset()
                {
                    _inner.Reset();
                    _started = false;
                }

                object IEnumerator.Current { get { return Current; } }

                public T Current
                {
                    get
                    {
                        if (!_started) throw new InvalidOperationException(@"Invalid attempt to read when no data is present.");
                        return _inner.Current;
                    }
                }
            }
        }
        */
    }
}

#endif // VB
