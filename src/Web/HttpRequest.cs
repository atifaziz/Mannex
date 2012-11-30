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
    using System.Web;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="HttpRequest"/> and 
    /// <see cref="HttpRequestBase"/>.
    /// </summary>

    static partial class HttpRequestExtensions
    {
        /// <summary>
        /// Determines whether this request originated using Ajax.
        /// </summary>

        public static bool IsAjax(this HttpRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return IsAjax(request.Headers);
        }

        /// <summary>
        /// Determines whether this request originated using Ajax.
        /// </summary>

        public static bool IsAjax(this HttpRequestBase request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return IsAjax(request.Headers);
        }

        static bool IsAjax(NameValueCollection headers)
        {
            // See "Common non-standard request headers[1]"
            // [1] http://en.wikipedia.org/wiki/List_of_HTTP_header_fields#Common_non-standard_request_headers

            return headers != null 
                && "XMLHttpRequest".Equals(headers["X-Requested-With"], StringComparison.OrdinalIgnoreCase);
        }
    }
}
