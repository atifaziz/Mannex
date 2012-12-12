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
    using System.Net;
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

        /// <summary>
        /// Determines if <see cref="Uri.Scheme"/> is HTTP or HTTPS.
        /// </summary>

        [DebuggerStepThrough]
        public static bool IsHttpOrHttps(this Uri uri)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            return uri.Scheme == Uri.UriSchemeHttp
                || uri.Scheme == Uri.UriSchemeHttps;
        }

        /// <summary>
        /// Splits the URI and its <see cref="Uri.UserInfo"/> (using the
        /// <c>USER ":" PASSWORD</c> syntax) and returns a user-defined 
        /// aggregate of the two where the resulting URI has the 
        /// <see cref="Uri.UserInfo"/> portion removed.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// URI does not at least identify the user.
        /// </exception>

        public static T SplitUserNamePassword<T>(this Uri url, 
            Func<Uri, NetworkCredential, T> resultFunc)
        {
            if (url == null) throw new ArgumentNullException("url");
            if (resultFunc == null) throw new ArgumentNullException("resultFunc");
            return resultFunc(RemoveUserNamePassword(url), GetUserNamePassword(url));
        }

        /// <summary>
        /// Attempts to split the URI and its <see cref="Uri.UserInfo"/>
        /// (using the <c>USER ":" PASSWORD</c> syntax) and returns a 
        /// user-defined aggregate of the two where the resulting URI has 
        /// the <see cref="Uri.UserInfo"/> portion removed.
        /// </summary>

        public static T TrySplitUserNamePassword<T>(this Uri url, 
            Func<Uri, NetworkCredential, T> resultFunc)
        {
            if (url == null) throw new ArgumentNullException("url");
            if (resultFunc == null) throw new ArgumentNullException("resultFunc");
            return resultFunc(RemoveUserNamePassword(url), TryGetUserNamePassword(url));
        }

        /// <summary>
        /// Creates a new <see cref="Uri"/> from the this URI that has the 
        /// <see cref="Uri.UserInfo"/> (using the <c>USER ":" PASSWORD</c> 
        /// syntax) portion removed, whether initially present or not.
        /// </summary>
        
        public static Uri RemoveUserNamePassword(this Uri url)
        {
            if (url == null) throw new ArgumentNullException("url");

            return url.UserInfo.Length == 0 ? url 
                 : new UriBuilder(url) { UserName = null, Password = null }.Uri;
        }

        /// <summary>
        /// Extract the <see cref="Uri.UserInfo"/> portion (using the 
        /// <c>USER ":" PASSWORD</c> syntax) of the URI and return it as 
        /// a <see cref="NetworkCredential"/> object.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// URI does not at least identify the user.
        /// </exception>

        public static NetworkCredential GetUserNamePassword(this Uri url)
        {
            var credentials = TryGetUserNamePassword(url);
            if (credentials == null)
                throw new ArgumentException(string.Format("{0} is missing user credentials.", url));
            return credentials;
        }

        /// <summary>
        /// Attempts to extract the <see cref="Uri.UserInfo"/> portion 
        /// (using the <c>USER ":" PASSWORD</c> syntax) of the URI and 
        /// return it as a <see cref="NetworkCredential"/> object.
        /// </summary>
        /// <returns>
        /// <see cref="NetworkCredential"/> representing <see cref="Uri.UserInfo"/>
        /// or <c>null</c> when <see cref="Uri.UserInfo"/> is missing 
        /// the user name.
        /// </returns>

        public static NetworkCredential TryGetUserNamePassword(this Uri url)
        {
            if (url == null) throw new ArgumentNullException("url");
            
            return url.UserInfo.Split(':', (uid, pwd) 
                => uid.Length != 0 
                 ? new NetworkCredential(Uri.UnescapeDataString(uid), Uri.UnescapeDataString(pwd)) 
                 : null);
        }
    }
}