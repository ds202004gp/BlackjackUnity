using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealerController : CharacterBase
{
    public override void GameStart()
    {
        base.GameStart();
        UpCard();
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
}
