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

namespace Mannex
{
    #region Imports

    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Text;
    using System.Web;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Uri"/>.
    /// </summary>

    static partial class UriExtensions
    {
        /// <summary>
        /// Parses <see cref="Uri.Query"/> into a <see cref="NameValueCollection"/> 
        /// using UTF-8 encoding.
        /// </summary>

        [DebuggerStepThrough]
        public static NameValueCollection ParseQuery(this Uri uri)
        {
            return uri.ParseQuery(null);
        }

        /// <summary>
        /// Parses <see cref="Uri.Query"/> into a <see cref="NameValueCollection"/> 
        /// using the specified encoding or UTF-8 is a <c>null</c> reference is
        /// supplied for encoding.
        /// </summary>

        [DebuggerStepThrough]
        public static NameValueCollection ParseQuery(this Uri uri, Encoding encoding)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            return HttpUtility.ParseQueryString(uri.Query, encoding ?? Encoding.UTF8);
        }
    }
}