using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GotoVipRoomManager : MonoBehaviour
{
    [SerializeField]
    Button ruleDescriptionButton;

    [SerializeField]
    Button gotoVipRoomButton;

    [SerializeField]
    GameObject vipRoomText;

    [SerializeField]
    GameObject vipRoomRuleText;

    void Start()
    {
        ruleDescriptionButton.onClick.AddListener(RuleDescription);
        gotoVipRoomButton.onClick.AddListener(GotoVipRoom);
    }
    void RuleDescription()
    {
        vipRoomText.SetActive(false);
        vipRoomRuleText.SetActive(true);
    }
    void GotoVipRoom()
    {
        SceneManager.LoadScene("MainScene");
    }
}
