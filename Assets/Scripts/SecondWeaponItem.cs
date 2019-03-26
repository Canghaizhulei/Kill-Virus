using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SecondWeaponItem : MonoBehaviour
{
    public int WeaponId;
    public ulong UnLockLevel;
    private Upgrade upgrade;
    public void Bind(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        GetComponent<Toggle>().onValueChanged .AddListener(OnVelueChange);
    }
    public void OnVelueChange(bool isOn)
    {
        if (isOn)
        {
            upgrade .OnSelectSecondWpeaon(WeaponId , UnLockLevel);
        }
    }
}
