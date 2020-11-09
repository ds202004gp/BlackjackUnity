using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    GameObject gameClearPanel;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    DealerController dealerController;

    TrumpController trumpController;

    ButtonController buttonController;

    [SerializeField]
    Text judgementText;

    [SerializeField]
    Text dividendText;

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

    [SerializeField]
    int defaultPlayersMoney;

    void Start()
    {
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        trumpController = GetComponent<TrumpController>();
        buttonController = GetComponent<ButtonController>();

        if (stageEnum == StageEnum.Normal)
            playerController.Money = defaultPlayersMoney;

        playerController.Bet = minBet;

        BackGround();
        ResetField();

        buttonController.enabled = true;
    }

    public void GameStart()
    {
        playerController.GameStart();
        dealerController.GameStart();
    }

    public JudgeEnum judgeEnum;
    public enum JudgeEnum
    {
        Win,
        Draw,
        Lose,
    }
    int playerScore;
    int dealerScore;
    void Judgement()
    {
        if (playerController.IsSurrender)
        {
            judgeEnum = JudgeEnum.Lose;
            judgementText.text = "SURRENDER";
            judgementText.color = Color.blue;
            return;
        }

        playerScore = playerController.GetScore();
        dealerScore = dealerController.GetScore();

        if (playerScore > dealerScore)
        {
            judgeEnum = JudgeEnum.Win;
            judgementText.text = "WIN!!";
            judgementText.color = Color.yellow;
        }
        else if (playerScore == dealerScore)
        {
            judgeEnum = JudgeEnum.Draw;
            judgementText.text = "DRAW";
            judgementText.color = Color.grey;
        }
        else if (playerScore < dealerScore)
        {
            judgeEnum = JudgeEnum.Lose;
            judgementText.text = "LOSE...";
            judgementText.color = Color.blue;
        }
    }
    float dividendMultiplier;
    void DividendResult()
    {
        switch (judgeEnum)
        {
            case JudgeEnum.Win:

                if (playerController.IsBlackjack)
                {
                    dividendMultiplier = 2.5f;
                }
                else
                {
                    dividendMultiplier = 2;
                }

                dividendText.color = Color.yellow;
                break;

            case JudgeEnum.Draw:

                dividendMultiplier = 1;
                dividendText.color = Color.grey;
                break;

            case JudgeEnum.Lose:

                if (!playerController.IsSurrender)
                {
                    return;
                }

                dividendMultiplier = 0.5f;
                dividendText.color = Color.blue;
                break;
        }

        dealerController.DividendMultiplier = dividendMultiplier;
        dividendText.text = $"+ ${dealerController.Dividend}";
    }

    int playersMoney;
    bool IsGameFollow()
    {
        playersMoney = playerController.Money + dealerController.Dividend;
        return playersMoney >= minBet;
    }
    void GameOver()
    {
        stageEnum = StageEnum.Normal;
        gameOverPanel.SetActive(true);
    }
    [SerializeField]
    int gameClearMoney = 10000;
    public void Stand()
    {
        if (!playerController.IsSurrender)
        {
            dealerController.DrawDealer();
        }

        Judgement();
        DividendResult();

        if (!IsGameFollow())
        {
            GameOver();
        }

        if (stageEnum == StageEnum.Vip)
        {
            return;
        }

        if (playersMoney >= gameClearMoney)
        {
            stageEnum = StageEnum.Vip;
            gameClearPanel.SetActive(true);
        }
    }
    void DividendToPlayer()
    {
        playerController.Money += dealerController.Dividend;
    }
    public void BetToDealer(int bet)
    {
        playerController.Money -= bet;
        dealerController.Bet += bet;
    }
    public void ResetField()
    {
        DividendToPlayer();
        BetLimit();

        judgementText.text = "";
        dividendText.text = "";

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();
    }
    static StageEnum stageEnum;
    enum StageEnum
    {
        Normal,
        Vip,
    }
    [SerializeField]
    GameObject normalBackGround;

    [SerializeField]
    GameObject vipBackGround;
    void BackGround()
    {
        normalBackGround.SetActive(stageEnum == StageEnum.Normal);
        vipBackGround.SetActive(stageEnum == StageEnum.Vip);
    }
    void BetLimit()
    {
        if (stageEnum == StageEnum.Normal)
        {
            MinBet = minBet;
            MaxBet = maxBet;
        }
        else if (stageEnum == StageEnum.Vip)
        {
            MinBet = dealerController.Bet;
            MaxBet = minBet * 10;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
