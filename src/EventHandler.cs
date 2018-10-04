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

    #endregion

    /// <summary>
    /// Extension methods for <see cref="EventHandler"/> and
    /// <see cref="EventHandler{TEventArgs}"/>.
    /// </summary>

    static partial class EventHandlerExtensions
    {
        /// <summary>
        /// Ensures that an <see cref="EventHandler{T}"/> will fire only 
        /// once given a way to add and remove subscription from the event.
        /// </summary>

        public static void Once<T>(this EventHandler<T> handler,
                                   Action<EventHandler<T>> addHandler,
                                   Action<EventHandler<T>> removeHandler)
            where T : EventArgs
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (addHandler == null) throw new ArgumentNullException(nameof(addHandler));
            if (removeHandler == null) throw new ArgumentNullException(nameof(removeHandler));

            var proxy = new EventHandler<T>[1];
            addHandler(proxy[0] = (sender, args) =>
            {
                removeHandler(proxy[0]);
                handler(sender, args);
            });
        }

        /// <summary>
        /// Ensures that an <see cref="EventHandler"/> will fire only 
        /// once given a way to add and remove subscription from the event.
        /// </summary>

        public static void Once(this EventHandler handler,
                                Action<EventHandler> addHandler,
                                Action<EventHandler> removeHandler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (addHandler == null) throw new ArgumentNullException(nameof(addHandler));
            if (removeHandler == null) throw new ArgumentNullException(nameof(removeHandler));

            var proxy = new EventHandler[1];
            addHandler(proxy[0] = (sender, args) =>
            {
                removeHandler(proxy[0]);
                handler(sender, args);
            });
        }

        /// <summary>
        /// Fires an event given its arguments.
        /// </summary>
        /// <returns>
        /// Boolean value indicating whether the event was fired or not. The
        /// only event under which the event is not fired is there are no
        /// handlers attached.
        /// </returns>

        public static bool Fire<T>(this EventHandler<T> handler, object sender, T args) 
            where T : EventArgs
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (handler == null)
                return false;

            handler(sender, args);
            return true;
        }

        /// <summary>
        /// Fires an event given its arguments.
        /// </summary>
        /// <returns>
        /// Boolean value indicating whether the event was fired or not. The
        /// only event under which the event is not fired is there are no
        /// handlers attached.
        /// </returns>

        public static bool Fire(this EventHandler handler, object sender, EventArgs args)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (handler == null)
                return false;

            handler(sender, args);
            return true;
        }
    }
}