using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    GameObject startButton;

    [SerializeField]
    GameObject standButton;

    [SerializeField]
    GameObject retryButton;

    [SerializeField]
    GameObject playerButtons;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    DealerController dealerController;

    TrumpController trumpController;
    // Start is called before the first frame update
    void Start()
    {
        trumpController = GetComponent<TrumpController>();
        ResetField();
    }
    public void GameStart()
    {
        playerController.GameStart();
        dealerController.GameStart();

        startButton.SetActive(false);
        standButton.SetActive(true);
        retryButton.SetActive(false);
        playerButtons.SetActive(true);
    }
    public void Stand()
    {
        dealerController.DrawDealer();

        startButton.SetActive(false);
        standButton.SetActive(false);
        retryButton.SetActive(true);
        playerButtons.SetActive(false);
    }
    public void ResetField()
    {
        startButton.SetActive(true);
        standButton.SetActive(false);
        retryButton.SetActive(false);
        playerButtons.SetActive(false);

        trumpController.ResetCardsInfo();
        playerController.ResetCardsInfo();
        dealerController.ResetCardsInfo();
    }
    public void Judgement()
    {

    }
}
