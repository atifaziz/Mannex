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
    using System.Collections.Specialized;
    using System.IO;
    using System.Diagnostics;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="NameValueCollection"/>.
    /// </summary>

    static partial class NameValueCollectionExtensions
    {
        /// <summary>
        /// Formats collection as JSON text.
        /// </summary>

        public static string ToJsonString(this NameValueCollection collection)
        {
            return WriteJsonStringToImpl(collection, new StringWriter()).ToString();
        }

        /// <summary>
        /// Formats collection as JSON text, sending output to <paramref name="writer"/>.
        /// </summary>

        public static void WriteJsonStringTo(this NameValueCollection collection, TextWriter writer)
        {
            if (writer == null) throw new ArgumentNullException("writer");
            WriteJsonStringToImpl(collection, writer);
        }

        private static TextWriter WriteJsonStringToImpl(NameValueCollection collection, TextWriter writer)
        {
            Debug.Assert(writer != null);

            if (collection == null)
            {
                writer.Write("null");
                return writer;
            }

            writer.Write("{");

            for (var i = 0; i < collection.Count; i++)
            {
                if (i > 0) writer.Write(',');

                collection.GetKey(i).WriteJsonStringTo(writer);
                writer.Write(": ");

                var values = collection.GetValues(i);

                if (values.Length == 1)
                {
                    values[0].WriteJsonStringTo(writer);
                }
                else
                {
                    writer.Write("[ ");

                    for (var j = 0; j < values.Length; j++)
                    {
                        if (j > 0) writer.Write(", ");
                        values[j].WriteJsonStringTo(writer);
                    }

                    writer.Write(" ]");
                }
            }

            writer.Write("}");
            return writer;
        }
    }
}
