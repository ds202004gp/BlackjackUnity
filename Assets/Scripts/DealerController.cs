using UnityEngine;
using UnityEngine.UI;

public class DealerController : CharacterBase
{
    static int bet;
    public int Bet { get => bet; set => bet = value; }

    public float DividendMultiplier { private get; set; }
    public int Dividend => (int)(bet * DividendMultiplier);

    public override void GameStart()
    {
        base.GameStart();
        UpCard();
    }
    public override void ResetCardsInfo()
    {
        base.ResetCardsInfo();
        bet = 0;
        DividendMultiplier = 0;
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

            if (characterScore >= 17) break;

            AddCharacterCards(trumpController.DrawCard());
        }
    }
}
