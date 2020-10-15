using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    GameObject gameDirectorObject;

    protected GameDirector gameDirector;

    protected TrumpController trumpController;

    [SerializeField]
    protected Text scoreText;
    protected int characterScore;

    protected List<Card> characterCards = new List<Card>();

    protected SpriteRenderer[] characterCardSprites;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameDirector = gameDirectorObject.GetComponent<GameDirector>();
        trumpController = gameDirectorObject.GetComponent<TrumpController>();
    }
    public virtual void GameStart()
    {
        AddCharacterCards(trumpController.DrawCard());
        AddCharacterCards(trumpController.DrawCard());
    }
    protected void AddCharacterCards(Card card)
    {
        characterCards.Add(card);
    }

    protected void ShowCharacterCards()
    {
        for (int i = 0; i < characterCards.Count; i++)
        {
            characterCardSprites[i].sprite = characterCards[i].Sprite;
        }
    }
    bool CharacterScoreSum()
    {
        int aceCount = 0;
        foreach (Card card in characterCards)
        {
            if (card.Number == 1)
            {
                characterScore += 11;
                aceCount++;
            }
            else if (card.Number > 11)
            {
                characterScore += 10;
            }
            else
            {
                characterScore += card.Number;
            }
        }

        if (characterScore == 21 && aceCount == 1)
        {
            return true;
        }

        if (characterScore > 21)
        {
            while (aceCount > 0)
            {
                characterScore -= 10;
                aceCount--;
            }
        }

        return false;
    }
    protected void ShowCharacterScore()
    {
        bool isBlackJack = CharacterScoreSum();

        if (isBlackJack)
        {
            scoreText.text = "BLACKJACK!";
        }
        else if (characterScore > 21)
        {
            scoreText.text = "BUST!";
        }
        else
        {
            scoreText.text = characterScore.ToString();
        }
    }
}
