using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealerController : CharacterBase
{
    SpriteRenderer[] dealerCards;

    // Start is called before the first frame update
    protected override void Start()
    {
        characterName = "DEALER";
        base.Start();
        SpriteRenderer[] dealerCards =
            GetComponentsInChildren<SpriteRenderer>().ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
