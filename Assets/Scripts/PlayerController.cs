using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CharacterBase
{
    SpriteRenderer[] playerCards;

    // Start is called before the first frame update
    protected override void Start()
    {
        characterName = "PLAYER";
        base.Start();
        SpriteRenderer[] playerCards =
            GetComponentsInChildren<SpriteRenderer>().ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
