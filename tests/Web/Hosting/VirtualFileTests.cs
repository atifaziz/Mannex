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

namespace Mannex.Tests.Web.Hosting
{
    #region Imports

    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Hosting;
    using Mannex.Web.Hosting;
    using Xunit;

    #endregion

    public class VirtualFileTests
    {
        [Fact]
        public void ReadAllTextFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => VirtualFileExtensions.ReadAllText(null));
            Assert.Equal("file", e.ParamName);
        }

        [Fact]
        public void ReadAllTextFailsWithNullEncoding()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TestVirtualFile.Empty("foo").ReadAllText(null));
            Assert.Equal("encoding", e.ParamName);
        }

        [Fact]
        public void ReadAllTextAutoDetectingEncoding()
        {
            const string text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            Assert.Equal(text, TestVirtualFile.Create("foo", text, Encoding.BigEndianUnicode).ReadAllText());
        }

        [Fact]
        public void ReadAllTextWithEncodingSpecified()
        {
            const string text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            var encoding = Encoding.BigEndianUnicode;
            Assert.Equal(text, TestVirtualFile.Create("foo", text, encoding).ReadAllText(encoding));
        }

        [Fact]
        public void ReadLinesFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => VirtualFileExtensions.ReadLines(null));
            Assert.Equal("file", e.ParamName);
        }

        [Fact]
        public void ReadLinesFailsWithNullEncoding()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TestVirtualFile.Empty("foo").ReadLines(null));
            Assert.Equal("encoding", e.ParamName);
        }

        [Fact]
        public void ReadLinesAutoDetectingEncoding()
        {
            var inputs = new[]
            {
                @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                @"Sed quis pretium enim. Nulla porttitor aliquet mi ut cursus.",
            };
            var file = TestVirtualFile.Create("foo", string.Join(Environment.NewLine, inputs), Encoding.BigEndianUnicode);
            Assert.Equal(inputs, file.ReadLines().ToArray());
        }

        [Fact]
        public void ReadLinesWithEncodingSpecified()
        {
            var inputs = new[]
            {
                @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                @"Sed quis pretium enim. Nulla porttitor aliquet mi ut cursus.",
            };
            var encoding = Encoding.BigEndianUnicode;
            var file = TestVirtualFile.Create("foo", string.Join(Environment.NewLine, inputs), Encoding.BigEndianUnicode);
            Assert.Equal(inputs, file.ReadLines(encoding).ToArray());
        }

        sealed class TestVirtualFile : VirtualFile
        {
            readonly byte[] _content;

            TestVirtualFile(string virtualPath, byte[] content) : 
                base(virtualPath)
            {
                _content = content ?? new byte[0];
            }

            public override Stream Open()
            {
                return new MemoryStream(_content);
            }
 
            public static VirtualFile Create(string virtualPath, string text, Encoding encoding)
            {
                var bytes = encoding.GetPreamble().Concat(encoding.GetBytes(text));
                return new TestVirtualFile(virtualPath, bytes.ToArray());
            }

            public static VirtualFile Empty(string virtualPath)
            {
                return new TestVirtualFile(virtualPath, null);
            }
        }
    }
}