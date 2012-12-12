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
    using System.Web;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Uri"/>.
    /// </summary>

    static partial class UriExtensions
    {
        /// <summary>
        /// Randomizes the URL by adding a query string parameter
        /// named <c>__rnd</c> and whose value is a newly generated GUID.
        /// </summary>

        public static Uri Randomize(this Uri url)
        {
            return Randomize(url, null);
        }

        /// <summary>
        /// Randomizes the URL by adding a query string parameter
        /// named <c>__rnd</c> and whose random value component
        /// is the string representation of <paramref name="value"/>.
        /// </summary>

        public static Uri Randomize(this Uri url, object value)
        {
            return Randomize(url, null, value ?? Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// Randomizes the URL by adding a query string parameter
        /// named as <paramref name="key"/> and whose random value 
        /// component is the string representation of 
        /// <paramref name="value"/>.
        /// </summary>

        public static Uri Randomize(this Uri url, string key, object value)
        {
            if (url == null) throw new ArgumentNullException("url");
            var builder = new UriBuilder(url);
            var qs = HttpUtility.ParseQueryString(builder.Query);
            qs[string.IsNullOrEmpty(key) ? "__rnd" : key] = value.ToString();
            builder.Query = qs.ToString();
            return builder.Uri;
        }
    }
}
