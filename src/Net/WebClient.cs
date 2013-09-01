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

namespace Mannex.Net
{
    #region Imports

    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mime;
    using System.Text;
    using Mime;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="WebClient"/>.
    /// </summary>

    static partial class WebClientExtensions
    {
        /// <summary>
        /// Gets the values of the HTTP 
        /// <a href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.17">Content-Type</a>
        /// response header as an instance of the <see cref="ContentType"/> object.
        /// </summary>

        public static ContentType GetResponseContentType(this WebClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            var headers = client.ResponseHeaders;
            if (headers == null)
                throw new InvalidOperationException("Response headers not available.");

            return headers.Map(HttpResponseHeader.ContentType, h => new ContentType(h));
        }

        /// <summary>
        /// Same as <see cref="WebClient.DownloadString(string)"/> except it
        /// correctly use the character set indicated in the response to decode
        /// the string. Otherwise it uses <see cref="WebClient.Encoding"/>.
        /// </summary>

        public static string DownloadStringUsingResponseEncoding(this WebClient client, string address)
        {
            if (client == null) throw new ArgumentNullException("client");
            return DownloadStringUsingResponseEncodingImpl(client, client.DownloadData(address));
        }

        /// <summary>
        /// Same as <see cref="WebClient.DownloadString(System.Uri)"/> except it
        /// correctly use the character set indicated in the response to decode
        /// the string. Otherwise it uses <see cref="WebClient.Encoding"/>.
        /// </summary>

        public static string DownloadStringUsingResponseEncoding(this WebClient client, Uri address)
        {
            if (client == null) throw new ArgumentNullException("client");
            return DownloadStringUsingResponseEncodingImpl(client, client.DownloadData(address));
        }

        private static string DownloadStringUsingResponseEncodingImpl(WebClient client, byte[] data)
        {
            Debug.Assert(client != null);
            Debug.Assert(data != null);

            var contentType = client.GetResponseContentType();
            var encoding = contentType != null
                         ? contentType.EncodingFromCharSet(client.Encoding)
                         : null;

            return (encoding ?? client.Encoding).GetString(data);
        }
    }
}
