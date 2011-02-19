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

namespace Mannex.Web
{
    #region Imports

    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="NameValueCollection"/>.
    /// </summary>

    static partial class NameValueCollectionExtensions
    {
        /// <summary>
        /// Creates a query string from the key and value pairs found
        /// in the collection.
        /// </summary>
        /// <remarks>
        /// A question mark (?) is prepended if the resulting query string
        /// is not empty.
        /// </remarks>

        public static string ToQueryString(this NameValueCollection collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            var sb = new StringBuilder();

            var names = collection.AllKeys;
            for (var i = 0; i < names.Length; i++)
            {
                var name = names[i];
                var values = collection.GetValues(i);

                if (values == null)
                    continue;
                
                foreach (var value in values)
                {
                    sb.Append('&');

                    if (!string.IsNullOrEmpty(name))
                        sb.Append(name).Append('=');

                    sb.Append(string.IsNullOrEmpty(value) 
                              ? string.Empty 
                              : HttpUtility.UrlPathEncode(value));
                }
            }

            if (sb.Length == 0)
                return string.Empty;
            
            sb[0] = '?';
            return sb.ToString();
        }
    }
}
