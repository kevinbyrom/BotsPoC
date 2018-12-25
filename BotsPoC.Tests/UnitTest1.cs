using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BotsPoC;


namespace BotsPoC.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSet()
        {
            var proc = new Processor();

            proc.Set(Registers.AX, 123);
            proc.Set(Registers.BX, 456);
            proc.Set(Registers.CX, 789);
            proc.Set(Registers.DX, 999);

            Assert.Equals(proc.Register[Registers.AX], 123);
            Assert.Equals(proc.Register[Registers.BX], 345);
            Assert.Equals(proc.Register[Registers.CX], 789);
            Assert.Equals(proc.Register[Registers.DX], 999);
        }
    }
}
