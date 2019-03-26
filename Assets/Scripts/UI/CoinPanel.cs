using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinPanel : BasePanel
{

    public Text CoinTxt;
    public Button AddCoinButton;
    public override void OnInit()
    {
        base.OnInit();
        CoinTxt = transform.Find("CoinTxt").GetComponent<Text>();
        AddCoinButton = transform.Find("AddCoinBtn").GetComponent<Button>();
        PlayerController.GetInstance.PlayerData.OnCoinChange += UpdateCoin;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UpdateCoin(PlayerController .GetInstance .PlayerData .Coin);
    }

    void UpdateCoin(ulong coin)
    {
        CoinTxt.text = Util.ToString(coin);
    }

}
