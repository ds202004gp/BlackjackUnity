using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    [SerializeField]
    Sprite[] cards;

    public int number;
    public int mark;
    Sprite card;

    //tart is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = cards[0];

        card = cards[1];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
