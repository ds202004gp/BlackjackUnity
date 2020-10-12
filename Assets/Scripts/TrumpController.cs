using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;
using UnityEditorInternal;

public class TrumpController : MonoBehaviour
{
    [SerializeField]
    Sprite[] trump;

    List<Card> orderCards = new List<Card>();
    Stack<Card> playingCards = new Stack<Card>();

    void Start()
    {
        CreateCards();

        ShuffleCards();

        foreach (var item in playingCards)
        {
            Debug.Log($"{item.Suit} {item.Number} {item.Sprite}");
        }

        Debug.Log(playingCards.Count);
    }

    public void CreateCards()
    {
        for (int i = 1; i < trump.Length; i++)
        {
            if (i < 14)
            {
                orderCards.Add(new Clover(trump[i], i));
            }
            else if (i < 27)
            {
                orderCards.Add(new Daia(trump[i], i - 13));
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

    public void ShuffleCards()
    {
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
public class Daia : Card
{
    public Daia(Sprite sprite, int number) : base(sprite, number)
    {
        Suit = "Daia";
    }
}
