using System;


namespace BotsPoC
{
    public struct Instruction
    {
        public int Code;
    }

    public class Processor
    {
        public int AX;
        public int BX;
        public int T;
        public Instruction[] Code;
        public int CodePtr;

        public Processor()
        {
        }

        public void NextCycle()
        {

        }

        public void Jmp()
        {
            int pos = this.AX;

            if (pos >= this.Code.Length)
                throw new Exception();

            this.CodePtr = this.AX;
        }

        public void Test()
        {
            if (this.AX == this.BX)
                this.T = 1;
            else
                this.T = 0;
        }
    }

    /*
     * MOV 1 AX
     * MOV 2 BX
     * TGE AX BX
     * 
    */
}
