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
    using System.Text;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="FileInfo"/>.
    /// </summary>

    static partial class FileInfoExtensions
    {
        /// <summary>
        /// Reads all lines from the file using deferred semantics.
        /// The encoding is selected using byte order mark detection.
        /// </summary>

        public static IEnumerable<string> ReadLines(this FileInfo info)
        {
            return ReadLines(info, null);
        }

        /// <summary>
        /// Reads all lines from the file using deferred semantics.
        /// </summary>

        public static IEnumerable<string> ReadLines(this FileInfo info, Encoding encoding)
        {
            if (info == null) throw new ArgumentNullException("info");
            return ReadLinesImpl(info.FullName, encoding);
        }

        static IEnumerable<string> ReadLinesImpl(string path, Encoding encoding)
        {
            var reader = encoding == null 
                       ? new StreamReader(path, true) 
                       : new StreamReader(path, encoding);

            using (reader)
            using (var line = reader.ReadLines())
            {
                while (line.MoveNext())
                    yield return line.Current;
            }
        }
    }
}