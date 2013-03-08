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

namespace Mannex.Tests
{
    #region Imports

    using System;
    using System.IO;
    using System.Linq;
    using Xunit;

    #endregion

    public class EnumTests
    {
        [Fact]
        public void GetFlagsFailsWithNonEnumTypeArgument()
        {
            Assert.Throws<NotSupportedException>(() => FileAttributes.Archive.GetFlags<string>());
        }

        [Fact]
        public void GetFlagsFailsWithValueMismatchingTypeArgument()
        {
            Assert.Throws<ArgumentException>(() => FileAttributes.Archive.GetFlags<Environment.SpecialFolder>());
        }

        [Fact]
        public void GetFlagsWithSingleFlag()
        {
            var flags = FileAttributes.Archive.GetFlags<FileAttributes>().ToArray();
            Assert.Equal(1, flags.Length);
            Assert.Contains(FileAttributes.Archive, flags);
        }

        [Fact]
        public void GetFlagsWithMultipleFlags()
        {
            var flags = (FileAttributes.Archive | FileAttributes.System | FileAttributes.Hidden).GetFlags<FileAttributes>().ToArray();
            Assert.Equal(3, flags.Length);
            Assert.Contains(FileAttributes.Archive, flags);
            Assert.Contains(FileAttributes.System, flags);
            Assert.Contains(FileAttributes.Hidden, flags);
        }

        [Fact]
        public void HasEitherFlag()
        {
            const FileAttributes shd 
                = FileAttributes.System 
                | FileAttributes.Hidden
                | FileAttributes.Directory;

            // Invoked with 6 arguments to force the most general overload
            // and which internally invokes the others and provides 
            // sufficient coverage.

            Assert.True(shd.HasEitherFlag(FileAttributes.Hidden, 
                                          FileAttributes.ReadOnly, 
                                          FileAttributes.System, 
                                          FileAttributes.Encrypted, 
                                          FileAttributes.Directory,
                                          FileAttributes.Compressed));

            Assert.False(shd.HasEitherFlag(FileAttributes.Archive, 
                                          FileAttributes.ReadOnly, 
                                          FileAttributes.Offline, 
                                          FileAttributes.Encrypted, 
                                          FileAttributes.ReparsePoint,
                                          FileAttributes.Compressed));
        }
    }
}