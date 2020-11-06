using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharacterBase
{
    DealerController dealerController;

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

    [SerializeField]
    int bet;
    public int Bet
    {
        get => bet;
        set
        {
            bet = value;
            betText.text = $"${bet:n0}";
        }
    }

    int maxBet;
    public int MaxBet { set => maxBet = value; }

    int minBet;
    public int MinBet { set => minBet = value; }

    protected override void Awake()
    {
        base.Awake();
        dealerController = characterController.GetComponent<DealerController>();
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
        isSurrender = false;
        maxBet = Mathf.Min(money, maxBet);

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
    bool isSurrender;
    public bool IsSurrender { get => isSurrender; }
    public void Surrender()
    {
        isSurrender = true;
        gameDirector.Stand();
    }

    bool canUp;
    public bool CanUp { get => bet + minBet <= maxBet; }
    bool canDown;
    public bool CanDown { get => bet > minBet; }
    public void BetUp()
    {
        Bet += minBet;
    }
    public void BetDown()
    {
        Bet -= minBet;
    }
    void ThrowBet()
    {
        Money -= bet;
        dealerController.Bet += bet;
    }
}
