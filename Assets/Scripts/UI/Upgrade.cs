using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Upgrade : BasePanel
{
    private ToggleGroup groups;
    private Toggle mainBtn;
    private Toggle secondBtn;
    private Toggle dayCoinBtn;
    private GameObject addPanel;
    private GameObject secondWeaponPanel;
    private Text property1Txt;
    private Text property2Txt;
    private Button addLevel1;
    private Button addLevel2;
    private Text addLevelCoin1;
    private Text addLevelCoin2;

    private ToggleGroup secondaryWeapGroup;

    private WeaponModel currentWeaponModel;

    private int currentClickToggle = -1; // 0 - main  1 - second  2 - day coin
    public override void OnInit()
    {
        base.OnInit();
        mainBtn = transform.Find("Main").GetComponent<Toggle>();
        secondBtn = transform.Find("Second").GetComponent<Toggle>();
        dayCoinBtn = transform.Find("DayCoin").GetComponent<Toggle>();

        addPanel = transform.Find("UpgradeWeapon").gameObject;
        secondWeaponPanel = transform.Find("SecondaryWeapon").gameObject;

        property1Txt = transform.Find("UpgradeWeapon/Property1").GetComponent<Text>();
        property2Txt = transform.Find("UpgradeWeapon/Property2").GetComponent<Text>();
        addLevel1 = transform.Find("UpgradeWeapon/AddLevel1").GetComponent<Button>();
        addLevel2= transform.Find("UpgradeWeapon/AddLevel2").GetComponent<Button>();
        addLevelCoin1 = transform.Find("UpgradeWeapon/AddLevel1/Text").GetComponent<Text>();
        addLevelCoin2 = transform.Find("UpgradeWeapon/AddLevel2/Text").GetComponent<Text>();

        groups = GetComponent<ToggleGroup>();
        secondaryWeapGroup = transform.Find("SecondaryWeapon/Scroll View/Viewport/Content").GetComponent<ToggleGroup>();

        mainBtn .onValueChanged .AddListener(OnMainBtnClick);
        secondBtn.onValueChanged.AddListener(OnSecondBtnClick);
        dayCoinBtn.onValueChanged.AddListener(OnDayCoinBtnClick);
        addLevel1 .onClick .AddListener(OnAddLevel1Click);
        addLevel2 .onClick .AddListener(OnAddLevel2Click);

        MessageManager .GetInstance .AddListener(MessageDefine .OnBgBtnAdd,OnBgBtnClick);

        Transform content = secondaryWeapGroup.transform;
        for (int i = 0; i < content.childCount; i++)
        {
            var item = content.GetChild(i).GetComponent<SecondWeaponItem>();
            item.Bind(this);
        }
    }

    void OnBgBtnClick()
    {
        if (groups.AnyTogglesOn())
        {
            groups.SetAllTogglesOff();
            if (currentWeaponModel != null)
            {
                RemoveListener(currentWeaponModel);
            }
            currentWeaponModel = null;
            currentClickToggle = -1;
            PlayerController .GetInstance .CurrentPlayer .Machine .TranslateState(PlayerState.Idle);
        }
        else
        {
            if (PlayerController.GetInstance.CurrentPlayer.Machine.GetCurrentState() == PlayerState.Ready)
            {
                   GameController .GetInstance .EnterBattle();
            }else if(PlayerController.GetInstance.CurrentPlayer.Machine.GetCurrentState() == PlayerState.Idle)
            {
                PlayerController .GetInstance .CurrentPlayer .Machine .TranslateState(PlayerState.Ready);
            }
        }
    }
    void OnMainBtnClick(bool isOn)
    {
        if (!isOn) return;
        if (currentClickToggle != 0)
        {
            currentClickToggle = 0;
            if (currentWeaponModel != null)
            {
                RemoveListener(currentWeaponModel);
            }
            currentWeaponModel = PlayerController.GetInstance.PlayerData.MainWeapon;
            UpdateDate(currentWeaponModel);
            AddListener(currentWeaponModel);
        }
        if (PlayerController.GetInstance.CurrentPlayer.Machine.GetCurrentState() == PlayerState.Ready)
        {
            PlayerController .GetInstance .CurrentPlayer .Machine .TranslateState( PlayerState.Idle);
        }
    }

    void OnSecondBtnClick(bool isOn)
    {
        if (!isOn) return;
        if (PlayerController.GetInstance.PlayerData.Level < 5)
        {
            addPanel.SetActive(false);
            secondWeaponPanel.SetActive(false);
            MessageManager.GetInstance.Dispatch<string>(MessageDefine.OpenWarning, "第5关解锁");
            return;
        }

        Transform content = secondaryWeapGroup.transform;
        if (PlayerController.GetInstance.PlayerData.CurrentUsedSecondWeapon != -1)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                var item = content.GetChild(i).GetComponent<SecondWeaponItem>();
                if (PlayerController.GetInstance.PlayerData.CurrentUsedSecondWeapon == item.WeaponId)
                {
                    content.GetChild(i).GetComponent<Toggle>().isOn = true;
                    break;
                }
            }
        }
        else
        {
            addPanel.SetActive(false);
            return;
        }

        if (currentClickToggle != 1)
        {
            currentClickToggle = 1;
            if (currentWeaponModel != null)
            {
                RemoveListener(currentWeaponModel);
            }
            currentWeaponModel = PlayerController.GetInstance.PlayerData.GetSecondWeaponModel();
            UpdateDate(currentWeaponModel);
            AddListener(currentWeaponModel);
        }
        if (PlayerController.GetInstance.CurrentPlayer.Machine.GetCurrentState() == PlayerState.Ready)
        {
            PlayerController.GetInstance.CurrentPlayer.Machine.TranslateState(PlayerState.Idle);
        }
    }

    void OnDayCoinBtnClick(bool isOn)
    {
        if (!isOn) return;
        if (currentClickToggle != 2)
        {
            currentClickToggle = 2;
            if (currentWeaponModel != null)
            {
                RemoveListener(currentWeaponModel);
            }
            currentWeaponModel = PlayerController.GetInstance.PlayerData.EverydayCoin;
            UpdateDate(currentWeaponModel);
            AddListener(currentWeaponModel);
        }
        if (PlayerController.GetInstance.CurrentPlayer.Machine.GetCurrentState() == PlayerState.Ready)
        {
            PlayerController.GetInstance.CurrentPlayer.Machine.TranslateState(PlayerState.Idle);
        }
    }

    void OnAddLevel1Click()
    {
        PlayerController.GetInstance.PlayerData.Coin -= currentWeaponModel.Properties[0].NextLevelUpdateCoin();
        currentWeaponModel.Properties[0].Level ++;
    }
    void OnAddLevel2Click()
    {
        PlayerController.GetInstance.PlayerData.Coin -= currentWeaponModel.Properties[1].NextLevelUpdateCoin();
        currentWeaponModel.Properties[1].Level ++;
    }


    public void OnSelectSecondWpeaon(int id,ulong unLockLevel)
    {

        if (PlayerController.GetInstance.PlayerData.Level < unLockLevel)
        {
            MessageManager.GetInstance.Dispatch<string>(MessageDefine.OpenWarning, unLockLevel + "级解锁");
            return;
        }
        if (!addPanel.activeSelf)
        {
            addPanel.SetActive(true);
        }
        if (PlayerController.GetInstance.PlayerData.SecondWeapons == null)
        {
            PlayerController.GetInstance.PlayerData.CurrentUsedSecondWeapon = id;
            PlayerController .GetInstance .PlayerData .SecondWeapons = new Dictionary<int ,WeaponModel>();
            currentWeaponModel = PlayerController.GetInstance.AllSecondWeapons[id];
            PlayerController.GetInstance.PlayerData.SecondWeapons.Add(id, currentWeaponModel);
            UpdateDate(currentWeaponModel);
            AddListener(currentWeaponModel);
            return;
        }
        if (!PlayerController.GetInstance.PlayerData.SecondWeapons.ContainsKey(id))
        {
            PlayerController.GetInstance.PlayerData.SecondWeapons.Add(id, PlayerController.GetInstance.AllSecondWeapons[id]);
        }
        if (PlayerController.GetInstance.PlayerData.CurrentUsedSecondWeapon == id) return;
        RemoveListener(currentWeaponModel);
        PlayerController.GetInstance.PlayerData.CurrentUsedSecondWeapon = id;
        currentWeaponModel = PlayerController.GetInstance.AllSecondWeapons[id];
        UpdateDate(currentWeaponModel);
        AddListener(currentWeaponModel);

    }

    void UpdateDate(WeaponModel model)
    {
        if (model == null) return;
        property1Txt.text = string.Format("{0}(Lv{1})       {2}", model.Properties[0].RopertyName, model.Properties[0].Level,
            Util.ToString(model.Properties[0].PropertyValue()));
        property2Txt.text = string.Format("{0}(Lv{1})       {2}", model.Properties[1].RopertyName, model.Properties[1].Level,
            Util.ToString(model.Properties[1].PropertyValue()));
        addLevelCoin1.text = Util.ToString(model.Properties[0].NextLevelUpdateCoin());
        addLevelCoin2.text = Util.ToString(model.Properties[1].NextLevelUpdateCoin());

        if (model.Properties[0].NextLevelUpdateCoin() > PlayerController.GetInstance.PlayerData.Coin)
        {
            addLevel1.interactable = false;
            addLevelCoin1.color = Color.red;
        }
        else
        {
            addLevel1.interactable = true;
            addLevelCoin1.color = Color.black;
        }
        if (model.Properties[1].NextLevelUpdateCoin() > PlayerController.GetInstance.PlayerData.Coin)
        {
            addLevel2.interactable = false;
            addLevelCoin2.color = Color.red;
        }
        else
        {
            addLevel2.interactable = true;
            addLevelCoin2.color = Color.black;
        }
    }
    void AddListener(WeaponModel model)
    {
        if (model == null) return;
        model.Properties[0].OnPropertyValueChange += OnPropertyValueChange;
        model.Properties[1].OnPropertyValueChange += OnPropertyValueChange;
    }
    void RemoveListener(WeaponModel model)
    {
        if (model == null) return;
        model.Properties[0].OnPropertyValueChange -= OnPropertyValueChange;
        model.Properties[1].OnPropertyValueChange -= OnPropertyValueChange;
    }

    void OnPropertyValueChange()
    {
       UpdateDate(currentWeaponModel);
    }
}
