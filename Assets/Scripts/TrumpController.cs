using System.Collections.Generic;
using UnityEngine;

public class TrumpController : MonoBehaviour
{
    [SerializeField]
    Sprite[] trump;

    [SerializeField]
    Sprite back;
    public Sprite Back { get => back; }

    List<Card> orderCards;
    Stack<Card> playingCards;

    public void ResetCardsInfo()
    {
        CreateCards();
        ShuffleCards();
    }

    void CreateCards()
    {
        orderCards = new List<Card>();

        for (int i = 1; i < trump.Length; i++)
        {
            if (i < 14)
            {
                orderCards.Add(new Clover(trump[i], i));
            }
            else if (i < 27)
            {
                orderCards.Add(new Diamond(trump[i], i - 13));
            }
            else if (i < 40)
            {
                orderCards.Add(new Hart(trump[i], i - 26));
            }
            else
            {
                orderCards.Add(new Spade(trump[i], i - 39));
            }
        }
    }

    void ShuffleCards()
    {
        playingCards = new Stack<Card>();

        while (orderCards.Count != 0)
        {
            int rand = Random.Range(0, orderCards.Count);
            playingCards.Push(orderCards[rand]);
            orderCards.RemoveAt(rand);
        }
    }

    public Card DrawCard()
    {
        return playingCards.Pop();
    }
}

public class Card
{
    private Sprite sprite;
    private int number;
    private string suit;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int Number { get => number; set => number = value; }
    public string Suit { get => suit; set => suit = value; }

    protected Card(Sprite sprite, int number)
    {
        this.sprite = sprite;
        this.number = number;
    }
}
public class Hart : Card
{
    public Hart(Sprite sprite, int number) : base(sprite, number)
    {
        Suit = "Hart";
    }
}
public class Spade : Card
{
    public Spade(Sprite sprite, int number) : base(sprite, number)
    {
        Suit = "Spade";
    }
}
public class Clover : Card
{
    public Clover(Sprite sprite, int number) : base(sprite, number)
    {
        Suit = "Clover";
    }
}
public class Diamond : Card
{
    public Diamond(Sprite sprite, int number) : base(sprite, number)
    {
        Suit = "Diamond";
    }
}
