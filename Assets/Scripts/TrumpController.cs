using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpController : MonoBehaviour
{
    [SerializeField]
    Sprite[] cards;

    List<int> drawCards = new List<int>();

    public Sprite DrawCard()
    {
        while (true)
        {
            int rand = Random.Range(1, 54);
            if (!drawCards.Contains(rand))
            {
                drawCards.Add(rand);
                return cards[rand];
            }
        }
    }
}
public class Card
{
    int num;
}
public class Hart : Card
{

}
public class Spade : Card
{

}
public class Clover : Card
{

}
public class Daia : Card
{

}
