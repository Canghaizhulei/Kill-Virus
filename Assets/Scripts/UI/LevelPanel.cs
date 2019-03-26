using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelPanel : BasePanel
{

    private Text preLevelTxt;
    private Text curLevelTxt;
    private Text nexLevelTxt;

    public override void OnInit()
    {
        base.OnInit();
        preLevelTxt = transform.Find("Pre/Text").GetComponent<Text>();
        curLevelTxt = transform.Find("Cur/Text").GetComponent<Text>();
        nexLevelTxt = transform.Find("Next/Text").GetComponent<Text>();
        PlayerController.GetInstance.PlayerData.OnLevelChange += UpdateLevel;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        UpdateLevel(PlayerController.GetInstance.PlayerData.Level);
    }
    void UpdateLevel(ulong level)
    {
        if (level == 1)
        {
            preLevelTxt.transform.parent.gameObject.SetActive(false);

        }
        else
        {
            preLevelTxt.transform.parent.gameObject.SetActive(transform);
        }
        preLevelTxt.text = ToString(level - 1);
        curLevelTxt.text = ToString(level);
        nexLevelTxt.text = ToString(level + 1);
    }

    string ToString(ulong num)
    {
        if (num > 999)
            return "999+";
        return num.ToString();
    }
}
