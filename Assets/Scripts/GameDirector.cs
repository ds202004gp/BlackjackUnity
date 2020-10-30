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
    GameObject betButtons;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    DealerController dealerController;

    [SerializeField]
    Text judgementText;

    TrumpController trumpController;

    int bet;
    public int Bet { get => bet; set => bet = value; }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        trumpController = GetComponent<TrumpController>();
        ResetField();
    }

    public void GameStart()
    {
        Bet = 0;
        playerController.GameStart();
        dealerController.GameStart();

        startButton.SetActive(false);
        standButton.SetActive(true);
        retryButton.SetActive(false);
        playerButtons.SetActive(true);
        betButtons.SetActive(false);
    }
    public void Stand()
    {
        dealerController.DrawDealer();
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
        return !(playerController.Money <= 0 && isLose);
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
        Dividend(Bet);

        isWin = false;
        isDraw = false;
        isLose = false;

        startButton.SetActive(true);
        standButton.SetActive(false);
        retryButton.SetActive(false);
        playerButtons.SetActive(false);
        betButtons.SetActive(true);

        judgementText.gameObject.SetActive(false);

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();
    }

    bool isWin;
    bool isDraw;
    bool isLose;
    void Judgement()
    {
        judgementText.gameObject.SetActive(true);

        int playerScore = playerController.GetScore();
        int dealerScore = dealerController.GetScore();

        if (playerScore > dealerScore)
        {
            judgementText.text = "WIN!";
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
    }

    void Dividend(int dividend)
    {
        if (isWin)
        {
            if (playerController.IsBlackjack)
            {
                dividend = (int)(dividend * 2.5);
                playerController.Money += dividend;
            }
            else
            {
                dividend *= 2;
                playerController.Money += dividend;
            }
        }
        else if (isDraw)
        {
            playerController.Money += dividend;
        }
        else if (isLose)
        {
            return;
        }
        else
        {
            dividend /= 2;
            playerController.Money += dividend;
        }
    }

    public void StandButtonOnly()
    {
        startButton.SetActive(false);
        standButton.SetActive(true);
        retryButton.SetActive(false);
        playerButtons.SetActive(false);
    }
}
