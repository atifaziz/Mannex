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

            var header = headers[HttpResponseHeader.ContentType];
            
            return !string.IsNullOrEmpty(header) 
                 ? new ContentType(header) 
                 : null;
        }

        /// <summary>
        /// Same as <see cref="WebClient.DownloadString(string)"/> except it
        /// correctly use the character set indicated in the response to decode
        /// the string. Otherwise it uses <see cref="WebClient.Encoding"/>.
        /// </summary>

        public static string DownloadStringUsingResponseEncoding(this WebClient client, string address)
        {
            return DownloadStringUsingResponseEncodingImpl(client, () => client.DownloadData(address));
        }

        /// <summary>
        /// Same as <see cref="WebClient.DownloadString(System.Uri)"/> except it
        /// correctly use the character set indicated in the response to decode
        /// the string. Otherwise it uses <see cref="WebClient.Encoding"/>.
        /// </summary>

        public static string DownloadStringUsingResponseEncoding(this WebClient client, Uri address)
        {
            return DownloadStringUsingResponseEncodingImpl(client, () => client.DownloadData(address));
        }

        private static string DownloadStringUsingResponseEncodingImpl(WebClient client, Func<byte[]> downloader)
        {
            if (client == null) throw new ArgumentNullException("client");
            Debug.Assert(downloader != null);

            var data = downloader();
            var contentType = client.GetResponseContentType();

            var encoding = contentType == null || string.IsNullOrEmpty(contentType.CharSet)
                               ? client.Encoding 
                               : Encoding.GetEncoding(contentType.CharSet);

            return encoding.GetString(data);
        }
    }
}
