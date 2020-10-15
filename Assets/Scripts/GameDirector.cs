using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    Button startButton;

    [SerializeField]
    Button stayButton;

    [SerializeField]
    GameObject playerButtons;

    [SerializeField]
    GameObject playerGameObject;

    [SerializeField]
    GameObject dealerGameObject;

    PlayerController playerController;

    DealerController dealerController;

    TrumpController trumpController;

    SpriteRenderer[] characterCards;


    // Start is called before the first frame update
    void Start()
    {
        trumpController = GetComponent<TrumpController>();
        playerController = playerGameObject.GetComponent<PlayerController>();
        dealerController = dealerGameObject.GetComponent<DealerController>();
        characterCards = playerGameObject.GetComponentsInChildren<SpriteRenderer>().ToArray();

        stayButton.gameObject.SetActive(false);
        playerButtons.SetActive(false);
    }
    public void GameStart()
    {
        playerController.GameStart();
        dealerController.GameStart();

        startButton.gameObject.SetActive(false);
        stayButton.gameObject.SetActive(true);
        playerButtons.SetActive(true);
    }
    void Stay()
    {

    }
}
