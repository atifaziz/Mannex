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
    using System.Net;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="WebException"/>.
    /// </summary>

    static partial class WebExceptionExtensions
    {
        /// <summary>
        /// Returns the <see cref="WebResponse"/> associated with the 
        /// <see cref="WebException"/> when its 
        /// (<see cref="WebException.Status"/> property) reads 
        /// <see cref="WebExceptionStatus.ProtocolError"/>. Otherwise it 
        /// returns a null reference.
        /// </summary>

        public static WebResponse TryGetWebResponse(this WebException exception) 
        {
            return exception.TryGetWebResponse<WebResponse>();
        }

        /// <summary>
        /// Returns the web response <typeparamref name="T"/> associated 
        /// with the <see cref="WebException"/> when its status 
        /// (<see cref="WebException.Status"/> property) reads 
        /// <see cref="WebExceptionStatus.ProtocolError"/> and the 
        /// associated response is an instance of <typeparamref name="T"/>. 
        /// Otherwise it returns a null reference.
        /// </summary>

        public static T TryGetWebResponse<T>(this WebException exception) 
            where T : WebResponse
        {
            if (exception == null) throw new ArgumentNullException("exception");
            return exception.Status == WebExceptionStatus.ProtocolError 
                 ? exception.Response as T
                 : null;
        }
    }
}
