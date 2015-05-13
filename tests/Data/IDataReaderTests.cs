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

namespace Mannex.Tests.Data
{
    #region Imports

    using System;
    using System.Data;
    using System.Linq;
    using Mannex.Collections.Generic;
    using Mannex.Data;
    using Xunit;

    #endregion

    public class IDataReaderTests
    {
        [Fact]
        public void SelectFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        IDataReaderExtensions.Select<object>(null, delegate { throw new NotImplementedException(); }));
            Assert.Equal("reader", e.ParamName);
        }

        [Fact]
        public void SelectFailsWithNullSelector()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        new DataReaderDummy().Select<object>(null));
            Assert.Equal("selector", e.ParamName);
        }

        [Fact]
        public void Select()
        {
            var reader = new DataReaderStub<int>(12, 34, 56, 78);

            var disposed = false;
            reader.Disposed += delegate { disposed = true; };

            using (var e = reader.Select(r => (int)r[0]))
            {
                Assert.NotNull(e);
                Assert.NotNull(e);
                Assert.True(e.MoveNext());
                Assert.Equal(12, e.Current);
                Assert.True(e.MoveNext());
                Assert.Equal(34, e.Current);
                Assert.True(e.MoveNext());
                Assert.Equal(56, e.Current);
                Assert.True(e.MoveNext());
                Assert.Equal(78, e.Current);
                Assert.False(e.MoveNext());
                Assert.True(disposed);                
            }
        }

        [Fact]
        public void SelectReturnsCopiesOfRecords()
        {
            var reader = new DataReaderStub<int>(12, 34, 56, 78);
            var records = reader.Select(r => r).ToArray();
            Assert.Equal(12, records[0][0]);
            Assert.Equal(34, records[1][0]);
            Assert.Equal(56, records[2][0]);
            Assert.Equal(78, records[3][0]);
        }

        class DataReaderDummy : IDataReader
        {
            #region Implementation of IDisposable

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region Implementation of IDataRecord

            public string GetName(int i)
            {
                throw new NotImplementedException();
            }

            public string GetDataTypeName(int i)
            {
                throw new NotImplementedException();
            }

            public Type GetFieldType(int i)
            {
                throw new NotImplementedException();
            }

            public object GetValue(int i)
            {
                throw new NotImplementedException();
            }

            public int GetValues(object[] values)
            {
                throw new NotImplementedException();
            }

            public int GetOrdinal(string name)
            {
                throw new NotImplementedException();
            }

            public bool GetBoolean(int i)
            {
                throw new NotImplementedException();
            }

            public byte GetByte(int i)
            {
                throw new NotImplementedException();
            }

            public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public char GetChar(int i)
            {
                throw new NotImplementedException();
            }

            public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public Guid GetGuid(int i)
            {
                throw new NotImplementedException();
            }

            public short GetInt16(int i)
            {
                throw new NotImplementedException();
            }

            public int GetInt32(int i)
            {
                throw new NotImplementedException();
            }

            public long GetInt64(int i)
            {
                throw new NotImplementedException();
            }

            public float GetFloat(int i)
            {
                throw new NotImplementedException();
            }

            public double GetDouble(int i)
            {
                throw new NotImplementedException();
            }

            public string GetString(int i)
            {
                throw new NotImplementedException();
            }

            public decimal GetDecimal(int i)
            {
                throw new NotImplementedException();
            }

            public DateTime GetDateTime(int i)
            {
                throw new NotImplementedException();
            }

            public IDataReader GetData(int i)
            {
                throw new NotImplementedException();
            }

            public bool IsDBNull(int i)
            {
                throw new NotImplementedException();
            }

            public int FieldCount
            {
                get { throw new NotImplementedException(); }
            }

            object IDataRecord.this[int i]
            {
                get { throw new NotImplementedException(); }
            }

            object IDataRecord.this[string name]
            {
                get { throw new NotImplementedException(); }
            }

            #endregion

            #region Implementation of IDataReader

            public void Close()
            {
                throw new NotImplementedException();
            }

            public DataTable GetSchemaTable()
            {
                throw new NotImplementedException();
            }

            public bool NextResult()
            {
                throw new NotImplementedException();
            }

            public bool Read()
            {
                throw new NotImplementedException();
            }

            public int Depth
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsClosed
            {
                get { throw new NotImplementedException(); }
            }

            public int RecordsAffected
            {
                get { throw new NotImplementedException(); }
            }

            #endregion
        }

        class DataReaderStub<T> : IDataReader
        {
            int _index;
            readonly T[] _values;

            public DataReaderStub(params T[] values)
            {
                _index = -1;
                _values = values;
            }

            public event EventHandler Disposed;

            #region Implementation of IDisposable

            public void Dispose()
            {
                var handler = Disposed;
                if (handler != null)
                    handler(this, EventArgs.Empty);
            }

            #endregion

            #region Implementation of IDataRecord

            public string GetName(int i)
            {
                return string.Empty;
            }

            public string GetDataTypeName(int i)
            {
                return GetFieldType(i).GetType().FullName;
            }

            public Type GetFieldType(int i)
            {
                return typeof(T);
            }

            public object GetValue(int i)
            {
                throw new NotImplementedException();
            }

            public int GetValues(object[] values)
            {
                values[0] = ((IDataReader) this)[0];
                return 1;
            }

            public int GetOrdinal(string name)
            {
                throw new NotImplementedException();
            }

            public bool GetBoolean(int i)
            {
                throw new NotImplementedException();
            }

            public byte GetByte(int i)
            {
                throw new NotImplementedException();
            }

            public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public char GetChar(int i)
            {
                throw new NotImplementedException();
            }

            public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public Guid GetGuid(int i)
            {
                throw new NotImplementedException();
            }

            public short GetInt16(int i)
            {
                throw new NotImplementedException();
            }

            public int GetInt32(int i)
            {
                throw new NotImplementedException();
            }

            public long GetInt64(int i)
            {
                throw new NotImplementedException();
            }

            public float GetFloat(int i)
            {
                throw new NotImplementedException();
            }

            public double GetDouble(int i)
            {
                throw new NotImplementedException();
            }

            public string GetString(int i)
            {
                throw new NotImplementedException();
            }

            public decimal GetDecimal(int i)
            {
                throw new NotImplementedException();
            }

            public DateTime GetDateTime(int i)
            {
                throw new NotImplementedException();
            }

            public IDataReader GetData(int i)
            {
                throw new NotImplementedException();
            }

            public bool IsDBNull(int i)
            {
                throw new NotImplementedException();
            }

            public int FieldCount
            {
                get { return 1; }
            }

            object IDataRecord.this[int i]
            {
                get { return _values[_index]; }
            }

            object IDataRecord.this[string name]
            {
                get { throw new NotImplementedException(); }
            }

            #endregion

            #region Implementation of IDataReader

            public void Close()
            {
                Dispose();
            }

            public DataTable GetSchemaTable()
            {
                throw new NotImplementedException();
            }

            public bool NextResult()
            {
                throw new NotImplementedException();
            }

            public bool Read()
            {
                return ++_index < _values.Length;
            }

            public int Depth
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsClosed
            {
                get { throw new NotImplementedException(); }
            }

            public int RecordsAffected
            {
                get { throw new NotImplementedException(); }
            }

            #endregion
        }
        /*
                [Fact]
                public void FindReturnsDefaultWhenKeyKeyNotPresent()
                {
                    Assert.Equal(0, new Dictionary<int, int>().Find(42));
                }

                [Fact]
                public void FindReturnsSpecificDefaultWhenKeyKeyNotPresent()
                {
                    Assert.Equal(-42, new Dictionary<int, int>().Find(42, -42));
                }

                [Fact]
                public void FindReturnsValueOfPresentKey()
                {
                    var dict = new Dictionary<int, string> { { 42, "fourty two" } };
                    Assert.Equal("fourty two", dict.Find(42));
                }*/
    }
}