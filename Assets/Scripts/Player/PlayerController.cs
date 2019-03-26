using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;



public class PlayerController:CustomYieldInstruction
{
    private static PlayerController instance;
    public static PlayerController GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerController();
            }
            return instance;
        }
    }

    public PlayerModel PlayerData;
    public Player CurrentPlayer { get; set; }

    public  Dictionary<int, WeaponModel> AllSecondWeapons;

    public LevelDataModel LevelData;

    private bool isLoading = true ;
    private PlayerController()
    {
        AllSecondWeapons = new Dictionary<int, WeaponModel>();
        GameController.GetInstance.StartCoroutine(Load());
        
    }
    public void StartUp()
    {
    }

    IEnumerator Load()
    {
        isLoading = true;
        string path =
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.streamingAssetsPath + "/SecondWeaponsdata.txt";
#elif UNITY_IPHONE && !UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + "/SecondWeaponsdata.txt";
#elif UNITY_STANDLONE_WIN||UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + "/SecondWeaponsdata.txt";
#else
        string.Empty;
#endif
        //string path = Application.streamingAssetsPath + "/SecondWeaponsdata.txt";
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone && string.IsNullOrEmpty(www.error))
        {
            List<WeaponModel> weapons = JsonConvert.DeserializeObject<List<WeaponModel>>(www.text);
            for (int i = 0; i < weapons.Count; i++)
            {
                AllSecondWeapons.Add(weapons[i].Id, weapons[i]);
            }
        }
        else
        {
            Debug .Log(www .isDone + www .error);
        }
        //LoadDataFromLocal();
        LevelData = new LevelDataModel();
        Test();
        isLoading = false;

        var p = ObjectPool.GetInstance().GetObj("Player");
        CurrentPlayer = p.GetComponent<Player>();
    }

    private void InitPlayerModel()
    {
        //todo 改成从服务器读取初始数据
        WeaponModel main = new WeaponModel();
        main.Id = 0;
        main .Properties = new Dictionary<int,WeaponProperty>();
        main .Properties .Add(0,new WeaponProperty(0,"射速",1,100,5,1.1f,200,3000000,900));
        main .Properties .Add(1,new WeaponProperty(1,"火力",1,int.MaxValue,100,10f,200,2000000,800));

        WeaponModel dayCoin = new WeaponModel();
        dayCoin.Id = 0;
        dayCoin .Properties = new Dictionary<int, WeaponProperty>();
        dayCoin.Properties.Add(0, new WeaponProperty(0, "金币价值", 1, 100, 100, 2, 150, 3000000, 600));
        dayCoin.Properties.Add(1, new WeaponProperty(1, "日常收益", 1, 100, 300, 5, 200, 3000000, 800));
        PlayerData = new PlayerModel(0,100,0,1,main ,null ,-1,dayCoin );
    }

    void Update()
    {
    }


    void LoadDataFromLocal()
    {
        string path = Application.persistentDataPath + "/data.txt";
        if (File.Exists(path))
        {
            PlayerData = JsonConvert.DeserializeObject<PlayerModel>(File.ReadAllText(path));
        }
        else
        {
            InitPlayerModel();
        }
    }

    public  void WriteDataToLocal()
    {
        string path = Application.persistentDataPath + "/data.txt";
        var content = JsonConvert .SerializeObject(PlayerData);
        File .WriteAllText(path,content);
    }

    public override bool keepWaiting
    {
        get { return isLoading; }
    }

    void Test()
    {
        WeaponModel main = new WeaponModel();
        main.Id = 0;
        main.Properties = new Dictionary<int, WeaponProperty>();
        main.Properties.Add(0, new WeaponProperty(0, "射速", 1, 100, 5, 1.1f, 200, 3000000, 900));
        main.Properties.Add(1, new WeaponProperty(1, "火力", 1, int.MaxValue, 10, 80f, 200, 2000000, 800));

        WeaponModel dayCoin = new WeaponModel();
        dayCoin.Id = 0;
        dayCoin.Properties = new Dictionary<int, WeaponProperty>();
        dayCoin.Properties.Add(0, new WeaponProperty(0, "金币价值", 1, 100, 100, 2, 150, 3000000, 600));
        dayCoin.Properties.Add(1, new WeaponProperty(1, "日常收益", 1, 100, 300, 5, 200, 3000000, 800));
        PlayerData = new PlayerModel(100, 100, 0, 1, main, null, -1, dayCoin);
    }
}
