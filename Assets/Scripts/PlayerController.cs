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
            if (value <= 0)
            {
                money = 0;
            }
            else if (value >= 1000000000)
            {
                money = 999999999;
            }
            else
            {
                money = value;
            }
            moneyText.text = $"${money:n0}";
        }
    }

    [SerializeField]
    Text betText;

    int maxBet;
    public int MaxBet { set => maxBet = value; }

    int minBet;
    public int MinBet { set => minBet = value; }

    [SerializeField]
    int bet;
    int Bet
    {
        get => bet;
        set
        {
            bet = value;
            betText.text = $"${bet:n0}";
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Bet = bet;
        Money = money;
    }
    public override void GameStart()
    {
        base.GameStart();
        ThrowBet();
        ShowCharacterCards();
        ShowCharacterScore();
    }
    public override void ResetCardsInfo()
    {
        base.ResetCardsInfo();

        while (money < bet)
        {
            Bet -= minBet;
        }
    }
    public void Hit()
    {
        AddCharacterCards(trumpController.DrawCard());
        ShowCharacterCards();
        ShowCharacterScore();
    }
    public void DoubleDown()
    {
        ThrowBet();
        Hit();
    }
    public void Surrender()
    {
        gameDirector.isSrrender = true;
        gameDirector.Stand();
    }
    public void BetUpDown(bool IsUpDown)
    {
        if (IsUpDown)
        {
            if (bet + minBet <= Mathf.Min(money, maxBet))
            {
                Bet += minBet;
            }
        }
        else
        {
            if (bet > minBet)
            {
                Bet -= minBet;
            }
        }
    }
    void ThrowBet()
    {
        Money -= bet;
        gameDirector.Bet += bet;
    }
}
