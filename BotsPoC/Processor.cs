using System;
using System.Collections;
using System.Collections.Generic;


namespace BotsPoC
{


    public struct Instruction
    {
        public int Code;

        public static Instruction Create(int code)
        {
            var i = new Instruction();
            i.Code = code;

            return i;
        }
    }


    public class Processor
    {
        public double AX;
        public double BX;
        public string SX;
        public bool T;
        public Instruction[] Code;
        public int CodePtr;


        public Processor()
        {
        }

        public void Load(Instruction[] code)
        {
            this.CodePtr = 0;
            this.Code = code;
        }

        public void Execute()
        {
            while (this.CodePtr < this.Code.Length)
            {
                Cycle();
            }
        }

        public void Cycle()
        {
            if (this.CodePtr >= this.Code.Length)
                return;

            this.CodePtr++;
        }

        public void Jmp()
        {
            int pos = (int)this.AX;

            if (pos >= this.Code.Length)
                throw new Exception();

            this.CodePtr = (int)this.AX;
        }

        public void Test()
        {
            if (this.AX == this.BX)
                this.T = true;
            else
                this.T = false;
        }
    }

    /*
     * MOV 1 AX
     * MOV 2 BX
     * TGE AX BX
     * --------------------------
     *
     * DECLARE X;
     * 
     * X = In1;
     * 
     * IF X == 0 {
     *  Out1 = 1
     * }
     * ELSE
     * {
     *  Out1 = 0
     * }
     * -------------------------
     * PUSH 0   'STK1 = 0
     * PUSH 0
     * SETA 21
     * INT
     * SETD 0
     * STORA
     * STORA 0
     * LOADA 0
     * LOADB 1
     * TEST
     * JEQ  
     *  
     * LOAD 0      ' AX = 0
     * 
    */
}
