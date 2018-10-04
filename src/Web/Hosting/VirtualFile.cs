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

#if ASPNET

namespace Mannex.Web.Hosting
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web.Hosting;
    using IO;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="VirtualFile"/>.
    /// </summary>

    static partial class VirtualFileExtensions
    {
        /// <summary>
        /// Opens the text file, reads and returns all its content as a
        /// single string and then closes the file.
        /// </summary>

        public static string ReadAllText(this VirtualFile file)
        {
            return ReadAllTextImpl(file, null);
        }

        /// <summary>
        /// Opens the text file, reads and returns all its content as a
        /// single string and then closes the file. An additional parameter
        /// specified the encoding to convert file bytes to text.
        /// </summary>

        public static string ReadAllText(this VirtualFile file, Encoding encoding)
        {
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));
            return ReadAllTextImpl(file, encoding);
        }

        static string ReadAllTextImpl(this VirtualFile file, Encoding encoding)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            using (var stream = file.Open())
            using (var reader = encoding == null
                              ? new StreamReader(stream)
                              : new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Reads the lines of the file.
        /// </summary>
        /// <remarks>
        /// This method uses deferred semantics.
        /// </remarks>

        public static IEnumerable<string> ReadLines(this VirtualFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            return file.ReadLines(null);
        }

        /// <summary>
        /// Reads the lines of the file with a specified encoding.
        /// </summary>
        /// <remarks>
        /// This method uses deferred semantics.
        /// </remarks>

        public static IEnumerable<string> ReadLines(this VirtualFile file, Encoding encoding)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            return _(); IEnumerable<string> _()
            {
                using (var stream = file.Open())
                using (var reader = encoding == null
                                  ? new StreamReader(stream)
                                  : new StreamReader(stream, encoding))
                using (var e = reader.ReadLines())
                while (e.MoveNext())
                    yield return e.Current;
            }
        }
    }
}

#endif // ASPNET
