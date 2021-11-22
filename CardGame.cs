using System;
using System.Collections.Generic;
using CardStructures;
using Display;

namespace CardGame
{

    public enum PlayerType
    {
        USER,
        COMPUTER,
        DEALER
    }

    public class Player
    {
        private readonly Random _rnd = new Random();
        public int levelOfRisk;
        private Hand hand;
        private PlayerType playerType;
        private String name;


        public Player(PlayerType playerType, String name, int levelOfRisk)
        {
            this.playerType = playerType;
            this.name = name;
            int risk;
            if (levelOfRisk == 0)
            {
                risk = 11 + _rnd.Next(0,8);
            }
            else
            {
                risk = levelOfRisk;
            }
            this.levelOfRisk = risk;
        }

        public Boolean HasHand()
        {
            return hand != null;
        }

        public PlayerType GetPlayerType()
        {
            return playerType;
        }

        public void SetHand(Hand hand)
        {
            this.hand = hand;
        }

        public void SetPlayerType(PlayerType playerType)
        {
            this.playerType = playerType;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public String GetName() { return this.name; }

        public Hand GetHand()
        {
            return this.hand;
        }

        public String NextPlay()
        {
            return "";
        }
    }
    public class CardGame
    {
        protected int noOfCards = 2;
        protected Deck Deck = new Deck();
        protected List<Player> Players = new List<Player>();
        protected ConsoleInput UserInput = new ConsoleInput();
        protected ConsoleOutput UserOutput = new ConsoleOutput();


        public void SetNoOfCards(int noOfCards)
        {
            this.noOfCards = noOfCards;
        }

        public CardGame()
        {

        }

        private void createHumanPlayer()
        {
            UserOutput.Output("What is your name");
            String name = UserInput.GetInputString();
            Players.Add(new Player(PlayerType.USER, name, 0));
        }

        private void createComputerPlayers(int noOfPlayers)
        {
            Player dealer = new Player(PlayerType.DEALER, "Dealer 1", 17);
            Players.Add(dealer);
            noOfPlayers -= 1; //Remove the dealer
            for (int counter = 2; counter < noOfPlayers; counter++)
            {
                Players.Add(new Player(PlayerType.COMPUTER, "Comp" + counter, 0));
            }
        }

        protected void InitiatePlayers()
        {
            Players.Clear();
            createHumanPlayer();
            UserOutput.Output("How many players, minimum of two?");
            int noOfPlayers = UserInput.GetInputInt();
            createComputerPlayers(noOfPlayers);


        }

        public void DealCards()
        {
            Boolean allCards = false;
            int noOfCards;
            if (this.noOfCards == 0)
            {
                Double calculateNoOfCards = Deck.Size() / Players.Count;
                noOfCards = Convert.ToInt32(Math.Floor(calculateNoOfCards));
                allCards = true;
            }
            else
            {
                noOfCards = this.noOfCards;
            }
            foreach (Player player in Players)
            {
                Hand hand = new Hand();
                DealHand(hand, noOfCards);
                player.SetHand(hand);
            }
            if (allCards)
            {
                foreach (Player player in Players)
                {
                    if (Deck.Size() > 0)
                    {
                        player.GetHand().Add(Deck.PlayACard());
                    }
                }
            }
        }

        public void DealHand(Hand hand, int noOfCards)
        {

            for (int cardCounter = 0; cardCounter < noOfCards; cardCounter++)
            {
                if (Deck.Size() > 0)
                {
                    hand.Add(Deck.PlayACard());
                }
            }
        }


        public void Initiate()
        {
            InitiatePlayers();
            Deck.ShuffleDeck();
            DealCards();
        }

        public void Play()
        {
            Initiate();
            int counterOfPlayers = 0;
            while (!PlayerHasWon(Players[counterOfPlayers]))
            {
                PlayerPlaysHand(Players[counterOfPlayers]);
                counterOfPlayers = (counterOfPlayers + 1) % Players.Count;
            }

        }

        public int GetScore(Hand hand)
        {
            return 0;
        }

        public void PlayerPlaysHand(Player player)
        {

        }

        public Boolean PlayerHasWon(Player player)
        {
            Boolean winner = true;
            return winner;
        }

        public void ShowPlayers()
        {
            foreach (Player player in Players)
            {
                UserOutput.Output(player.GetName());
                UserOutput.Output(player.GetHand());

            }
        }

    }
}
