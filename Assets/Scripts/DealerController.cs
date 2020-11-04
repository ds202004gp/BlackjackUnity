using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealerController : CharacterBase
{
    PlayerController playerController;

    int bet;
    public int Bet { get => bet; set => bet = value; }

    protected override void Awake()
    {
        base.Awake();
        playerController = characterController.GetComponent<PlayerController>();
    }
    public override void GameStart()
    {
        base.GameStart();
        UpCard();
    }
    public override void ResetCardsInfo()
    {
        base.ResetCardsInfo();
        bet = 0;
        dividend = 0;
    }
    void UpCard()
    {
        characterCardSprites[0].sprite = characterCards[0].Sprite;
        characterScore = characterCards[0].Number;
        scoreText.text = characterScore.ToString();
    }
    public void DrawDealer()
    {
        while (true)
        {
            ShowCharacterCards();
            ShowCharacterScore();

            if (characterScore >= 17)
            {
                break;
            }

            AddCharacterCards(trumpController.DrawCard());
        }
    }

    int dividend;
    public int Dividend(float dividendMultiplier)
    {
        dividend = (int)(bet * dividendMultiplier);
        return dividend;
    }
    public void DividendToPlayer()
    {
        playerController.Money += dividend;
    }
}
