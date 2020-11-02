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
        playerController.GameStart();
        dealerController.GameStart();
    }

    bool IsGameFollow()
    {
        return !(playerController.Money < minBet && judgeEnum == JudgeEnum.lose);
    }
    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public JudgeEnum judgeEnum;
    public enum JudgeEnum
    {
        win,
        draw,
        lose,
        surrender
    }
    public bool isSrrender;
    void Judgement()
    {
        if (judgeEnum == JudgeEnum.surrender)
        {
            judgementText.text = "SURRENDER";
            judgementText.color = Color.blue;
            return;
        }

        int playerScore = playerController.GetScore();
        int dealerScore = dealerController.GetScore();

        if (playerScore > dealerScore)
        {
            judgementText.text = "WIN!!";
            judgementText.color = Color.yellow;
            judgeEnum = JudgeEnum.win;
        }
        else if (playerScore == dealerScore)
        {
            judgementText.text = "DRAW";
            judgementText.color = Color.grey;
            judgeEnum = JudgeEnum.draw;
        }
        else if (playerScore < dealerScore)
        {
            judgementText.text = "LOSE...";
            judgementText.color = Color.blue;
            judgeEnum = JudgeEnum.lose;
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

                dividend = 0;
                return;

            case JudgeEnum.surrender:

                dividend = bet / 2;
                plusMoneyText.color = Color.blue;
                break;
        }

        plusMoneyText.text = $"+ ${dividend}";
    }
    public void Stand()
    {
        if (!(judgeEnum == JudgeEnum.surrender))
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

        judgeEnum = JudgeEnum.draw;

        judgementText.text = "";
        plusMoneyText.text = "";

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();
    }
}
