using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealerController : CharacterBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        characterName = "DEALER";
        base.Start();

        characterCards =
             GetComponentsInChildren<SpriteRenderer>().ToArray();
    }
    public override void ShowDrawCards()
    {
        characterCards[0].sprite = drawCards[0].Sprite;
    }
}
