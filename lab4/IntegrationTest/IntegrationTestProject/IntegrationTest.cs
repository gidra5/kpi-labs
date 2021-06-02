using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.CoSFE.DatabaseUtils;
using IIG.BinaryFlag;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class IntegrationTestFlagPole
    {
        private const string Server = @"localhost";
        private const string Database = @"IIG.CoSWE.FlagPoleDB";
        private const string Login = @"sa";
        private const string Password = @"12hr56tf";
        private const int ConnectionTimeout = 75;
        private const bool IsTrusted = false;

        FlagpoleDatabaseUtils connectDatabase()
        {
            return new FlagpoleDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);
        }

        int maxIndex(FlagpoleDatabaseUtils database)
        {
            int? maxIndex = database.GetIntBySql(@"SELECT MAX([MultipleBinaryFlagID]) FROM [IIG.CoSWE.FlagpoleDB].[dbo].[MultipleBinaryFlags]");
            return maxIndex != null ? (int)maxIndex : 0;
        }

        void checkFlagPole(FlagpoleDatabaseUtils database, MultipleBinaryFlag bf)
        {
            int index = maxIndex(database);

            Assert.IsTrue(database.AddFlag(bf.ToString(), (bool)bf.GetFlag()));
            Assert.IsTrue(database.GetFlag(index + 1, out string str, out bool? flag));
            Assert.AreEqual(str, bf.ToString());
            Assert.AreEqual(flag, bf.GetFlag());
        }

        [TestMethod]
        public void TestFlagPoleDatabaseIO()
        {
            var database = connectDatabase();
            var bf = new MultipleBinaryFlag(7);
            bf.ResetFlag(4);

            checkFlagPole(database, new MultipleBinaryFlag(2));
            checkFlagPole(database, new MultipleBinaryFlag(10));
            checkFlagPole(database, bf);
        }
    }

    [TestClass]
    public class IntegrationTestHashing
    {
        void checkHash(string filepath, string hash)
        {
            Assert.IsTrue(BaseFileWorker.Write(hash, filepath));
            Assert.AreEqual(BaseFileWorker.ReadAll(filepath), hash);
        }

        [TestMethod]
        public void TestPasswordHashingFileIO()
        {
            string filepath = ".\\test.txt";

            checkHash(filepath, PasswordHasher.GetHash(@"12hr56tf"));
            checkHash(filepath, PasswordHasher.GetHash("oskdoskodskd"));
            checkHash(filepath, PasswordHasher.GetHash("123"));
        }
    }
}

