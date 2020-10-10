using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameDirector : MonoBehaviour
{
    TrumpController trumpController;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();

        trumpController = GetComponent<TrumpController>();

        SpriteRenderer[] playerCards =
            GetComponentsInChildren<SpriteRenderer>().ToArray();
        SpriteRenderer[] dealerCards =
            GetComponentsInChildren<SpriteRenderer>().ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
