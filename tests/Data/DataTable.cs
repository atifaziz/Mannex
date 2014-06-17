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
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.UI.WebControls;
    using Mannex.Data;
    using Xunit;

    #endregion

    public class DataTableTests
    {
        static DataTable CreateSampleDataTable()
        {
            var table = new DataTable();
            table.Columns.AddRange(new[]
            {
                new DataColumn("Foo"),
                new DataColumn("Bar"),
                new DataColumn("Baz"),
            });
            return table;
        }

        [Fact]
        public void FindColumnsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        DataTableExtensions.FindColumns(null, new string[0]));
            Assert.Equal("table", e.ParamName);
        }

        [Fact]
        public void FindColumnsWithNoNames()
        {
            var table = new DataTable();
            Assert.Equal(new DataColumn[0], table.FindColumns().ToArray());
        }

        [Fact]
        public void FindColumnsWithNames()
        {
            var table = CreateSampleDataTable();
            var found = table.FindColumns("Baz", "foo", "???", "BAR");
            var cols = table.Columns;
            Assert.Equal(new[] { cols[2], cols[0], null, cols[1] }, found.ToArray());
        }

        [Fact]
        public void SetColumnsOrderFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        DataTableExtensions.SetColumnsOrder(null, new DataColumn[0]));
            Assert.Equal("table", e.ParamName);
        }

        [Fact]
        public void SetColumnsOrderWithNullArray()
        {
            CreateSampleDataTable().SetColumnsOrder((DataColumn[]) null);
        }

        [Fact]
        public void SetColumnsOrderWithNullSequence()
        {
            CreateSampleDataTable().SetColumnsOrder((IEnumerable<DataColumn>)null);
        }

        [Fact]
        public void SetColumnsOrder()
        {
            var table = CreateSampleDataTable();
            var cols = table.Columns;
            DataColumn foo = cols[0], bar = cols[1], baz = cols[2]; 
            table.SetColumnsOrder(baz, bar);
            Assert.Equal(new[] { baz, bar, foo }, table.Columns.Cast<DataColumn>().ToArray());
        }

        [Fact]
        public void SetColumnsOrderFailsWithUnrelatedColumn()
        {
            var e = Assert.Throws<ArgumentException>(() =>
                        CreateSampleDataTable().SetColumnsOrder(new DataColumn()));
            Assert.Equal("columns", e.ParamName);
        }

        [Fact]
        public void SetColumnsOrderFailsWithNullColumnElement()
        {
            var table = CreateSampleDataTable();
            var cols = table.Columns;
            var e = Assert.Throws<ArgumentException>(() =>
                        table.SetColumnsOrder(cols[2], null, cols[0]));
            Assert.Equal("columns", e.ParamName);
        }
    }
}