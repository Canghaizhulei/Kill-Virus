using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WarningPanel : BasePanel {

    public Text contentTxt;
    public override void OnInit()
    {
        base.OnInit();
        contentTxt = transform.Find("Text").GetComponent<Text>();
        MessageManager.GetInstance .AddListener<string>(MessageDefine .OpenWarning,OnOpen);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        CancelInvoke();
        Invoke("CloseSelf", 1f);
    }

    public void OnOpen(string content)
    {
        this.contentTxt.text = content;
        OnEnter();
    }

    void CloseSelf()
    {
        OnExit();
    }
}
