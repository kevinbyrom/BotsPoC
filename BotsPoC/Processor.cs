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

        private Dictionary<OpCode, OpHandler> handlers;


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
        }

        private void RegisterOpHandlers()
        {
            this.handlers = new Dictionary<OpCode, OpHandler>();
            
            When(OpCode.SET).Do((arg1, arg2) => Set(arg1, arg2));
            When(OpCode.MOV).Do((arg1, arg2) => Move(arg1, arg2));
            When(OpCode.LOAD).Do((arg1, arg2) => Load(arg1, arg2));
            When(OpCode.STOR).Do((arg1, arg2) => Store(arg1, arg2));
            When(OpCode.PUSH).Do((arg1, arg2) => Push(arg1));
            When(OpCode.POP).Do((arg1, arg2) => Pop(arg1));
            When(OpCode.OUT).Do((arg1, arg2) => Output(arg1, arg2));
            When(OpCode.INP).Do((arg1, arg2) => Input(arg1, arg2));
            When(OpCode.ADD).Do((arg1, arg2) => Add(arg1, arg2));
            When(OpCode.SUB).Do((arg1, arg2) => Subtract(arg1, arg2));
            When(OpCode.MUL).Do((arg1, arg2) => Multiply(arg1, arg2));
            When(OpCode.DIV).Do((arg1, arg2) => Divide(arg1, arg2));
            When(OpCode.TEQ).Do((arg1, arg2) => TestEquals(arg1, arg2));
            When(OpCode.TEG).Do((arg1, arg2) => TestEqualsOrGreater(arg1, arg2));
            When(OpCode.TEL).Do((arg1, arg2) => TestEqualsOrLess(arg1, arg2));
            When(OpCode.TG).Do((arg1, arg2) => TestGreater(arg1, arg2));
            When(OpCode.TL).Do((arg1, arg2) => TestLess(arg1, arg2));
            When(OpCode.JMP).Do((arg1, arg2) => Jump(arg1));
            When(OpCode.TJMP).Do((arg1, arg2) => JumpIfTrue(arg1));
            When(OpCode.FJMP).Do((arg1, arg2) => JumpIfFalse(arg1));
            When(OpCode.BR).Do((arg1, arg2) => Branch(arg1));
            When(OpCode.TBR).Do((arg1, arg2) => BranchIfTrue(arg1));
            When(OpCode.FBR).Do((arg1, arg2) => BranchIfFalse(arg1));
            When(OpCode.RET).Do((arg1, arg2) => Return());


            //this.ops[(int)OpCode.JMP] = { Jump };
            //this.ops[(int)OpCode.MOV] = Move;
            // this.When(Ops.Jmp).Do((v1,v1)=>{ Jump(v1) })
        } 
        
        private OpHandler When(OpCode opCode)
        {
            var handler = new OpHandler();

            this.handlers.Add(opCode, handler);

            return handler;
        }

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

        public void Jump(uint pos)
        {
            if (pos >= this.Code.Length)
                throw new Exception();

            this.CodePtr = pos;
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
