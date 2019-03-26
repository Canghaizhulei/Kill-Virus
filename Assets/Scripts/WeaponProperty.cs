using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProperty
{
    private int id;
    private string ropertyName;
    private int level;
    private int maxLevel;
    private ulong propertyValue;
    private ulong baseValue;
    private float valueCoe;

    private ulong baseUpdateCoin;
    private ulong maxUpdateCoin;
    private float coinCoe;
    private ulong nextLevelUpdateCoin;

    public delegate void OnChange();
    public event OnChange OnPropertyValueChange;
    public string RopertyName
    {
        get
        {
            return ropertyName;
        }

        set
        {
            ropertyName = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
            if (OnPropertyValueChange != null)
            {
                OnPropertyValueChange();
            }
        }
    }
    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }


    public ulong BaseUpdateCoin
    {
        get
        {
            return baseUpdateCoin;
        }

        set
        {
            baseUpdateCoin = value;
        }
    }

    public int MaxLevel
    {
        get
        {
            return maxLevel;
        }

        set
        {
            maxLevel = value;
        }
    }
    

    public ulong BaseValue
    {
        get
        {
            return baseValue;
        }

        set
        {
            baseValue = value;
        }
    }

    public float ValueCoe
    {
        get
        {
            return valueCoe;
        }

        set
        {
            valueCoe = value;
        }
    }
    

    public ulong MaxUpdateCoin
    {
        get
        {
            return maxUpdateCoin;
        }

        set
        {
            maxUpdateCoin = value;
        }
    }

    public float CoinCoe
    {
        get
        {
            return coinCoe;
        }

        set
        {
            coinCoe = value;
        }
    }

    public ulong PropertyValue()
    {
        return (ulong) (BaseValue + level * ValueCoe);
    }
    public ulong NextLevelUpdateCoin()
    {
            nextLevelUpdateCoin = (ulong) (BaseUpdateCoin + (level * CoinCoe));
            return nextLevelUpdateCoin>MaxUpdateCoin? MaxUpdateCoin:nextLevelUpdateCoin;
    }

    public bool CanUpdateLevel(ulong coin)
    {
        if (level == MaxLevel) return false;
        if (nextLevelUpdateCoin > coin) return false;
        return true;
    }
   

    public WeaponProperty(int id, string name, int level,int maxLevel, ulong baseValue,float valueCoe, ulong baseCoin,ulong maxCoin, float coinCoe)
    {
        this.Id = id;
        this.RopertyName = name;
        this.Level = level;
        this.BaseValue = baseValue;
        this.ValueCoe = valueCoe;
        this.BaseUpdateCoin = baseCoin;
        this.MaxLevel = maxLevel;
        this.CoinCoe = coinCoe;
        this.MaxUpdateCoin = maxCoin;
    }
}
