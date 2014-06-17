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
    using Mannex.Data;
    using Xunit;

    #endregion

    public class DataTableTests
    {
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
            var table = new DataTable();
            var cols = table.Columns;
            cols.AddRange(new[]
            {
                new DataColumn("Foo"), 
                new DataColumn("Bar"),
                new DataColumn("Baz"), 
            });
            var found = table.FindColumns("Baz", "foo", "???", "BAR");
            Assert.Equal(new[] { cols[2], cols[0], null, cols[1] }, found.ToArray());
        }
    }
}