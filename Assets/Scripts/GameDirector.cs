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

        startButton.SetActive(false);
        standButton.SetActive(false);
        retryButton.SetActive(true);
        playerButtons.SetActive(false);
    }
    public void ResetField()
    {
        startButton.SetActive(true);
        standButton.SetActive(false);
        retryButton.SetActive(false);
        playerButtons.SetActive(false);
        betButtons.SetActive(true);

        judgementText.gameObject.SetActive(false);

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();

        playerController.ShowMoney();
    }

    bool isWin;
    public void Judgement()
    {
        isWin = false;

        judgementText.gameObject.SetActive(true);

        int playerScore = playerController.GetScore();
        int dealerScore = dealerController.GetScore();

        if (playerScore > dealerScore)
        {
            judgementText.text = "WIN!";
            judgementText.color = Color.yellow;
            isWin = true;
            Dividend(Bet);
        }
        else if (playerScore == dealerScore)
        {
            judgementText.text = "DRAW";
            judgementText.color = Color.white;
            Dividend(bet);
        }
        else if (playerScore < dealerScore)
        {
            judgementText.text = "LOSE...";
            judgementText.color = Color.blue;
        }
    }

    void Dividend(int dividend)
    {
        if (isWin)
        {
            dividend *= 2;
            playerController.Money += dividend;
        }
        else
        {
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
