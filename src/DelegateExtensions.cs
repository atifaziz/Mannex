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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for <see cref="Delegate"/> and
    /// <see cref="MulticastDelegate"/>.
    /// </summary>

    static partial class DelegateExtensions
    {
        /// <summary>
        /// Sequentially invokes each delegate in the invocation list as
        /// <see cref="EventHandler{TEventArgs}"/> and ignores exceptions
        /// thrown during the invocation of any one handler (continuing
        /// with the next handler in the list).
        /// </summary>

        public static void InvokeAsEventHandlerWhileIgnoringErrors<T>(this Delegate del, object sender, T args)
            #if !NET45
            where T : EventArgs // constraint removed post-.NET 4
            #endif
        {
            if (del == null) throw new ArgumentNullException("del");
            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (EventHandler<T> handler in del.GetInvocationList())
                try { handler(sender, args); } catch { /* ignored */ }
        }
    }
}