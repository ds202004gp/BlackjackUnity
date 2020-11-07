using UnityEngine;
using UnityEngine.UI;

public class DealerController : CharacterBase
{
    PlayerController playerController;

    [SerializeField]
    Text dividendText;

    static int bet;
    public int Bet { get => bet; set => bet = value; }

    protected override void Awake()
    {
        base.Awake();
        playerController = characterController.GetComponent<PlayerController>();
    }
    public override void GameStart()
    {
        base.GameStart();
        UpCard();
    }
    public override void ResetCardsInfo()
    {
        base.ResetCardsInfo();
        bet = 0;
        dividendText.text = "";
    }
    void UpCard()
    {
        characterCardSprites[0].sprite = characterCards[0].Sprite;
        characterScore = characterCards[0].Number;
        scoreText.text = characterScore.ToString();
    }
    public void DrawDealer()
    {
        while (true)
        {
            ShowCharacterCards();
            ShowCharacterScore();

            if (characterScore >= 17)
            {
                break;
            }

            AddCharacterCards(trumpController.DrawCard());
        }
    }

    int dividend;
    public int Dividend { get => dividend; }

    public void DividendToPlayer()
    {
        playerController.Money += dividend;
    }
    public void DividendResult(GameDirector.JudgeEnum judgeEnum)
    {
        switch (judgeEnum)
        {
            case GameDirector.JudgeEnum.Win:

                if (playerController.IsBlackjack)
                {
                    dividend = (int)(bet * 2.5);
                }
                else
                {
                    dividend = bet * 2;
                }

                dividendText.color = Color.yellow;
                break;

            case GameDirector.JudgeEnum.Draw:

                dividend = bet;
                dividendText.color = Color.gray;
                break;

            case GameDirector.JudgeEnum.Lose:

                if (playerController.IsSurrender)
                {
                    dividend = bet / 2;
                    dividendText.color = Color.blue;
                    break;
                }

                dividend = 0;
                return;
        }

        dividendText.text = $"+ ${dividend}";
    }
}
