using System;
using CardStructures;
using Display;
using BlackJack;

public class CSharpCardGames
{
    static void Main(string[] args)
    {
        ConsoleOutput output = new ConsoleOutput();
        ConsoleInput input = new ConsoleInput();
        output.Output("Would you like to play Black Jack?");
        String answer = input.GetInputString();
        if (answer == "Y")
        {
            BlackJack.BlackJack blackJack = new BlackJack.BlackJack();
            blackJack.Play();
        }
    }
}