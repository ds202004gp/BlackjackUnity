using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    Button gotoMainButton;

    [SerializeField]
    Button gameRuleButton;

    [SerializeField]
    Button backToTitleButton;

    [SerializeField]
    GameObject gameRulePanel;
    // Start is called before the first frame update
    void Start()
    {
        BackToTitle();
        gotoMainButton.onClick.AddListener(GotoMain);
        gameRuleButton.onClick.AddListener(GameRule);
        backToTitleButton.onClick.AddListener(BackToTitle);
    }

    void GotoMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    void GameRule()
    {
        gameRulePanel.SetActive(true);
    }
    void BackToTitle()
    {
        gameRulePanel.SetActive(false);
    }
}
