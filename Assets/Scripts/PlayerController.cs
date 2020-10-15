using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CharacterBase
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
        ShowCharacterCards();
        ShowCharacterScore();
    }
    public void Hit()
    {
        AddCharacterCards(trumpController.DrawCard());
        ShowCharacterCards();
        ShowCharacterScore();
    }
    public void DoubleDown()
    {

    }
    public void Surrender()
    {

    }
    public void Stand()
    {

    }
}
