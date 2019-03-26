using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerPanel : BasePanel {

    public Text PowerTxt;
    public Button AddPowerButton;
    private Slider powerSlider;
    public override void OnInit()
    {
        base.OnInit();
        PowerTxt = transform.Find("PowerTxt").GetComponent<Text>();
        AddPowerButton = transform.Find("AddPowerBtn").GetComponent<Button>();
        powerSlider = transform.Find("PowerSlider").GetComponent<Slider>();
        PlayerController.GetInstance.PlayerData.OnEnergyChange += UpdatePower;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        UpdatePower(PlayerController.GetInstance.PlayerData.Energy);
    }
    void UpdatePower(int power)
    {
        PowerTxt.text =Util.ToString(power);
        powerSlider.value = power/100f;
    }
}
