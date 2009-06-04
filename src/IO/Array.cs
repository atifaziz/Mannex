namespace Mannex.IO
{
    #region Imports

    using System;
    using System.IO;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Array"/> sub-types.
    /// </summary>

    static partial class ArrayExtensions
    {
        /// <summary>
        /// Creates a read-only stream on top of the supplied byte array.
        /// </summary>
        
        public static Stream OpenRead(this byte[] buffer)
        {
            return OpenStream(buffer, null, null, false);
        }

        /// <summary>
        /// Creates a read-only stream on top of a section of supplied 
        /// byte array.
        /// </summary>

        public static Stream OpenRead(this byte[] buffer, int index, int count)
        {
            return OpenStream(buffer, index, count, false);
        }

        /// <summary>
        /// Creates a read-write stream on top of the supplied byte array.
        /// </summary>

        public static Stream OpenReadWrite(this byte[] buffer)
        {
            return OpenStream(buffer, null, null, true);
        }

        /// <summary>
        /// Creates a read-write stream on top of a section of supplied 
        /// byte array.
        /// </summary>

        public static Stream OpenReadWrite(this byte[] buffer, int index, int count)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            return OpenStream(buffer, index, count, true);
        }

        private static Stream OpenStream(byte[] buffer, int? index, int? count, bool writeable)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            return new MemoryStream(buffer, index ?? 0, count ?? buffer.Length, writeable);
        }
    }
}
