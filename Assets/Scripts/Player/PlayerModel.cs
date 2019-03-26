using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel {
    private ulong coin;
    private int energy;
    private ulong diamond;
    private ulong level;
    private WeaponModel mainWeapon;
    private Dictionary<int,WeaponModel> secondWeapons;
    private int currentUsedSecondWeapon;
    private WeaponModel everydayCoin;

    public delegate void CoinChange(ulong coin);

    public delegate void EnergyChange(int energy);

    public delegate void DiamondChange(ulong diamond);

    public delegate void LevelChange(ulong level);

    public delegate void UsedSecondWeaponChange(int id);

    public event CoinChange OnCoinChange;
    public event EnergyChange OnEnergyChange;
    public event DiamondChange OnDiamondChange;
    public event LevelChange OnLevelChange;
    public event UsedSecondWeaponChange OnSecondWeaponChange;

#region 属性
    public ulong Coin
    {
        get
        {
            return coin;
        }

        set
        {
            coin = value;
            if (OnCoinChange != null)
            {
                OnCoinChange(coin);
            }
        }
    }

    public int Energy
    {
        get
        {
            return energy;
        }

        set
        {
            energy = value;
            if (OnEnergyChange != null)
            {
                OnEnergyChange(energy);
            }
        }
    }

    public ulong Diamond
    {
        get
        {
            return diamond;
        }

        set
        {
            diamond = value;
            if (OnDiamondChange != null)
            {
                OnDiamondChange(diamond);
            }
        }
    }

    public ulong Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
            if (OnLevelChange != null)
            {
                OnLevelChange(level);
            }
        }
    }

    public WeaponModel MainWeapon
    {
        get
        {
            return mainWeapon;
        }

        set
        {
            mainWeapon = value;
        }
    }

    public Dictionary<int, WeaponModel> SecondWeapons
    {
        get
        {
            return secondWeapons;
        }

        set
        {
            secondWeapons = value;
        }
    }

    public WeaponModel EverydayCoin
    {
        get
        {
            return everydayCoin;
        }

        set
        {
            everydayCoin = value;
        }
    }

    public int CurrentUsedSecondWeapon
    {
        get
        {
            return currentUsedSecondWeapon;
        }

        set
        {
            currentUsedSecondWeapon = value;
            if (OnSecondWeaponChange != null)
            {
                OnSecondWeaponChange(currentUsedSecondWeapon);
            }
        }
    }
#endregion

    public PlayerModel()
    {
        
    }

    public PlayerModel(ulong coin, int energy, ulong diamond, ulong level, WeaponModel mainWeapon,
        Dictionary<int, WeaponModel> secondWeapons, int currentUsedSecondWeapon, WeaponModel everydayCoin)
    {
        this.Coin = coin;
        this.Energy = energy;
        this.Diamond = diamond;
        this.Level = level;
        this.MainWeapon = mainWeapon;
        this.SecondWeapons = secondWeapons;
        this.CurrentUsedSecondWeapon = currentUsedSecondWeapon;
        this.EverydayCoin = everydayCoin;
    }

    public WeaponModel GetSecondWeaponModel()
    {
        return secondWeapons[currentUsedSecondWeapon];
    }
}
