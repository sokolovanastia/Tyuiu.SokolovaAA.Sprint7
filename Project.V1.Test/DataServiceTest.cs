using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Project.V1.Lib;
using System.IO;
namespace Project.V1.Test
    
{
    [TestClass]
    public class DataServiceTest
    {
        DataService ds = new DataService();
        [TestMethod]
        
            

        
        public void SortNumValid()
        {
            string[,] array = { { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" },
                                { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" } };
            string[,] res = ds.SortNum(array);
            string[,] wait = {  { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" } };
            CollectionAssert.AreEqual(wait, res);
        }


        [TestMethod]
        public void SortRatevalid()
        {
            string[,] array = { { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" } };
            string[,] res = ds.SortRate(array);
            string[,] wait = { { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                               { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" },
                               { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" } };
            CollectionAssert.AreEqual(wait, res);
        }


        [TestMethod]
        public void SortTimeDurationValid()
        {
            string[,] array = { { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                                { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" } };
            string[,] res = ds.SortTimeDuration(array);
            string[,] wait = { { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" } };
            CollectionAssert.AreEqual(wait, res);
        }


        [TestMethod]
        public void SortTimeOpenValid()
        {
            string[,] array = { { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" } };
            string[,] res = ds.SortTimeOpen(array);
            string[,] wait = { { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" },
                                { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" } };
            CollectionAssert.AreEqual(wait, res);
        }


        [TestMethod]
        public void SortTimeCloseValid()
        {
            string[,] array = { { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" } };
            string[,] res = ds.SortTimeClose(array);
            string[,] wait = { { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" } };
            CollectionAssert.AreEqual(wait, res);
        }


        [TestMethod]
        public void SortNameValid()
        {
            string[,] array = { { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" },
                                { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" } };
            //string[,] res = ds.SortName(array);
            string[,] wait = { { "2", "AuttoPro", "10:00", "20:00", "8 345 255 35 53", "4,2" },
                                { "3", "ProTech", "9:00", "18:00", "8 345 232 23 11", "3,7" },
                                { "1", "CrBroke", "8:30", "19:00", "8 345 210 34 45", "3,5" } };
        }
    }
}
