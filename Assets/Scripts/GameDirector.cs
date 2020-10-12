using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    TrumpController trumpController;

    Button button;

    [SerializeField]
    GameObject playerGameObject;

    [SerializeField]
    GameObject dealerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        trumpController = GetComponent<TrumpController>();
        button = GetComponent<Button>();

        SpriteRenderer[] playerCards =
            playerGameObject.GetComponentsInChildren<SpriteRenderer>().ToArray();
        SpriteRenderer[] dealerCards =
            dealerGameObject.GetComponentsInChildren<SpriteRenderer>().ToArray();

        playerCards[0].sprite = trumpController.DrawCard().Sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
