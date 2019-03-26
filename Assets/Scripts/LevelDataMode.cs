using UnityEngine;
using System.Collections;

public class LevelDataModel
{
    private ulong challenageLevel;
    private float leftEnemyPercent;
    private ulong getCoin;

    public delegate void OnLeftChange(float leftEnemyPercent);
    public event OnLeftChange OnLeftValueChange;
    public delegate void OnGetCoinChange(ulong getCoin);
    public event OnGetCoinChange OnGetCoinValueChange;

    public void Init(ulong level, float left, ulong coin)
    {
        ChallenageLevel = level;
        LeftEnemyPercent = left;
        GetCoin = coin;
    }
   

    public float LeftEnemyPercent
    {
        get
        {
            return leftEnemyPercent;
        }

        set
        {
            if (OnLeftValueChange != null)
            {
                OnLeftValueChange(value);
            }
            leftEnemyPercent = value;
        }
    }

    public ulong GetCoin
    {
        get
        {
            return getCoin;
        }

        set
        {
            if (OnGetCoinValueChange != null)
            {
                OnGetCoinValueChange(value);
            }
            getCoin = value;
        }
    }

    public ulong ChallenageLevel
    {
        get
        {
            return challenageLevel;
        }

        set
        {
            challenageLevel = value;
        }
    }
}
