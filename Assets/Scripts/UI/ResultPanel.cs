using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultPanel : BasePanel
{

    private Image bg;

    private GameObject header;
    private GameObject end;
    private Slider leftSlider;
    private Text coinTxt1;
    private Text coinTxt2;
    private Text leftEnemyTxt;
    private Button getCoinBtn;

    private Transform coinImage;
    public override void OnInit()
    {
        base.OnInit();
        bg = GetComponent<Image>();
        header = transform.Find("Header").gameObject;
        end = transform.Find("End").gameObject;
        leftEnemyTxt = transform.Find("Header/Left").GetComponent<Text>();
        coinTxt1 = transform.Find("Header/GetCoin/Text").GetComponent<Text>();
        coinTxt2 = transform.Find("End/Get/Text").GetComponent<Text>();
        leftSlider = transform.Find("Header/Slider").GetComponent<Slider>();
        getCoinBtn = transform.Find("End/Get/GetCoinBtn").GetComponent<Button>();
        getCoinBtn.onClick.AddListener(OnClickGetCoinBtn);
        coinImage = transform.Find("Header/GetCoin");

        PlayerController.GetInstance.LevelData.OnLeftValueChange += OnLeftEnemyChange;
        PlayerController.GetInstance.LevelData.OnGetCoinValueChange += OnGetCoinChange;

        MessageManager .GetInstance .AddListener<bool>(MessageDefine .OnLevelEnd ,OnLevelEnd);
       
        Coin.Target = coinImage.transform.position;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Color c = Color.white;
        c.a = 0;
        bg.color = c;
        header.SetActive(true);
        end.SetActive(false);

        OnLeftEnemyChange(1);
        OnGetCoinChange(0);
    }

    void OnLeftEnemyChange(float left)
    {
        leftSlider.value = left;
        leftEnemyTxt.text = string.Format("剩余病毒：{0}%",(int) (left*100));
    }

    void OnGetCoinChange(ulong coin)
    {
        coinTxt1.text = Util.ToString(coin);
        coinTxt2.text = Util.ToString(coin);
    }

    void OnClickGetCoinBtn()
    {
        UIManager.Instance.ClosePannel(UIPanelType.Result);
        GameController .GetInstance .ReStart();

    }

    void OnLevelEnd(bool isWin)
    {
        bg .color = new Color(0,0,0,81/255f);
        end.SetActive(true);
        UIManager .Instance .OpenPannel(UIPanelType.Level);
    }
}
