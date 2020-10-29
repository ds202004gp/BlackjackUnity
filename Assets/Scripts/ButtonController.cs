using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    Button upButton;

    [SerializeField]
    Button downButton;

    [SerializeField]
    Button startButton;

    [SerializeField]
    Button standButton;

    [SerializeField]
    Button retryButton;

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
        upButton.onClick.AddListener(UpButton);
        downButton.onClick.AddListener(DownButton);
        startButton.onClick.AddListener(StartButton);
        standButton.onClick.AddListener(StandButton);
        retryButton.onClick.AddListener(RetryButton);
        hitButton.onClick.AddListener(HitButton);
        doubleDownButton.onClick.AddListener(DoubleDownButton);
        surrenderButton.onClick.AddListener(SurrenderButton);
    }
    void UpButton()
    {
        playerController.BetUpDown(true);
    }
    void DownButton()
    {
        playerController.BetUpDown(false);
    }
    void StartButton()
    {
        gameDirector.GameStart();
    }
    void StandButton()
    {
        gameDirector.Stand();
    }
    void RetryButton()
    {
        gameDirector.ResetField();
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
