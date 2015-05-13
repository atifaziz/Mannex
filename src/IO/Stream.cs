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

namespace Mannex.IO
{
    #region Imports

    using System;
    using System.Diagnostics;
    using System.IO;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Stream"/>.
    /// </summary>

    static partial class StreamExtensions
    {
        /// <summary>
        /// Copies one stream into another using a transfer buffer size of 4K.
        /// </summary>

        public static void Copy(this Stream input, Stream output)
        {
            ValidateArguments(input, output);
            Copy(input, output, 0);
        }

        /// <summary>
        /// Copies one stream into another using a caller-specified transfer 
        /// buffer size.
        /// </summary>
        
        public static void Copy(this Stream input, Stream output, int bufferSize)
        {
            ValidateArguments(input, output, bufferSize);
            Copy(input, output, bufferSize == 0 ? null : new byte[bufferSize]);
        }

        /// <summary>
        /// Copies one stream into another using a caller-specified transfer 
        /// buffer. If the buffer is null then a default one of 4K is used.
        /// </summary>

        public static void Copy(this Stream input, Stream output, byte[] buffer)
        {
            ValidateArguments(input, output);

            buffer = buffer ?? new byte[4096];
            int count;

            do
            {
                count = input.Read(buffer, 0, buffer.Length);
                output.Write(buffer, 0, count);
            }
            while (count > 0);
        }

        /// <summary>
        /// Copies content of the stream to fill the given buffer.
        /// </summary>

        public static int SaveTo(this Stream input, byte[] buffer)
        {
            ValidateArguments(input, buffer);
            return SaveTo(input, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Copies content of the stream to fill the given buffer.
        /// </summary>

        public static int SaveTo(this Stream input, byte[] buffer, int offset, int count)
        {
            ValidateArguments(input, buffer, offset, count);

            var total = 0;
            while (total < count)
            {
                var read = input.Read(buffer, offset, count - total);
                if (read == 0)
                    break;
                offset += read;
                total += read;
            }
            return total;
        }

        /// <summary>
        /// Saves the content of the input stream from its current position
        /// to the given file path using a default transfer buffer size of 
        /// 4K.
        /// </summary>

        public static void SaveToFile(this Stream input, string path)
        {
            ValidateArguments(input);
            SaveToFile(input, path, 0);
        }

        /// <summary>
        /// Saves the content of the input stream from its current position
        /// to the given file path using a caller-specified transfer 
        /// buffer size.
        /// </summary>

        public static void SaveToFile(this Stream input, string path, int bufferSize)
        {
            ValidateArguments(input);

            using (var output = File.OpenWrite(path))
                Copy(input, output, bufferSize);
        }

        /// <summary>
        /// Copies the content of the input stream from its current position
        /// to a memory-based stream.
        /// </summary>

        public static MemoryStream Memorize(this Stream input)
        {
            ValidateArguments(input);
            var output = new MemoryStream();
            Copy(input, output);
            output.Position = 0;
            return output;
        }

        /// <summary>
        /// Returns the remaining contents of the input as an array of 
        /// unsigned bytes.
        /// </summary>

        public static byte[] ToArray(this Stream input)
        {
            ValidateArguments(input);
            return input.Memorize().ToArray();
        }
        
        #region Argument Validation

        [DebuggerStepThrough]
        static void ValidateArguments(Stream input)
        {
            ValidateInputStream(input);
        }

        [DebuggerStepThrough]
        static void ValidateArguments(Stream input, Stream output)
        {
            ValidateInputStream(input);
            ValidateOutputStream(output);
        }

        [DebuggerStepThrough]
        static void ValidateArguments(Stream input, Stream output, int bufferSize)
        {
            ValidateInputStream(input);
            ValidateOutputStream(output);
            ValidateBufferSize(bufferSize);
        }

        [DebuggerStepThrough]
        static void ValidateArguments(Stream input, byte[] buffer)
        {
            ValidateInputStream(input);
            ValidateBuffer(buffer);
        }

        [DebuggerStepThrough]
        static void ValidateArguments(Stream input, byte[] buffer, int offset, int count)
        {
            ValidateInputStream(input);
            ValidateBuffer(buffer, offset, count);
        }

        [DebuggerStepThrough]
        static void ValidateInputStream(Stream input)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (!input.CanRead) throw new ArgumentException("Cannot read from input stream", "input");
        }

        [DebuggerStepThrough]
        static void ValidateOutputStream(Stream output)
        {
            if (output == null) throw new ArgumentNullException("output");
            if (!output.CanWrite) throw new ArgumentException("Cannot write to output stream", "output");
        }

        [DebuggerStepThrough]
        static void ValidateBufferSize(int bufferSize)
        {
            if (bufferSize < 0) throw new ArgumentException("Invalid buffer size.", "bufferSize");
        }

        [DebuggerStepThrough]
        static void ValidateBuffer(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
        }

        [DebuggerStepThrough]
        static void ValidateBuffer(byte[] buffer, int offset, int count)
        {
            ValidateBuffer(buffer);
            if (offset < 0) throw new ArgumentOutOfRangeException("offset", offset, null);
            if (count < 0) throw new ArgumentOutOfRangeException("count", count, null);
            if (offset + count > buffer.Length) throw new ArgumentOutOfRangeException("offset", offset, null);
        }

        #endregion    
    }
}