using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Hashing_Equal_Strings_Produces_Equal_Hashes()
        {
            var str = "madlmadlkasd';ol";
            var hash1 = PasswordHasher.GetHash(str);
            var hash2 = PasswordHasher.GetHash(str);

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void Test_Hashing_Different_Strings_Produces_Different_Hashes()
        {
            var str1 = "madlmadlkasd';ol";
            var str2 = "madlmadlkasd';ol6";
            var hash1 = PasswordHasher.GetHash(str1);
            var hash2 = PasswordHasher.GetHash(str2);

            Assert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void Test_Hashing_With_0_Or_1_Mod_Throws()
        {
            var str = "madlmadlkasd';ol";

            Assert.ThrowsException<System.ArgumentException>(() => PasswordHasher.GetHash(str, "", 0));
            Assert.ThrowsException<System.ArgumentException>(() => PasswordHasher.GetHash(str, "", 1));
        }

        [TestMethod]
        public void Test_Hashing_With_Empty_String_Salt_Dont_Change_Hash()
        {
            var str = "madlmadlkasd';ol";
            var salt = "";
            var hash1 = PasswordHasher.GetHash(str);
            var hash2 = PasswordHasher.GetHash(str, salt);

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void Test_Hashing_Equal_Strings_With_Custom_Salt_And_Mod_Produces_Equal_Hashes()
        {
            var str = "madlmadlkasd';ol";
            var salt = "kndaslkd'dsd";
            uint mod = 5321;
            var hash1 = PasswordHasher.GetHash(str, salt, mod);
            var hash2 = PasswordHasher.GetHash(str, salt, mod);

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void Test_Hashing_Equal_Strings_With_Custom_Salt_Produces_Equal_Hashes()
        {
            var str = "madlmadlkasd';ol";
            var salt = "kndaslkd'dsd";
            var hash1 = PasswordHasher.GetHash(str, salt);
            var hash2 = PasswordHasher.GetHash(str, salt);

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void Test_Hashing_Equal_Strings_With_Different_Salt_And_Mod_Produces_Different_Hashes()
        {
            var str = "madlmadlkasd';ol";
            var salt = "kndaslkd'dsd";
            uint mod = 5321;
            var hash1 = PasswordHasher.GetHash(str);
            var hash2 = PasswordHasher.GetHash(str, salt);
            var hash3 = PasswordHasher.GetHash(str, salt, mod);

            Assert.AreNotEqual(hash1, hash2);
            Assert.AreNotEqual(hash2, hash3);
            Assert.AreNotEqual(hash1, hash3);
        }
    }
}
