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
    using System;
    using System.IO;

    /// <summary>
    /// Extension methods for <see cref="FileSystemInfo"/>.
    /// </summary>

    static partial class FileSystemInfoExtensions
    {
        /// <summary>
        /// Determines if the file system entry should be generally hidden
        /// (has <see cref="FileAttributes.Hidden"/> or
        /// <see cref="FileAttributes.System"/> set) in a user interface
        /// based on its attributes.
        /// </summary>
        /// <remarks>
        /// On Unix systems, if <see cref="FileSystemInfo.Name"/> starts with
        /// a period or dot (.) then it is considered user-invisible.
        /// </remarks>

        public static bool IsUserVisible(this FileSystemInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");
            return 0 == (info.Attributes & (FileAttributes.Hidden | FileAttributes.System))
                && (!Environment.OSVersion.IsUnix() || info.Name.TryCharAt(0) != '.');
        }
    }
}