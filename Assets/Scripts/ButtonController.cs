using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    Button gotoTitleButton;

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
        gotoTitleButton.onClick.AddListener(GotoTitleButton);
        upButton.onClick.AddListener(UpButton);
        downButton.onClick.AddListener(DownButton);
        startButton.onClick.AddListener(StartButton);
        standButton.onClick.AddListener(StandButton);
        retryButton.onClick.AddListener(RetryButton);
        hitButton.onClick.AddListener(HitButton);
        doubleDownButton.onClick.AddListener(DoubleDownButton);
        surrenderButton.onClick.AddListener(SurrenderButton);

        startButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        surrenderButton.gameObject.SetActive(false);
        doubleDownButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(true);
        downButton.gameObject.SetActive(true);
    }
    void GotoTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
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

        startButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        surrenderButton.gameObject.SetActive(true);

        if (playerController.Money - gameDirector.Bet >= 0)
        {
            doubleDownButton.gameObject.SetActive(true);
        }
        else
        {
            doubleDownButton.gameObject.SetActive(false);
        }

        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
    }
    void StandButton()
    {
        gameDirector.Stand();

        startButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        surrenderButton.gameObject.SetActive(false);
        doubleDownButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
    }
    void RetryButton()
    {
        gameDirector.ResetField();

        startButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        surrenderButton.gameObject.SetActive(false);
        doubleDownButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(true);
        downButton.gameObject.SetActive(true);
    }
    void HitButton()
    {
        playerController.Hit();

        if (playerController.GetScore() == 0)
        {
            hitButton.gameObject.SetActive(false);
        }

        surrenderButton.gameObject.SetActive(false);
        doubleDownButton.gameObject.SetActive(false);
    }
    void DoubleDownButton()
    {
        playerController.DoubleDown();

        hitButton.gameObject.SetActive(false);
        surrenderButton.gameObject.SetActive(false);
        doubleDownButton.gameObject.SetActive(false);
    }
    void SurrenderButton()
    {
        playerController.Surrender();

        startButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        surrenderButton.gameObject.SetActive(false);
        doubleDownButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
    }
}
