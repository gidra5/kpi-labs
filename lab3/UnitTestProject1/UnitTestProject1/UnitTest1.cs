using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.BinaryFlag;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Route_1_2_Lower_Bound()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
              () => { new MultipleBinaryFlag(1); });
        }

        [TestMethod]
        public void Test_Route_1_2_Upper_Bound()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
              () => { new MultipleBinaryFlag(17179868705); });
        }

        [TestMethod]
        public void Test_Route_1_3_4_Upper_Bound()
        {
            var bf = new MultipleBinaryFlag(2);
            Assert.ThrowsException<ArgumentOutOfRangeException>(
              () => { bf.SetFlag(2); });
            Assert.AreEqual("TT", bf.ToString());
        }

        [TestMethod]
        public void Test_Route_1_3_4_Upper_Bound_After_Disposed()
        {
            var bf = new MultipleBinaryFlag(2);
            bf.Dispose();
            bf.SetFlag(2);
        }

        [TestMethod]
        public void Test_Route_1_3_4_Upper_Bound_With_Initial_False()
        {
            var bf = new MultipleBinaryFlag(2, false);
            Assert.ThrowsException<ArgumentOutOfRangeException>(
              () => { bf.SetFlag(2); });
            Assert.AreEqual("FF", bf.ToString());
        }

        [TestMethod]
        public void Test_Route_1_3_5_6_Upper_Bound()
        {
            var bf = new MultipleBinaryFlag(2, false);
            bf.SetFlag(1);
            Assert.AreEqual("FT", bf.ToString());
            Assert.ThrowsException<ArgumentOutOfRangeException>(
              () => { bf.ResetFlag(2); });
            Assert.AreEqual("FT", bf.ToString());
        }

        [TestMethod]
        public void Test_Route_1_3_5_7_8()
        {
            var bf = new MultipleBinaryFlag(2, false);
            bf.SetFlag(1);
            Assert.AreEqual("FT", bf.ToString());
            bf.ResetFlag(1);
            Assert.AreEqual("FF", bf.ToString());
            bf.Dispose();
        }

        [TestMethod]
        public void Test_Route_1_3_5_7_8_Double_Dispose()
        {
            var bf = new MultipleBinaryFlag(2, false);
            bf.SetFlag(1);
            Assert.AreEqual("FT", bf.ToString());
            bf.ResetFlag(1);
            Assert.AreEqual("FF", bf.ToString());
            bf.Dispose();
            bf.Dispose();
        }

        [TestMethod]
        public void Test_Route_1_3_5_7_8_GetFlag_After_Dispose()
        {
            var bf = new MultipleBinaryFlag(2, false);
            bf.SetFlag(1);
            Assert.AreEqual("FT", bf.ToString());
            bf.ResetFlag(1);
            Assert.AreEqual("FF", bf.ToString());
            bf.Dispose();
            Assert.IsNull(bf.GetFlag());
        }

        [TestMethod]
        public void Test_Route_1_3_5_7_8_ToString_After_Dispose()
        {
            var bf = new MultipleBinaryFlag(2, false);
            bf.SetFlag(1);
            Assert.AreEqual("FT", bf.ToString());
            bf.ResetFlag(1);
            Assert.AreEqual("FF", bf.ToString());
            bf.Dispose();
            Assert.IsNull(bf.ToString());
        }
    }
}
