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
    public Card DrawCard()
    {
        Card drawCard = trumpController.DrawCard();
        ConvertNumberForBlackjack(drawCard);
        return drawCard;
    }
    void ConvertNumberForBlackjack(Card card)
    {
        if (card.Number == 1) card.Number = 11;
        else if (card.Number > 10) card.Number = 10;
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
    void Judgement()
    {
        if (playerController.IsSurrender)
        {
            judgeEnum = JudgeEnum.Lose;
            judgementText.text = "SURRENDER";
            judgementText.color = Color.blue;
            return;
        }

        int playerScore = playerController.GetScore();
        int dealerScore = dealerController.GetScore();

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
    void DividendResult()
    {
        float dividendMultiplier = 0;

        switch (judgeEnum)
        {
            case JudgeEnum.Win:

                dividendMultiplier = playerController.IsBlackjack ? 2.5f : 2;
                dividendText.color = Color.yellow;
                break;

            case JudgeEnum.Draw:

                dividendMultiplier = 1;
                dividendText.color = Color.grey;
                break;

            case JudgeEnum.Lose:

                if (!playerController.IsSurrender) return;

                dividendMultiplier = 0.5f;
                dividendText.color = Color.blue;
                break;
        }

        dealerController.DividendMultiplier = dividendMultiplier;
        dividendText.text = $"+ ${dealerController.Dividend}";
    }

    int PlayersMoney => playerController.Money + dealerController.Dividend;
    bool IsGameOver => PlayersMoney < minBet;
    bool IsGameClear => PlayersMoney >= gameClearMoney;
    void GameOver()
    {
        stageEnum = StageEnum.Normal;
        gameOverPanel.SetActive(true);
    }
    void GameClear()
    {
        stageEnum = StageEnum.Vip;
        gameClearPanel.SetActive(true);
    }
    [SerializeField]
    int gameClearMoney = 10000;
    public void Stand()
    {
        if (!playerController.IsSurrender) dealerController.DrawDealer();

        Judgement();
        DividendResult();

        if (IsGameOver) GameOver();
        if (stageEnum == StageEnum.Vip) return;
        if (IsGameClear) GameClear();
    }
    public void BetToDealer(int bet)
    {
        playerController.Money -= bet;
        dealerController.Bet += bet;
    }
    void DividendToPlayer() => playerController.Money = PlayersMoney;
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
        switch (stageEnum)
        {
            case StageEnum.Normal:

                MinBet = minBet;
                MaxBet = maxBet;
                break;

            case StageEnum.Vip:

                MinBet = dealerController.Bet;
                MaxBet = minBet * 10;
                break;
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
