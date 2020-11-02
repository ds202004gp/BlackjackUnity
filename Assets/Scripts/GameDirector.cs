using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    GameObject startButton;

    [SerializeField]
    GameObject standButton;

    [SerializeField]
    GameObject retryButton;

    [SerializeField]
    GameObject playerButtons;

    [SerializeField]
    GameObject doubleDownButton;

    [SerializeField]
    GameObject betButtons;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    DealerController dealerController;

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
    TrumpController trumpController;

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
        bet = 0;
        playerController.GameStart();
        dealerController.GameStart();

        startButton.SetActive(false);
        standButton.SetActive(true);
        retryButton.SetActive(false);
        playerButtons.SetActive(true);
        betButtons.SetActive(false);

        if (playerController.Money - bet < 0)
        {
            doubleDownButton.SetActive(false);
        }
    }
    public void Stand()
    {
        if (!isSrrender)
        {
            dealerController.DrawDealer();
        }
        Judgement();

        if (IsGameFollow())
        {
            startButton.SetActive(false);
            standButton.SetActive(false);
            retryButton.SetActive(true);
            playerButtons.SetActive(false);
        }
        else
        {
            GameOver();
        }
    }

    bool IsGameFollow()
    {
        return !(playerController.Money < minBet && isLose);
    }
    void GameOver()
    {
        gameOverPanel.SetActive(true);

        startButton.SetActive(false);
        standButton.SetActive(false);
        retryButton.SetActive(false);
        playerButtons.SetActive(false);
        betButtons.SetActive(false);
    }
    public void ResetField()
    {
        playerController.Money += dividend;

        isWin = false;
        isDraw = false;
        isLose = false;
        isSrrender = false;

        startButton.SetActive(true);
        standButton.SetActive(false);
        retryButton.SetActive(false);
        playerButtons.SetActive(false);
        betButtons.SetActive(true);

        judgementText.text = "";
        plusMoneyText.text = "";

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();
    }

    bool isWin;
    bool isDraw;
    bool isLose;
    public bool isSrrender;
    void Judgement()
    {
        if (isSrrender)
        {
            judgementText.text = "SURRENDER";
            judgementText.color = Color.blue;
            Dividend(bet);
            return;
        }

        int playerScore = playerController.GetScore();
        int dealerScore = dealerController.GetScore();

        if (playerScore > dealerScore)
        {
            judgementText.text = "WIN!!";
            judgementText.color = Color.yellow;
            isWin = true;
        }
        else if (playerScore == dealerScore)
        {
            judgementText.text = "DRAW";
            judgementText.color = Color.grey;
            isDraw = true;
        }
        else if (playerScore < dealerScore)
        {
            judgementText.text = "LOSE...";
            judgementText.color = Color.blue;
            isLose = true;
        }
        Dividend(bet);
    }

    int dividend;
    void Dividend(int bet)
    {
        if (isWin)
        {
            if (playerController.IsBlackjack)
            {
                dividend = (int)(bet * 2.5);
            }
            else
            {
                dividend = bet * 2;
            }
            plusMoneyText.color = Color.yellow;
        }
        else if (isDraw)
        {
            dividend = bet;
            plusMoneyText.color = Color.gray;
        }
        else if (isLose)
        {
            dividend = 0;
            return;
        }
        else
        {
            dividend = bet / 2;
            plusMoneyText.color = Color.blue;
        }
        plusMoneyText.text = $"+ ${dividend}";
    }

    public void StandButtonOnly()
    {
        startButton.SetActive(false);
        standButton.SetActive(true);
        retryButton.SetActive(false);
        playerButtons.SetActive(false);
    }
}
