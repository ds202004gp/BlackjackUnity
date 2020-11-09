using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharacterBase
{
    [SerializeField]
    Text moneyText;

    static int money;
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

    public int MaxBet { private get; set; }
    public int MinBet { private get; set; }

    protected override void Awake()
    {
        base.Awake();
        Money = money;
    }
    public override void GameStart()
    {
        base.GameStart();
        gameDirector.BetToDealer(bet);
        ShowCharacterCards();
        ShowCharacterScore();
    }
    public override void ResetCardsInfo()
    {
        base.ResetCardsInfo();
        IsSurrender = false;
        MaxBet = Mathf.Min(money, MaxBet);

        while (money < bet)
        {
            Bet -= MinBet;
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
        gameDirector.BetToDealer(bet);
        Hit();
    }
    public bool IsSurrender { get; private set; }
    public void Surrender()
    {
        IsSurrender = true;
    }

    public bool CanUp { get => bet + MinBet <= MaxBet; }
    public bool CanDown { get => bet > MinBet; }
    public void BetUp()
    {
        Bet += MinBet;
    }
    public void BetDown()
    {
        Bet -= MinBet;
    }
}
