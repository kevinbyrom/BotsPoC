using System;
using System.Collections;
using System.Collections.Generic;


namespace BotsPoC
{

    public static class Registers
    {
        public const uint AX = 0;
        public const uint BX = 1;
        public const uint CX = 2;
        public const uint DX = 3;
    }


    public static class OpCodes
    {
        public const uint SET = 0;
        public const uint MOV = 1;
        public const uint LOAD = 2;
        public const uint STOR = 3;
        public const uint PUSH = 4;
        public const uint POP = 5;
        public const uint OUT = 6;
        public const uint OUTI = 7;
        public const uint INP = 8;
        public const uint INPI = 9;
        public const uint ADD = 10;
        public const uint ADDI = 11;
        public const uint SUB = 12;
        public const uint SUBI = 13;
        public const uint MUL = 14;
        public const uint MULI = 15;
        public const uint DIV = 16;
        public const uint DIVI = 17;
        public const uint TEQ = 18;
        public const uint TEG = 19;
        public const uint TEL = 20;
        public const uint TG = 21;
        public const uint TL = 22;
        public const uint JMP = 23;
        public const uint TJMP = 24;
        public const uint FJMP = 25;
        public const uint BR = 26;
        public const uint TBR = 27;
        public const uint FBR = 28;
        public const uint RET = 29;
    }


    public struct Instruction
    {
        public uint OpCode;
        public uint Val1;
        public uint Val2;

        public static Instruction Create(uint opCode, uint val1 = 0, uint val2 = 0)
        {
            return new Instruction() { OpCode = opCode, Val1 = val1, Val2 = val2 };
        }
    }


    public class OpHandler
    {
        private Action<uint, uint> action;

        public void Execute(uint v1, uint v2)
        {
            this.action.Invoke(v1, v2);
        }

        public void Do(Action<uint, uint> action)
        {
            this.action = action;
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

        private Dictionary<uint, OpHandler> handlers;


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
            var handler = this.handlers[instr.OpCode];

            if (handler == null)
                throw new Exception("Unknown opcode");

            handler.Execute(instr.Val1, instr.Val2);
        }

        private void RegisterOpHandlers()
        {
            this.handlers = new Dictionary<uint, OpHandler>();
            
            When(OpCodes.SET).Do((arg1, arg2) => Set(arg1, arg2));
            When(OpCodes.MOV).Do((arg1, arg2) => Move(arg1, arg2));
            When(OpCodes.LOAD).Do((arg1, arg2) => Load(arg1, arg2));
            When(OpCodes.STOR).Do((arg1, arg2) => Store(arg1, arg2));
            When(OpCodes.PUSH).Do((arg1, arg2) => Push(arg1));
            When(OpCodes.POP).Do((arg1, arg2) => Pop(arg1));
            When(OpCodes.OUT).Do((arg1, arg2) => Output(arg1, arg2));
            When(OpCodes.INP).Do((arg1, arg2) => Input(arg1, arg2));
            When(OpCodes.ADD).Do((arg1, arg2) => Add(arg1, arg2));
            When(OpCodes.SUB).Do((arg1, arg2) => Subtract(arg1, arg2));
            When(OpCodes.MUL).Do((arg1, arg2) => Multiply(arg1, arg2));
            When(OpCodes.DIV).Do((arg1, arg2) => Divide(arg1, arg2));
            When(OpCodes.TEQ).Do((arg1, arg2) => TestEquals(arg1, arg2));
            When(OpCodes.TEG).Do((arg1, arg2) => TestEqualsOrGreater(arg1, arg2));
            When(OpCodes.TEL).Do((arg1, arg2) => TestEqualsOrLess(arg1, arg2));
            When(OpCodes.TG).Do((arg1, arg2) => TestGreater(arg1, arg2));
            When(OpCodes.TL).Do((arg1, arg2) => TestLess(arg1, arg2));
            When(OpCodes.JMP).Do((arg1, arg2) => Jump(arg1));
            When(OpCodes.TJMP).Do((arg1, arg2) => JumpIfTrue(arg1));
            When(OpCodes.FJMP).Do((arg1, arg2) => JumpIfFalse(arg1));
            When(OpCodes.BR).Do((arg1, arg2) => Branch(arg1));
            When(OpCodes.TBR).Do((arg1, arg2) => BranchIfTrue(arg1));
            When(OpCodes.FBR).Do((arg1, arg2) => BranchIfFalse(arg1));
            When(OpCodes.RET).Do((arg1, arg2) => Return());
        } 
        
        private OpHandler When(uint opCode)
        {
            var handler = new OpHandler();

            this.handlers.Add(opCode, handler);

            return handler;
        }


        #region Data Instructions

        public void Set(uint reg, uint val)
        {
            this.Register[reg] = val;
        }
        

        public void Move(uint reg, uint val)
        {
            this.Register[reg] = this.Register[val];
        }


        public void Load(uint reg, uint val)
        {
            // Grab memory addressed at D and put into specified reg
        }


        public void Store(uint reg, uint val)
        {
            // Grab value at specified reg and store at memory addressed at D
        }


        public void Push(uint reg)
        {
            // Push value to stack from specified register

            this.Stack.Push(this.Register[reg]);
        }


        public void Pop(uint reg)
        {
            // Pop value from stack and store at specified register

            this.Register[reg] = this.Stack.Pop();
        }


        public void Output(uint reg, uint port)
        {
            throw new NotImplementedException();
        }


        public void Input(uint port, uint reg)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Arithmetic Instructions

        public void Add(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }


        public void Subtract(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }


        public void Multiply(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }


        public void Divide(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Comparison Instructions

        public void TestEquals(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }


        public void TestEqualsOrGreater(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }


        public void TestEqualsOrLess(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }


        public void TestGreater(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }


        public void TestLess(uint reg1, uint reg2)
        {
            throw new NotImplementedException();
        }


        #endregion


        #region Branch Instructions

        public void Jump(uint pos)
        {
            if (pos >= this.Code.Length)
                throw new Exception();

            this.CodePtr = pos;
        }


        public void JumpIfTrue(uint pos)
        {
            throw new NotImplementedException();
        }


        public void JumpIfFalse(uint pos)
        {
            throw new NotImplementedException();
        }


        public void Branch(uint pos)
        {
            throw new NotImplementedException();
        }


        public void BranchIfTrue(uint pos)
        {
            throw new NotImplementedException();
        }


        public void BranchIfFalse(uint pos)
        {
            throw new NotImplementedException();
        }


        public void Return()
        {
            throw new NotImplementedException();
        }

        #endregion

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
