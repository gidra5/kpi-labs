using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.CoSFE.DatabaseUtils;
using IIG.BinaryFlag;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private const string Server = @"localhost";
        private const string Database = @"IIG.CoSWE.FlagPoleDB";
        private const string Login = @"sa";
        private const string Password = @"12hr56tf";
        private const int ConnectionTimeout = 75;
        private const bool IsTrusted = false;

        [TestMethod]
        public void TestFlagPoleDatabaseIO()
        {
            var database = new FlagpoleDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);
            var bf = new MultipleBinaryFlag(2);
            var str = "";
            bool? flag;
            int maxIndex = database.GetLstBySql(@"SELECT ALL [MultipleBinaryFlagID] FROM[IIG.CoSWE.FlagpoleDB].[dbo].[MultipleBinaryFlags]")
                .Select(row => Int32.Parse(row[0]))
                .Aggregate(0, (acc, x) => Math.Max(acc, x));
            Assert.IsTrue(database.AddFlag(bf.ToString(), (bool)bf.GetFlag()));
            Assert.IsTrue(database.GetFlag(maxIndex + 1, out str, out flag));
            Assert.IsTrue(str == bf.ToString());
            Assert.IsTrue(flag == (bool)bf.GetFlag());
        }

        [TestMethod]
        public void TestPasswordHashingFileIO()
        {
            string path = ".\\test.txt";

            Assert.IsTrue(BaseFileWorker.Write(PasswordHasher.GetHash("123"), path));
            Assert.IsTrue(BaseFileWorker.ReadAll(path) == PasswordHasher.GetHash("123"));
        }
    }
}

