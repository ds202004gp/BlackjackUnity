using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrumpGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject playerCardsObject;

    [SerializeField]
    GameObject dealerCardsObject;

    TrumpController trumpController;
    // Start is called before the first frame update
    void Start()
    {
        trumpController = GetComponent<TrumpController>();

        SpriteRenderer[] playerCards =
            playerCardsObject.GetComponentsInChildren<SpriteRenderer>().ToArray();
        SpriteRenderer[] dealerCards =
            dealerCardsObject.GetComponentsInChildren<SpriteRenderer>().ToArray();

        // playerCards[0].sprite = trumpController.DrawCard();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
