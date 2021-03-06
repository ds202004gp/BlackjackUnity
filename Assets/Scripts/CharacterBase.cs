﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject characterController;

    [SerializeField]
    protected Text blackjackOrBust;

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
        AddCharacterCards();
        AddCharacterCards();
    }
    public virtual void ResetCardsInfo()
    {
        blackjackOrBust.text = "";
        characterCards = new List<Card>();
        ShowCharacterScore();
        CharacterCardSpritesReset();

        for (int i = 0; i < characterCardSprites.Count; i++)
        {
            characterCardSprites[i].sprite = trumpController.Back;
        }
    }
    void CharacterCardSpritesReset()
    {
        for (int i = 2; i < characterCardSprites.Count; i++)
        {
            characterCardSprites[i].gameObject.SetActive(false);
        }
    }
    protected void AddCharacterCards()
    {
        characterCards.Add(gameDirector.DrawCard());
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

    void CharacterScoreSum()
    {
        int score = 0;
        int aceCount = 0;

        foreach (Card card in characterCards)
        {
            if (card.Number == 11) aceCount++;
            score += card.Number;
        }

        while (score > 21 && aceCount > 0)
        {
            score -= 10;
            aceCount--;
        }

        characterScore = score;
    }

    public bool IsBlackjack { get => characterScore == 21 && characterCards.Count == 2; }
    public bool IsBust { get => characterScore > 21; }

    protected void ShowCharacterScore()
    {
        CharacterScoreSum();

        if (IsBlackjack)
        {
            blackjackOrBust.text = "BLACKJACK!!";
            blackjackOrBust.color = Color.yellow;
        }
        else if (IsBust)
        {
            blackjackOrBust.text = "BUST...";
            blackjackOrBust.color = Color.blue;
        }

        scoreText.text = $"{characterScore}";
    }
    public int GetScore()
    {
        if (IsBlackjack) return 22;

        return IsBust ? 0 : characterScore;
    }
}
