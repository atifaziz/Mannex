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

namespace Mannex.Data
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="IDbCommand"/>.
    /// </summary>

    static partial class IDbCommandExtensions
    {
        /// <summary>
        /// Executes this command for each row in the given sequence of rows
        /// and returns a sequence of the number of rows affected by each
        /// execution
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution semantics. The command's
        /// parameters are modified with each execution. If a particular row
        /// has fewer argument than parameters then the remaining parameters
        /// are assigned <see cref="DBNull.Value"/>.
        /// </remarks>

        public static IEnumerable<int> ExecuteForEach<T>(this IDbCommand command, IEnumerable<IEnumerable<T>> rows)
        {
            return ExecuteForEach(command, 0, rows);
        }

        /// <summary>
        /// Executes this command for each row in the given sequence of rows
        /// and returns a sequence of the number of rows affected by each
        /// execution. An additional parameter specifies the index at which
        /// to start populating parameters of the commands with values of a
        /// row.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution semantics. The command's
        /// parameters are modified with each execution. If a particular row
        /// has fewer argument than parameters then the remaining parameters
        /// are assigned <see cref="DBNull.Value"/>.
        /// </remarks>

        public static IEnumerable<int> ExecuteForEach<T>(this IDbCommand command, int startIndex, IEnumerable<IEnumerable<T>> rows)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (rows == null) throw new ArgumentNullException(nameof(rows));

            return ExecuteForEachImpl(command, startIndex, rows);
        }

        static IEnumerable<int> ExecuteForEachImpl<T>(IDbCommand command, int startIndex, IEnumerable<IEnumerable<T>> rows)
        {
            Debug.Assert(command != null);
            Debug.Assert(rows != null);

            var parameters = command.Parameters.Cast<IDbDataParameter>().ToArray();
            foreach (var args in rows)
            {
                if (args != null)
                {
                    var i = startIndex;
                    using (var arg = args.GetEnumerator())
                        while (arg.MoveNext())
                            parameters[i++].Value = arg.Current;
                    while (i < parameters.Length)
                        parameters[i++].Value = DBNull.Value;
                    yield return command.ExecuteNonQuery();
                }
                else
                {
                    yield return -1;
                }
            }
        }
    }
}
