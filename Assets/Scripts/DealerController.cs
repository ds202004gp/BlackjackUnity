using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealerController : CharacterBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        characterCardSprites =
             GetComponentsInChildren<SpriteRenderer>().ToArray();
    }
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
}
