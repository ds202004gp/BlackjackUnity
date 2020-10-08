using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    Text nameText;
    protected string characterName;

    [SerializeField]
    Text scoreText;
    protected int characterScore;

    protected List<int> drawCards;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        nameText.text = characterName;
        scoreText.text = "---";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
