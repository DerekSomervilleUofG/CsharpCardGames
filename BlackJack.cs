using System;
using System.Collections.Generic;
using System.Text;
using CardStructures;
using CardGame;

namespace BlackJack
{

    public enum BlackJackActions
    {
        TWIST,
        STICK,
        PLAY,
        END
    }

    public class BlackJack : CardGame.CardGame
    {
        private int maxScore = 21;
        public int noOfCards = 2;

        public int getMaxScore()
        {
            return maxScore;
        }

        public void help()
        {
            UserOutput.Output("Please select one of the following options:");
            foreach (BlackJackActions action in Enum.GetValues(typeof(BlackJackActions)))
            {
                UserOutput.Output(action.ToString());
            }
        }

        public BlackJackActions GetPlayerAction(Player player)
        {
            String userChoice = " ";
            BlackJackActions userAction;
            help();
            if (player.HasHand())
            {
                UserOutput.Output(player.GetHand());
            }
            userChoice = UserInput.GetInputString();
            BlackJackActions[] actions = (BlackJackActions[])Enum.GetValues(typeof(BlackJackActions));
            int counter = 0;
            do 
            {
                userAction = actions[counter];
                counter += 1;
            } while (userAction.ToString().Substring(0, 1) != userChoice.ToUpper().Substring(0, 1) && counter < actions.Length);
            UserOutput.Output("You chose " + userAction.ToString());
            return userAction;
        }

        private void userPlays(Player player)
        {
            BlackJackActions userAction = BlackJackActions.PLAY;

            while (GetScore(player.GetHand()) <= maxScore && userAction != BlackJackActions.STICK)
            {
                userAction = GetPlayerAction(player);
                if (userAction == BlackJackActions.TWIST)
                {
                    UserOutput.Output("You twisted");
                    player.GetHand().Add(Deck.PlayACard());
                }

            }

        }

        public void ComputerPlays(Player player)
        {
            while (GetScore(player.GetHand()) <= player.levelOfRisk)
            {
                player.GetHand().Add(Deck.PlayACard());
            }
        }

        public void play()
        {
            this.SetNoOfCards(2);
            this.Initiate();
            userPlays(this.Players[0]);
            for (int counter = 1; counter < Players.Count; counter++)
            {
                ComputerPlays(Players[counter]);
            }
            determineWinner();
        }

        public void determineWinner()
        {
            int winningScore = 0;
            String winningName = "";
            int currentScore = 0;
            Hand winningHand = new Hand();
            foreach (Player player in Players)
            {
                currentScore = GetScore(player.GetHand());
                if (currentScore <= maxScore && currentScore > winningScore)
                {
                    winningName = player.GetName();
                    winningScore = currentScore;
                    winningHand = player.GetHand();
                }
                else if (currentScore > maxScore)
                {
                    UserOutput.Output(player.GetName() + " you are bust");
                }
            }
            UserOutput.Output("The winner is " + winningName);
            UserOutput.Output(winningHand);

        }


        public int GetScore(Hand hand)
        {
            int score = 0;
            foreach (Card card in hand.GetHandOfCards())
            {
                score += card.GetNumber();
            }
            return score;
        }

    }
}
