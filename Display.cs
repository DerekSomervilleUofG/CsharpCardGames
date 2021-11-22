using System;
using System.Collections.Generic;
using System.Text;
using CardStructures;

namespace Display
{
    public class ConsoleOutput
    {
        public void Output(String message)
        {
            Console.WriteLine(message);
        }

        public void Output(int number)
        {
            Console.WriteLine(number);
        }

        public void Output(Hand hand)
        {
            Output(hand.ToString());
        }
    }

    public class ConsoleInput
    {
        public String GetInputString()
        {
            return Console.ReadLine();
        }
        public int GetInputInt()
        {
            return Convert.ToInt32(GetInputString());
        }
    }
}
