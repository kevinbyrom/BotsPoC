using System;

namespace BotsPoC
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Processor p = new Processor();

            var code = new Instruction[] { Instruction.Create(1) };
            p.Load(code);

            p.Execute();
        }
    }
}
