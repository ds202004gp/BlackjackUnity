using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CharacterBase
{
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
        Hit();
    }
    public void Surrender()
    {

    }
}
