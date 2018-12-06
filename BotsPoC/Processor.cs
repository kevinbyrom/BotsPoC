using System;
using System.Collections;
using System.Collections.Generic;


namespace BotsPoC
{

    public enum Registers
    {
        AX,
        BX,
        CX,
        DX
    }

    public enum OpCode : int
    {
        SET = 0,
        MOV,
        LOAD,
        STOR,
        PUSH,
        POP,
        OUT,
        OUTI,
        INP,
        INPI,
        ADD,
        ADDI,
        SUB,
        SUBI,
        MUL,
        MULI,
        DIV,
        DIVI,
        TEQ,
        TEG,
        TEL,
        TG,
        TL,
        JMP,
        TJMP,
        FJMP,
        BR,
        TBR,
        FBR,
        RET
    }

    public struct Instruction
    {
        public uint OpCode;
        public uint Val1;
        public uint Val2;

        public static Instruction Create(uint opCode, uint val = 0)
        {
            return new Instruction() { OpCode = opCode, Val = val };
        }
    }


    public class Processor
    {
        const int MAX_REGISTERS = 4;
        public uint[] Register = new uint[4];
        public bool T;
        public Instruction[] Code;
        public uint CodePtr;
        public Stack<uint> Stack;

        private Action<uint, uint>[] ops;


        public Processor()
        {
            RegisterOpHandlers();
        }

        public void Load(Instruction[] code)
        {
            this.CodePtr = 0;
            this.Code = code;
        }

        public void Run()
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

        public void Execute(Instruction instr)
        {
            var handler = this.ops[instr.OpCode];
            handler(instr.Val1, instr.Val2);
        }

        private void RegisterOpHandlers()
        {
            this.ops[(int)OpCode.JMP] = Jmp;
            this.ops[(int)OpCode.MOV] = Move;
            //            this.ops.Add(OpCode.MOV, Jmp);

            STOR,
        PUSH,
        POP,
        OUT,
        OUTI,
        INP,
        INPI,
        ADD,
        ADDI,
        SUB,
        SUBI,
        MUL,
        MULI,
        DIV,
        DIVI,
        TEQ,
        TEG,
        TEL,
        TG,
        TL,
        JMP,
        TJMP,
        FJMP,
        BR,
        TBR,
        FBR,
        RET
        } 
        
        public void Set(uint val1, uint val2)
        {
            this.Register[val1] = val2;
        }
        
        public void Move(uint val1, uint val2)
        {
            this.Register[val1] = this.Register[val2];
        }

        public void Load(uint val1, uint val2)
        {
            // Grab memory addressed at D and put into specified reg
        }

        public void Store(uint val1, uint val2)
        {
            // Grab value at specified reg and store at memory addressed at D
        }

        public void Push(uint val1, uint val2)
        {
            // Push value to stack from specified register

            this.Stack.Push(this.Register[val1]);
        }

        public void Pop(uint val1, uint val2)
        {
            // Pop value from stack and store at specified register

            this.Register[val1] = this.Stack.Pop();
        }



        public void Jump(uint val1, uint val2)
        {
            if (val1 >= this.Code.Length)
                throw new Exception();

            this.CodePtr = val1;
        }

        public void Test()
        {
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
