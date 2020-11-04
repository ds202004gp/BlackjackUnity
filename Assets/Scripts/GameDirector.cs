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

    public JudgeEnum judgeEnum;
    public enum JudgeEnum
    {
        win,
        draw,
        lose,
    }
    void Judgement()
    {
        if (playerController.IsSurrender)
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

    bool IsGameFollow()
    {
        return playerController.Money + dealerController.Dividend >= minBet;
    }
    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void Stand()
    {
        if (!playerController.IsSurrender)
        {
            dealerController.DrawDealer();
        }

        Judgement();
        dealerController.DividendResult(judgeEnum);

        if (!IsGameFollow())
        {
            GameOver();
        }
    }

    public void ResetField()
    {
        dealerController.DividendToPlayer();

        judgementText.text = "";

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();
    }
}
