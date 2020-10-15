using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    Button startButton;

    [SerializeField]
    Button standButton;

    [SerializeField]
    Button hitButton;

    [SerializeField]
    Button doubleDownButton;

    [SerializeField]
    Button surrenderButton;

    [SerializeField]
    GameDirector gameDirector;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    DealerController dealerController;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartButton);
        standButton.onClick.AddListener(StandButton);
        hitButton.onClick.AddListener(HitButton);
        doubleDownButton.onClick.AddListener(DoubleDownButton);
        surrenderButton.onClick.AddListener(SurrenderButton);
    }
    void StartButton()
    {
        gameDirector.GameStart();
    }
    void StandButton()
    {
        playerController.Stand();
    }
    void HitButton()
    {
        playerController.Hit();
    }
    void DoubleDownButton()
    {
        playerController.DoubleDown();
    }
    void SurrenderButton()
    {
        playerController.Surrender();
    }
}
