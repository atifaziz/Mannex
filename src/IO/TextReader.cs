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
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Collections.Generic;
    using Microsoft.VisualBasic.FileIO;

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
            if (first == null) throw new ArgumentNullException("first");
            if (others == null) throw new ArgumentNullException("others");
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
            if (headerSelector == null) throw new ArgumentNullException("headerSelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return reader.ParseXsv(delimiter, quoted, 
                                   (_, hs) => headerSelector(hs), 
                                   (_, hs, fs) => resultSelector(hs, fs));
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
            if (reader == null) throw new ArgumentNullException("reader");
            if (headerSelector == null) throw new ArgumentNullException("headerSelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return ParseXsv(() => new TextFieldParser(reader)
            {
                TextFieldType  = FieldType.Delimited,
                Delimiters = new[] { delimiter }, 
                HasFieldsEnclosedInQuotes = quoted,
                TrimWhiteSpace = false,
            }, headerSelector, resultSelector).GetEnumerator();
        }

        static IEnumerable<TResult> ParseXsv<THeader, TResult>(
            Func<TextFieldParser> opener, 
            Func<long, string[], THeader> headerSelector,
            Func<long, THeader, string[], TResult> resultSelector)
        {
            Debug.Assert(opener != null);
            Debug.Assert(headerSelector != null);
            Debug.Assert(resultSelector != null);

            using (var parser = opener())
            {
                if (parser == null)
                    throw new NullReferenceException("Unexpected null reference where an instance of TextFieldParser was expected.");
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
         * TODO InvalidOperationException "Invalid attempt to read when no data is present"

        sealed class TextDataReader : IDataReader
        {
            string[] _headers;
            IEnumerator<string[]> _cursor;

            bool IsDisposed() { return _cursor == null; }

            void UndisposedGuard()
            {
                if (IsDisposed()) 
                    throw new ObjectDisposedException(GetType().FullName);
            }

            IEnumerator<string[]> Cursor { get { UndisposedGuard(); return _cursor; } }
            string[] Headers { get { UndisposedGuard(); return _headers; } }

            public void Dispose()
            {
                if (_cursor == null) return;
                _cursor.Dispose();
                _cursor = null;
                _headers = null; // not needed but good for GC
            }

            public void Close() { Dispose(); }

            int ValidatingIndex(int i) { return ValidatingIndex(i, i); }

            // ReSharper disable UnusedParameter.Local
            T ValidatingIndex<T>(int i, T result) // ReSharper restore UnusedParameter.Local
            {
                if (i < 0 || i >= FieldCount) throw new IndexOutOfRangeException();
                return result;
            }

            public string GetName(int i) { return Headers[ValidatingIndex(i)]; }
            public string GetDataTypeName(int i) { return GetFieldType(i).Name; }
            public Type GetFieldType(int i) { return ValidatingIndex(i, typeof(string)); }
            public object GetValue(int i) { return GetString(i); }

            public int GetValues(object[] values)
            {
                if (values == null) throw new ArgumentNullException("values");
                var length = Math.Min(values.Length, FieldCount);
                for (var i = 0; i < length; i++)
                    values[i] = this[i];
                return length;
            }

            public int GetOrdinal(string name)
            {
                var ordinal = Array.FindIndex(Headers, h => h.Equals(name, StringComparison.Ordinal));
                if (ordinal < 0)
                {
                    ordinal = Array.FindIndex(Headers, h => h.Equals(name, StringComparison.OrdinalIgnoreCase));
                    if (ordinal < 0)
                        throw new IndexOutOfRangeException();
                }
                return ordinal;
            }

            #region Unsupported GetXXX methods

            InvalidCastException BadTypeRequestImpl(int i) { return ValidatingIndex(i, new InvalidCastException()); }

            public bool        GetBoolean(int i)  { throw BadTypeRequestImpl(i); }
            public byte        GetByte(int i)     { throw BadTypeRequestImpl(i); }
            public char        GetChar(int i)     { throw BadTypeRequestImpl(i); }
            public Guid        GetGuid(int i)     { throw BadTypeRequestImpl(i); }
            public short       GetInt16(int i)    { throw BadTypeRequestImpl(i); }
            public int         GetInt32(int i)    { throw BadTypeRequestImpl(i); }
            public long        GetInt64(int i)    { throw BadTypeRequestImpl(i); }
            public float       GetFloat(int i)    { throw BadTypeRequestImpl(i); }
            public double      GetDouble(int i)   { throw BadTypeRequestImpl(i); }
            public string      GetString(int i)   { throw BadTypeRequestImpl(i); }
            public decimal     GetDecimal(int i)  { throw BadTypeRequestImpl(i); }
            public DateTime    GetDateTime(int i) { throw BadTypeRequestImpl(i); }
            public IDataReader GetData(int i)     { throw BadTypeRequestImpl(i); }
            public long        GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) { throw BadTypeRequestImpl(i); }
            public long        GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) { throw BadTypeRequestImpl(i); }

            public bool IsDBNull(int i) { return ValidatingIndex(i, false); }
            public int FieldCount { get { return Headers.Length; } }
            public object this[int i] { get { return GetValue(i); } }
            public object this[string name] { get { return this[GetOrdinal(name)]; } }

            #endregion

            public DataTable GetSchemaTable()
            {
                var table = new DataTable();
            }

            public bool NextResult()
            {
                // TODO close enumerator
                return false;
            }

            public bool Read()
            {
                return Cursor.MoveNext();
            }

            public int Depth { get; private set; }
            public bool IsClosed { get; private set; }
            public int RecordsAffected { get; private set; }
        }
        */
    }
}
