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
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>

    static partial class StringExtensions
    {
        static readonly char[] BadFileNameChars;
        static readonly string BadFileNameCharsPattern;
        static readonly char[] BadPathChars;
        static readonly string BadPathCharsPattern;

        static StringExtensions()
        {
            BadFileNameChars = Path.GetInvalidFileNameChars();
            BadFileNameCharsPattern = Patternize(BadFileNameChars);
            BadPathChars = Path.GetInvalidPathChars();
            BadPathCharsPattern = Patternize(BadPathChars);
        }

        static string Patternize(IEnumerable<char> chars)
        {
            Debug.Assert(chars != null);
            return "["
                 + string.Join(string.Empty, chars.Select(ch => Regex.Escape(ch.ToString())).ToArray())
                 + "]";
        }

        /// <summary>
        /// Makes the content of the string safe for use as a file name
        /// by replacing all invalid characters, those returned by
        /// <see cref="Path.GetInvalidFileNameChars"/>, with an underscore.
        /// </summary>
        /// <remarks>
        /// This method is not guaranteed to replace the complete set of
        /// characters that are invalid in file and directory names.
        /// The full set of invalid characters can vary by file system.
        /// </remarks>

        public static string ToFileNameSafe(this string str)
        {
            return ToFileNameSafe(str, null);
        }

        /// <summary>
        /// Makes the content of the string safe for use as a file name
        /// by replacing all invalid characters, those returned by
        /// <see cref="Path.GetInvalidFileNameChars"/>, with
        /// <paramref name="replacement"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <paramref name="replacement"/> string itself cannot
        /// carry any invalid file name characters. If
        /// <paramref name="replacement"/> is <c>null</c> or empty
        /// then it assumes the value of an underscore.</para>
        /// <para>
        /// This method is not guaranteed to replace the complete set of
        /// characters that are invalid in file and directory names.
        /// The full set of invalid characters can vary by file system.
        /// </para>
        /// </remarks>

        public static string ToFileNameSafe(this string str, string replacement)
        {
            return SanitizePathComponent(str,
                (replacement ?? string.Empty).MaskEmpty("_"),
                BadFileNameChars, BadFileNameCharsPattern);
        }

        /// <summary>
        /// Makes the content of the string safe for use as a file name
        /// by replacing all invalid characters, those returned by
        /// <see cref="Path.GetInvalidPathChars"/>, with an underscore.
        /// </summary>
        /// <remarks>
        /// This method is not guaranteed to replace the complete set of
        /// characters that are invalid in file and directory names.
        /// The full set of invalid characters can vary by file system.
        /// </remarks>

        public static string ToPathNameSafe(this string str)
        {
            return ToPathNameSafe(str, null);
        }

        /// <summary>
        /// Makes the content of the string safe for use as a file name
        /// by replacing all invalid characters, those returned by
        /// <see cref="Path.GetInvalidPathChars"/>, with
        /// <paramref name="replacement"/>.
        /// </summary>
        /// <remarks>
        /// The <paramref name="replacement"/> string itself cannot
        /// carry any invalid file name characters. If
        /// <paramref name="replacement"/> is <c>null</c> or empty
        /// then it assumes the value of an underscore.
        /// <para>
        /// This method is not guaranteed to replace the complete set of
        /// characters that are invalid in file and directory names.
        /// The full set of invalid characters can vary by file system.
        /// </para>
        /// </remarks>

        public static string ToPathNameSafe(this string str, string replacement)
        {
            return SanitizePathComponent(str,
                (replacement ?? string.Empty).MaskEmpty("_"),
                BadPathChars, BadPathCharsPattern);
        }

        static string SanitizePathComponent(string str, string replacement, char[] badChars, string badPattern)
        {
            Debug.Assert(replacement != null);
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (str.Length == 0) throw new ArgumentException(null, nameof(str));
            if (replacement.IndexOfAny(badChars) >= 0) throw new ArgumentException(null, nameof(replacement));
            return Regex.Replace(str, badPattern, replacement);
        }

        /// <summary>
        /// Returns a <see cref="TextReader"/> for reading string.
        /// </summary>

        public static TextReader Read(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return new StringReader(str);
        }
    }
}
