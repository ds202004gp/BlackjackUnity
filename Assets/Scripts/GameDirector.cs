using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    DealerController dealerController;

    TrumpController trumpController;

    [SerializeField]
    Text judgementText;

    [SerializeField]
    Text plusMoneyText;

    [SerializeField]
    int maxBet;
    int MaxBet
    {
        set
        {
            maxBet = value;
            playerController.MaxBet = value;
        }
    }

    [SerializeField]
    int minBet;
    int MinBet
    {
        set
        {
            minBet = value;
            playerController.MinBet = value;
        }
    }

    int bet;
    public int Bet { get => bet; set => bet = value; }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        MaxBet = maxBet;
        MinBet = minBet;
        trumpController = GetComponent<TrumpController>();
        ResetField();
    }

    public void GameStart()
    {
        playerController.GameStart();
        dealerController.GameStart();
    }

    JudgeEnum judgeEnum;
    enum JudgeEnum
    {
        win,
        draw,
        lose,
    }

    bool isSurrender;
    public bool IsSurrender { set => isSurrender = value; }

    void Judgement()
    {
        if (isSurrender)
        {
            judgeEnum = JudgeEnum.lose;
            judgementText.text = "SURRENDER";
            judgementText.color = Color.blue;
            return;
        }

        int playerScore = playerController.GetScore();
        int dealerScore = dealerController.GetScore();

        if (playerScore > dealerScore)
        {
            judgeEnum = JudgeEnum.win;
            judgementText.text = "WIN!!";
            judgementText.color = Color.yellow;
        }
        else if (playerScore == dealerScore)
        {
            judgeEnum = JudgeEnum.draw;
            judgementText.text = "DRAW";
            judgementText.color = Color.grey;
        }
        else if (playerScore < dealerScore)
        {
            judgeEnum = JudgeEnum.lose;
            judgementText.text = "LOSE...";
            judgementText.color = Color.blue;
        }
    }
    int dividend;
    void Dividend(int bet)
    {
        switch (judgeEnum)
        {
            case JudgeEnum.win:

                if (playerController.IsBlackjack)
                {
                    dividend = (int)(bet * 2.5);
                }
                else
                {
                    dividend = bet * 2;
                }

                plusMoneyText.color = Color.yellow;
                break;

            case JudgeEnum.draw:

                dividend = bet;
                plusMoneyText.color = Color.gray;
                break;

            case JudgeEnum.lose:

                if (isSurrender)
                {
                    dividend = bet / 2;
                    plusMoneyText.color = Color.blue;
                    break;
                }

                dividend = 0;
                return;
        }

        plusMoneyText.text = $"+ ${dividend}";
    }
    bool IsGameFollow()
    {
        return !(playerController.Money < minBet && judgeEnum == JudgeEnum.lose);
    }
    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void Stand()
    {
        if (!isSurrender)
        {
            dealerController.DrawDealer();
        }

        Judgement();
        Dividend(bet);

        if (!IsGameFollow())
        {
            GameOver();
        }
    }

    public void ResetField()
    {
        playerController.Money += dividend;
        bet = 0;

        isSurrender = false;

        judgementText.text = "";
        plusMoneyText.text = "";

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();
    }
}
