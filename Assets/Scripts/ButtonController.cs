using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    Button gotoVipRoomButton;

    [SerializeField]
    Button gotoTitleButton;

    [SerializeField]
    Button betUpButton;

    [SerializeField]
    Button betDownButton;

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

    private void OnEnable()
    {
        gotoVipRoomButton.onClick.AddListener(GotoVipRoom);
        gotoTitleButton.onClick.AddListener(GotoTitleButton);
        betUpButton.onClick.AddListener(BetUpButton);
        betDownButton.onClick.AddListener(BetDownButton);
        startButton.onClick.AddListener(StartButton);
        standButton.onClick.AddListener(StandButton);
        retryButton.onClick.AddListener(RetryButton);
        hitButton.onClick.AddListener(HitButton);
        doubleDownButton.onClick.AddListener(DoubleDownButton);
        surrenderButton.onClick.AddListener(SurrenderButton);

        StartWindowButtons();
    }

    void GotoVipRoom()
    {
        SceneManager.LoadScene("GotoVipRoomScene");
    }
    void GotoTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
    void BetUpButton()
    {
        playerController.BetUp();
        CanBetUpDown();
    }
    void BetDownButton()
    {
        playerController.BetDown();
        CanBetUpDown();
    }
    void CanBetUpDown()
    {
        betUpButton.gameObject.SetActive(playerController.CanUp);
        betDownButton.gameObject.SetActive(playerController.CanDown);
    }

    bool canDoubleDown => playerController.Money - dealerController.Bet >= 0;

    void StartButton()
    {
        gameDirector.GameStart();

        startButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        surrenderButton.gameObject.SetActive(true);
        doubleDownButton.gameObject.SetActive(canDoubleDown);
        betUpButton.gameObject.SetActive(false);
        betDownButton.gameObject.SetActive(false);
    }
    void StandButton()
    {
        gameDirector.Stand();

        ResultWindowButtons();
    }
    void RetryButton()
    {
        gameDirector.ResetField();

        StartWindowButtons();
    }
    void HitButton()
    {
        playerController.Hit();

        hitButton.gameObject.SetActive(!playerController.IsBust);
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

        gameDirector.Stand();
        ResultWindowButtons();
    }

    void StartWindowButtons()
    {
        startButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        surrenderButton.gameObject.SetActive(false);
        doubleDownButton.gameObject.SetActive(false);
        CanBetUpDown();
    }

    void ResultWindowButtons()
    {
        startButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        surrenderButton.gameObject.SetActive(false);
        doubleDownButton.gameObject.SetActive(false);
        betUpButton.gameObject.SetActive(false);
        betDownButton.gameObject.SetActive(false);
    }
}
