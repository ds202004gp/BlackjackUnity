using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    Transform characterTransform;

    [SerializeField]
    GameObject gameDirectorObject;

    protected GameDirector gameDirector;

    protected TrumpController trumpController;

    [SerializeField]
    protected Text scoreText;
    protected int characterScore;

    protected List<Card> characterCards;
    protected List<SpriteRenderer> characterCardSprites;
    protected bool isBlackjack;
    protected bool isBust;

    [SerializeField]
    SpriteRenderer cardPrefab;

    [SerializeField]
    float direction;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        gameDirector = gameDirectorObject.GetComponent<GameDirector>();
        trumpController = gameDirectorObject.GetComponent<TrumpController>();

        characterCardSprites = new List<SpriteRenderer>();
        characterCardSprites =
            GetComponentsInChildren<SpriteRenderer>().ToList();
    }
    public virtual void GameStart()
    {
        AddCharacterCards(trumpController.DrawCard());
        AddCharacterCards(trumpController.DrawCard());
    }
    public void ResetCardsInfo()
    {
        isBlackjack = false;
        isBust = false;
        characterCards = new List<Card>();
        ShowCharacterScore();
        CharacterCardSpritesReset();

        for (int i = 0; i < characterCardSprites.Count; i++)
        {
            characterCardSprites[i].sprite = trumpController.back;
        }
    }
    void CharacterCardSpritesReset()
    {
        for (int i = 2; i < characterCardSprites.Count; i++)
        {
            characterCardSprites[i].gameObject.SetActive(false);
        }
    }
    protected void AddCharacterCards(Card card)
    {
        characterCards.Add(card);
    }

    protected void ShowCharacterCards()
    {
        if (characterCards.Count > characterCardSprites.Count)
        {
            Vector2 cardPos = characterCardSprites[characterCardSprites.Count - 1].transform.position;
            CharacterCardSpritesAdd(cardPos);
        }
        for (int i = 0; i < characterCards.Count; i++)
        {
            characterCardSprites[i].gameObject.SetActive(true);
            characterCardSprites[i].sprite = characterCards[i].Sprite;
        }
    }
    void CharacterCardSpritesAdd(Vector2 cardPos)
    {
        Vector2 AddCardPos = cardPos;
        AddCardPos += new Vector2(direction, 0);
        SpriteRenderer card = Instantiate(cardPrefab, AddCardPos, Quaternion.identity);
        card.name = $"Card{characterCards.Count}";
        card.gameObject.transform.parent = characterTransform;
        characterCardSprites.Add(card);
    }

    bool CharacterScoreSum()
    {
        int score = 0;
        int aceCount = 0;
        foreach (Card card in characterCards)
        {
            if (card.Number == 11)
            {
                aceCount++;
            }
            score += card.Number;
        }

        if (score > 21)
        {
            for (int i = 0; i < aceCount; i++)
            {
                score -= 10;
            }
        }

        characterScore = score;
        return score == 21 && characterCards.Count == 2;
    }
    protected void ShowCharacterScore()
    {
        isBlackjack = CharacterScoreSum();

        if (isBlackjack)
        {
            scoreText.text = "BLACKJACK!";
        }
        else if (characterScore > 21)
        {
            isBust = true;
            scoreText.text = "BUST!";
        }
        else
        {
            scoreText.text = characterScore.ToString();
        }
    }
    public int GetScore()
    {
        if (isBlackjack)
        {
            return 22;
        }
        else if (isBust)
        {
            return 0;
        }
        else
        {
            return characterScore;
        }
    }
}
