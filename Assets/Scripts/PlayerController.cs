using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharacterBase
{
    [SerializeField]
    Text moneyText;

    [SerializeField]
    int money;

    public int Money
    {
        get => money;

        set
        {
            if (money > 0)
            {
                money = value;
                moneyText.text = $"${money:n0}";
            }
        }
    }

    [SerializeField]
    Text betText;

    [SerializeField]
    int maxBet = 1000;

    [SerializeField]
    int minBet = 50;

    int bet = 100;
    public int Bet
    {
        get => bet;
        set
        {
            bet = value;
            betText.text = $"${bet:n0}";
        }
    }

    public override void GameStart()
    {
        base.GameStart();
        ThrowBet();
        ShowCharacterCards();
        ShowCharacterScore();
    }
    public void Hit()
    {
        AddCharacterCards(trumpController.DrawCard());
        ShowCharacterCards();
        ShowCharacterScore();

        if (GetScore() == 0)
        {
            gameDirector.StandButtonOnly();
        }
    }
    public void DoubleDown()
    {
        ThrowBet();
        Hit();
        gameDirector.StandButtonOnly();
    }
    public void Surrender()
    {
        gameDirector.ResetField();
    }
    public void BetUpDown(bool IsUpDown)
    {
        if (IsUpDown)
        {
            if (Bet < Mathf.Min(Money, maxBet))
            {
                Bet += minBet;
            }
        }
        else
        {
            if (Bet > minBet)
            {
                Bet -= minBet;
            }
        }
    }
    void ThrowBet()
    {
        Money -= Bet;
        gameDirector.Bet += Bet;
    }
}
