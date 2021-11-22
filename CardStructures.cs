using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CardStructures
{
    public enum Suit
    {
        HEARTS,
        DIAMONDS,
        SPADES,
        CLUBS
    }

    public class Face 
    {
        public static List<String> s_FaceCards = new List<String> { "Ace", "King", "Queen", "Jack", "10", "9", "8", "7", "6", "5", "4", "3", "2" };

        public String FaceName { get; }
        public int FaceNumber { get; }
        public String FaceShortCode { get; }

        public Face(String faceName, int faceNumber)
        {
            this.FaceName = faceName;
            this.FaceNumber = faceNumber;
            this.FaceShortCode = faceName.Substring(0, 1);
        }

        public String ToString()
        {
            return FaceShortCode;
        }
    }
    public class Card
    {
        private Suit _suit;
        private Face _face;
        private String _cardShortCode;



        public Card(Suit suit, Face face)
        {
            this._suit = suit;
            this._face = face;
            this._cardShortCode = suit.ToString().Substring(0, 1) + face.FaceShortCode;
        }

        public Card(String card)
        {
            this._cardShortCode = card;
        }

        public int GetNumber()
        {
  
            return _face.FaceNumber;
        }

        public String ToString()
        {
            return _cardShortCode;
        }
    }

    public class Hand
    {
        protected List<Card> handOfCards = new List<Card>();

        public Hand()
        {
            this.handOfCards = new List<Card>();
        }

        public Hand(String[] listOfCards)
        {
            foreach (String card in listOfCards)
            {
                this.handOfCards.Add(new Card(card));
            }
        }

        public Hand(String listOfCards)
        {
            foreach (String card in listOfCards.Split(","))
            {
                this.handOfCards.Add(new Card(card));
            }
        }

        public List<Card> GetHandOfCards()
        {

            return handOfCards;
        }

        public Card GetFirstCard()
        {
            return handOfCards[0];
        }

        public Boolean PlayACard(Card card)
        {
            return handOfCards.Remove(card);
        }


        public Card PlayACard(String cardShortCode)
        {
            Card card = new Card(cardShortCode);
            if (!PlayACard(card))
            {
                card = null;
            }
            return card;
        }

        public Card PlayACard()
        {
            return PlayACard(0);
        }

        public Card PlayACard(int index)
        {
            Card card = handOfCards[index];
            handOfCards.RemoveAt(index);
            return card;
                
        }

        public void Add(Card card)
        {
            handOfCards.Add(card);
        }

        public void Set(int index, Card card)
        {
            handOfCards[index] = card;
        }

        public Hand Copy()
        {
            Hand newHand = new Hand();
            foreach (Card card in handOfCards)
            {
                newHand.Add(card);
            }
            return newHand;
        }

        public Boolean IsEmpty()
        {
            Boolean result = false;
            if (handOfCards.Count == 0)
            {
                result = true;
            }
            return result;
        }

        public void Remove(Card card)
        {
            handOfCards.Remove(card);
        }

        public void Clear()
        {
            handOfCards.Clear();
        }

        public int Size()
        {
            return handOfCards.Count;
        }

        public String ToString()
        {
            String display = "";
            String prefix = "";
            foreach (Card card in handOfCards)
            {
                display += prefix + card.ToString();
                prefix = ", ";
            }
            return display;
        }

        public void SortHand()
        {
            handOfCards.Sort();
        }
    }

    public class Deck : Hand
    {
        private readonly Random _rnd = new Random();
        public void GenerateDeck()
        {
            int counter = 1;
            Clear();
            foreach (String faceName in Face.s_FaceCards)
                {
                Face face = new Face(faceName, counter);
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    Add(new Card(suit, face));
                }
                counter += 1;
            }
        }
        public void ShuffleDeck()
        {
            
            if (handOfCards.Count == 0)
            {
                GenerateDeck();
            }
            List<Card> randomizeHand = new List<Card>();
            while (handOfCards.Count > 0)
            {
                int index = _rnd.Next(0, handOfCards.Count); 
                randomizeHand.Add(handOfCards[index]); 
                handOfCards.RemoveAt(index);
            }
            handOfCards = randomizeHand;
        } 

    }
}
