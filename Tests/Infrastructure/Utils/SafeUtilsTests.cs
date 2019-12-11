/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Infrastructure.Utils.Tests
{
    [TestClass]
    public class SafeUtilsTests
    {
        [TestMethod]
        public void IntTest()
        {
            Assert.IsTrue(SafeUtils.Int("a", 199) == 199);
            Assert.IsTrue(SafeUtils.Int("", 199) == 199);
            Assert.IsTrue(SafeUtils.Int(" ", 199) == 199);
            Assert.IsTrue(SafeUtils.Int(null, 121) == 121);
            Assert.IsTrue(SafeUtils.Int("199", 121) == 199);
            Assert.IsTrue(SafeUtils.Int("0199", 121) == 199);
        }

        [TestMethod]
        public void BoolTest()
        {
            Assert.IsTrue(!SafeUtils.Bool("a"));
            Assert.IsTrue(!SafeUtils.Bool(null));
            Assert.IsTrue(SafeUtils.Bool("y"));
            Assert.IsTrue(SafeUtils.Bool("Y"));
            Assert.IsTrue(SafeUtils.Bool("Yes"));
            Assert.IsTrue(!SafeUtils.Bool("n"));
            Assert.IsTrue(!SafeUtils.Bool("N"));
            Assert.IsTrue(!SafeUtils.Bool("No"));
        }

        /*
                [TestMethod]
                public void BoolTest1()
                {

                }

                [TestMethod]
                public void UShortTest()
                {

                }

                [TestMethod]
                public void ShortTest()
                {

                }

                [TestMethod]
                public void LongTest()
                {

                }

                [TestMethod]
                public void DecimalTest()
                {

                }

                [TestMethod]
                public void FloatTest()
                {

                }

                [TestMethod]
                public void DoubleTest()
                {

                }

                [TestMethod]
                public void EnumTest()
                {

                }

                [TestMethod]
                public void EnumTest1()
                {

                }

                [TestMethod]
                public void EnumTest2()
                {

                }

                [TestMethod]
                public void EnumTest3()
                {

                }

                [TestMethod]
                public void GuidTest()
                {

                }

                [TestMethod]
                public void GuidTest1()
                {

                }

                [TestMethod]
                public void DateTimeTest()
                {

                }

                [TestMethod]
                public void DateTimeTest1()
                {

                }

                [TestMethod]
                public void DateTimeTest2()
                {

                }
        */
    }
}