using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController GetInstance;

    void Awake()
    {
        GetInstance = this;
       StartCoroutine(Init()) ;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Init()
    {
        yield return PlayerController .GetInstance;
        CamData .GetInstance .StartUp();
        EnemyFactory .GetInstance .StartUp();
        AudioManager.GetInstance.StartUp(gameObject);
        AudioManager.GetInstance.PlayAudio("bgm", 0.5f, true);
        UIManager .Instance .OpenPannel(UIPanelType.Bg);
        UIManager .Instance .OpenPannel(UIPanelType.Title);
        UIManager .Instance .OpenPannel(UIPanelType.Coin);
        UIManager .Instance .OpenPannel(UIPanelType.Power);
        UIManager .Instance .OpenPannel(UIPanelType.Gem);
        UIManager .Instance .OpenPannel(UIPanelType.Level);
        UIManager .Instance .OpenPannel(UIPanelType.Upgrade);
        UIManager .Instance .OpenPannel(UIPanelType.Setting);
        UIManager .Instance .OpenPannel(UIPanelType.Warning);
        UIManager .Instance .ClosePannel(UIPanelType.Warning);


    }

    public void EnterBattle()
    {
        if (PlayerController.GetInstance.PlayerData.Energy < 5)
        {
            MessageManager.GetInstance.Dispatch(MessageDefine.OpenWarning, "体力不足");
            return;
        }

        PlayerController .GetInstance .CurrentPlayer .Machine .TranslateState(PlayerState.Fighting);
        UIManager.Instance.ClosePannel(UIPanelType.Title);
        UIManager.Instance.ClosePannel(UIPanelType.Power);
        UIManager.Instance.ClosePannel(UIPanelType.Gem);
        UIManager.Instance.ClosePannel(UIPanelType.Level);
        UIManager.Instance.ClosePannel(UIPanelType.Upgrade);
        UIManager.Instance.OpenPannel(UIPanelType.Result);

        PlayerController.GetInstance.PlayerData.Energy -= 5;
        EnemyFactory .GetInstance .StartCreat();

    }
    public void OnLevelEnd(bool isWin)
    {

        if (isWin)
        {
            PlayerController.GetInstance.CurrentPlayer.Machine.TranslateState(PlayerState.Success);
            if (PlayerController.GetInstance.LevelData.ChallenageLevel == PlayerController.GetInstance.PlayerData.Level)
            {
                PlayerController.GetInstance.PlayerData.Level++;
                PlayerController.GetInstance.PlayerData.Energy += 5;
            }
        }
        else
        {
            PlayerController.GetInstance.CurrentPlayer.Machine.TranslateState(PlayerState.Death);
              EnemyFactory.GetInstance.OnPlayerDeath();
        }
        MessageManager.GetInstance.Dispatch(MessageDefine.OnLevelEnd,isWin);
    }

    public void ReStart()
    {
        PlayerController.GetInstance.PlayerData.Coin += PlayerController.GetInstance.LevelData.GetCoin;

        UIManager.Instance.OpenPannel(UIPanelType.Title);
        UIManager.Instance.OpenPannel(UIPanelType.Power);
        UIManager.Instance.OpenPannel(UIPanelType.Gem);
        UIManager.Instance.OpenPannel(UIPanelType.Upgrade);

        EnemyFactory.GetInstance.ClearEnemies();
        PlayerController.GetInstance.CurrentPlayer.transform .position = new Vector3(0,-5f,0);
        PlayerController.GetInstance.CurrentPlayer.gameObject.SetActive(true);
        PlayerController.GetInstance.CurrentPlayer.Machine .TranslateState( PlayerState.Idle);
    }
    void OnDestroy()
    {
        PlayerController .GetInstance .WriteDataToLocal();
    }
}
