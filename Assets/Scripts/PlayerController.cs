﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharacterBase
{
    [SerializeField]
    Text moneyText;
    int money;

    public int Money
    {
        get => money;

        set
        {
            money = value;
            moneyText.text = Money.ToString();
        }
    }

    [SerializeField]
    Text betText;
    int bet;

    protected override void Awake()
    {
        base.Awake();
        Money = int.Parse(moneyText.text);
        bet = int.Parse(betText.text);
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
            if (bet < 5000)
            {
                bet += 500;
            }
        }
        else
        {
            if (bet > 500)
            {
                bet -= 500;
            }
        }
        betText.text = bet.ToString();

    }
    void ThrowBet()
    {
        Money -= bet;
        gameDirector.Bet += bet;
    }
}
