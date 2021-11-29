using System;

namespace Calculator
{
    class RPN
    {
        // Number of registers of the calculator
        readonly int regNum;
        public int RegistersNumber {
            get { return regNum; }
        }

        // Actual registers of the calculator
        double[] regs;
        public double[] Registers {
            get { return regs; }
        }

        // Constructs a calculator with the default number of registers
        public RPN()
            : this(4) {}

        // Constructs a calculator with a definite number of registers
        public RPN(int regNumber)
        {
            if (regNumber < 2)
                throw new ArgumentException("Registers number must not be less than 2");

            regNum = regNumber;
            regs = new double[regNum];
        }

        #region Operations

        public void Add()
        {
            double xReg = regs[0];
            FlushDown();
            regs[0] += xReg;
        }

        public void Subtract()
        {
            double xReg = regs[0];
            FlushDown();
            regs[0] -= xReg;
        }

        public void Multiply()
        {
            double xReg = regs[0];
            FlushDown();
            regs[0] *= xReg;
        }

        public void Divide()
        {
            double xReg = regs[0];
            FlushDown();
            regs[0] /= xReg;
        }

        #endregion

        #region Register shifts

        // Enters a number to the registers of the calculator by inserting
        // it in the first one and moving up the stack
        public void Enter(double num)
        {
            FlushUp();
            regs[0] = num;
        }

        // Shifts the registers downwards by flushing the first one
        // and cloning the last one
        private void FlushDown()
        {
            for (int i = 0; i < regNum - 1; i++)
                regs[i] = regs[i+1];
        }

        // Shifts the registers upwards by flushing the last one
        // and cloning the first one
        private void FlushUp()
        {
            for (int i = regNum - 1; i > 0; i--)
                regs[i] = regs[i-1];
        }

        #endregion

        // Prints the value of the first two registers
        public override string ToString()
        {
            return string.Format(
                "Y: {0}\nX: {1}",
                regs[1],
                regs[0]
            );
        }
    }
}