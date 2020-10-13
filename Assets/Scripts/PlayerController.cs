using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CharacterBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        characterName = "PLAYER";
        base.Start();

        characterCards =
            GetComponentsInChildren<SpriteRenderer>().ToArray();
    }
}
