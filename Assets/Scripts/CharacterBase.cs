using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    Text nameText;
    protected string characterName;

    [SerializeField]
    Text scoreText;
    protected int characterScore;

    protected List<Card> drawCards = new List<Card>();

    protected SpriteRenderer[] characterCards;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // nameText.text = characterName;
        // scoreText.text = "---";
    }
    public virtual void AddDrawCards(Card card)
    {
        drawCards.Add(card);
    }

    public virtual void ShowDrawCards()
    {
        for (int i = 0; i < drawCards.Count; i++)
        {
            characterCards[i].sprite = drawCards[i].Sprite;
        }
    }
}
