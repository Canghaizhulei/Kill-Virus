using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BgPanel : BasePanel
{

    Button BgBtn;

    public override void OnInit()
    {
        base.OnInit();
        BgBtn = transform.GetComponent<Button>();
        BgBtn.onClick .AddListener(()=>
        {
            MessageManager .GetInstance .Dispatch(MessageDefine.OnBgBtnAdd);
        });
    }
}