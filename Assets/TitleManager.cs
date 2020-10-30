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
    // Start is called before the first frame update
    void Start()
    {
        gotoMainButton.onClick.AddListener(GotoMain);
    }

    void GotoMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
