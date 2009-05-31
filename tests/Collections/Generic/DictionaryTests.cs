namespace Mannex.Tests.Collections.Generic
{
    #region Improts

    using System;
    using System.Collections.Generic;
    using Mannex.Collections.Generic;
    using Xunit;

    #endregion

    public class DictionaryTests
    {
        [Fact]
        public void FindFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => 
                DictionaryExtensions.Find<object, object>(null, null));
        }

        [Fact]
        public void FindReturnsDefaultWhenKeyKeyNotPresent()
        {
            Assert.Equal(0, new Dictionary<int, int>().Find(42));
        }

        [Fact]
        public void FindReturnsSpecificDefaultWhenKeyKeyNotPresent()
        {
            Assert.Equal(-42, new Dictionary<int, int>().Find(42, -42));
        }

        [Fact]
        public void FindReturnsValueOfPresentKey()
        {
            var dict = new Dictionary<int, string> { { 42, "fourty two" } };
            Assert.Equal("fourty two", dict.Find(42));
        }
    }
}