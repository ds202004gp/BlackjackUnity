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
    void DividendResult()
    {
        float dividendMultiplier = 0;

        switch (judgeEnum)
        {
            case JudgeEnum.win:

                if (playerController.IsBlackjack)
                {
                    dividendMultiplier = 2.5f;
                }
                else
                {
                    dividendMultiplier = 2;
                }

                plusMoneyText.color = Color.yellow;
                break;

            case JudgeEnum.draw:

                dividendMultiplier = 1;
                plusMoneyText.color = Color.gray;
                break;

            case JudgeEnum.lose:

                if (isSurrender)
                {
                    dividendMultiplier = 0.5f;
                    plusMoneyText.color = Color.blue;
                    break;
                }

                isDividendNone = true;
                return;
        }

        plusMoneyText.text = $"+ ${dealerController.Dividend(dividendMultiplier)}";
    }
    bool isDividendNone;
    bool IsGameFollow()
    {
        if (isSurrender)
        {

        }
        return !(playerController.Money < minBet && isDividendNone);
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
        DividendResult();

        if (!IsGameFollow())
        {
            GameOver();
        }
    }

    public void ResetField()
    {
        dealerController.DividendToPlayer();

        isSurrender = false;
        isDividendNone = false;

        judgementText.text = "";
        plusMoneyText.text = "";

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();
    }
}
