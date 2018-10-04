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

namespace Mannex.Json
{
    #region Imports

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>

    static partial class StringExtensions
    {
        /// <summary>
        /// Formats string as JSON text string.
        /// </summary>
        /// <remarks>
        /// String may be <c>null</c>, in which case <c>"null"</c> is
        /// returned.
        /// </remarks>

        [DebuggerStepThrough]
        public static string ToJsonString(this string str)
        {
            var writer = new StringWriter();
            WriteJsonStringTo(str, writer);
            return writer.ToString();
        }


        /// <summary>
        /// Formats string as JSON text string, sending output to
        /// <paramref name="writer"/>.
        /// </summary>
        /// <remarks>
        /// String may be <c>null</c>, in which case <c>"null"</c> is
        /// returned.
        /// </remarks>

        [DebuggerStepThrough]
        public static void WriteJsonStringTo(this string str, TextWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            if (str == null)
            {
                writer.Write("null");
                return;
            }

            var length = str.Length;

            writer.Write('"');

            var ch = '\0';

            for (var index = 0; index < length; index++)
            {
                var last = ch;
                ch = str[index];

                switch (ch)
                {
                    case '\\':
                    case '"':
                    {
                        writer.Write('\\');
                        writer.Write(ch);
                        break;
                    }

                    case '/':
                    {
                        if (last == '<')
                            writer.Write('\\');
                        writer.Write(ch);
                        break;
                    }

                    case '\b': writer.Write("\\b"); break;
                    case '\t': writer.Write("\\t"); break;
                    case '\n': writer.Write("\\n"); break;
                    case '\f': writer.Write("\\f"); break;
                    case '\r': writer.Write("\\r"); break;

                    default:
                    {
                        if (ch < ' ')
                        {
                            writer.Write("\\u");
                            writer.Write(((int)ch).ToString("x4", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            writer.Write(ch);
                        }

                        break;
                    }
                }
            }

            writer.Write('"');
        }
    }
}
