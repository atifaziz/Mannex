﻿#region License, Terms and Author(s)
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

    static partial class EventHandlerExtensions
    {
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
            if (sender == null) throw new ArgumentNullException("sender");
            if (args == null) throw new ArgumentNullException("args");

            if (handler == null)
                return false;

            handler(sender, args);
            return true;
        }
    }
}