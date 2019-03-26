using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GemPanel : BasePanel {

    public Text GemTxt;
    public Button AddGemBtn;
    public override void OnInit()
    {
        base.OnInit();
        GemTxt = transform.Find("GemTxt").GetComponent<Text>();
        AddGemBtn = transform.Find("AddGemBtn").GetComponent<Button>();
        PlayerController.GetInstance.PlayerData.OnDiamondChange += UpdateGem;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        UpdateGem(PlayerController.GetInstance.PlayerData.Diamond);
    }
    void UpdateGem(ulong gem)
    {
        GemTxt.text = Util.ToString(gem);
    }
}
