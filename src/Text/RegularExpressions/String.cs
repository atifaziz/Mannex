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

namespace Mannex.Text.RegularExpressions
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="string"/> that help with regular 
    /// expression matching.
    /// </summary>

    static partial class StringExtensions
    {
        /// <summary>
        /// Indicates whether the string contains a match for the regular 
        /// expression pattern specified as an argument.
        /// </summary>

        public static bool IsMatch(this string str, string pattern)
        {
            return IsMatch(str, pattern, RegexOptions.None);
        }

        /// <summary>
        /// Indicates whether the string contains a match for the regular 
        /// expression pattern specified as an argument  along with 
        /// matching options.
        /// </summary>

        public static bool IsMatch(this string str, string pattern, RegexOptions options)
        {
            if (str == null) throw new ArgumentNullException("str");
            if (pattern == null) throw new ArgumentNullException("pattern");
            return Regex.IsMatch(str, pattern, options);
        }

        /// <summary>
        /// Searches string for an occurrence of the regular expression 
        /// specified as an argument.
        /// </summary>

        public static Match Match(this string str, string pattern)
        {
            return Match(str, pattern, RegexOptions.None);
        }

        /// <summary>
        /// Searches string for an occurrence of the regular expression 
        /// specified as an argument along with matching options.
        /// </summary>

        public static Match Match(this string str, string pattern, RegexOptions options)
        {
            if (str == null) throw new ArgumentNullException("str");
            if (pattern == null) throw new ArgumentNullException("pattern");
            return Regex.Match(str, pattern, options);
        }

        /// <summary>
        /// Searches the specified input string for all occurrences of the 
        /// regular expression specified as an argument.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution semantics.
        /// </remarks>

        public static IEnumerable<Match> Matches(this string str, string pattern)
        {
            return Matches(str, pattern, RegexOptions.None);
        }

        /// <summary>
        /// Searches the specified input string for all occurrences of the 
        /// regular expression specified as an argument along with matching
        /// options.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution semantics.
        /// </remarks>

        public static IEnumerable<Match> Matches(this string str, string pattern, RegexOptions options)
        {
            if (str == null) throw new ArgumentNullException("str");
            if (pattern == null) throw new ArgumentNullException("pattern");
            return MatchesImpl(str, pattern, options);
        }

        private static IEnumerable<Match> MatchesImpl(string str, string pattern, RegexOptions options)
        {
            var match = str.Match(pattern, options);
            while (match.Success)
            {
                yield return match;
                match = match.NextMatch();
            }
        }
    }
}