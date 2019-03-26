using UnityEngine;
public enum PlayerState
{
    None,
    Idle,
    Ready,
    Fighting,
    Stop,
    Death,
    Success
}
public class Player : MonoBehaviour {


    public StateMachine Machine;

    private float moveSpeed;
    //todo effect
    private bool isMaxShotSpeed;
    private bool isDoublePower;
    private bool isUnbeatable;
    private bool isAddWingPlane;
    private bool isCrazyCoin;
    private bool isBeatBack;


    //data
    private  int bulletCount;
    private int maxBulletCount = 15;
    private float bulletInterval = 0.2f;
    public Transform Muzzle;

#region
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public bool IsMaxShotSpeed
    {
        get
        {
            return isMaxShotSpeed;
        }

        set
        {
            isMaxShotSpeed = value;
        }
    }

    public bool IsDoublePower
    {
        get
        {
            return isDoublePower;
        }

        set
        {
            isDoublePower = value;
        }
    }

    public bool IsUnbeatable
    {
        get
        {
            return isUnbeatable;
        }

        set
        {
            isUnbeatable = value;
        }
    }

    public bool IsAddWingPlane
    {
        get
        {
            return isAddWingPlane;
        }

        set
        {
            isAddWingPlane = value;
        }
    }

    public bool IsCrazyCoin
    {
        get
        {
            return isCrazyCoin;
        }

        set
        {
            isCrazyCoin = value;
        }
    }

    public bool IsBeatBack
    {
        get
        {
            return isBeatBack;
        }

        set
        {
            isBeatBack = value;
        }
    }

    public int BulletCount
    {
        get
        {
            return bulletCount;
        }

        set
        {
            bulletCount = value;
        }
    }

    public int MaxBulletCount
    {
        get
        {
            return maxBulletCount;
        }

        set
        {
            maxBulletCount = value;
        }
    }

    #endregion
    void Start()
    {

        IdleState idle = new IdleState(PlayerState.Idle, this);
        ReadyState ready = new ReadyState(PlayerState.Ready, this);
        FightingState fight = new FightingState(PlayerState.Fighting, this);
        StopState stop = new StopState(PlayerState.Stop, this);
        SuccessState success = new SuccessState(PlayerState.Success, this);
        DeathState death = new DeathState(PlayerState.Death, this);
        Machine = new StateMachine(idle);
        Machine.AddState(ready);
        Machine.AddState(fight);
        Machine.AddState(stop);
        Machine.AddState(success);
        Machine.AddState(death);

    }

    void Update()
    {
        Machine.Update();

    }

     public void Init()
    {

        BulletCount = (int) (PlayerController.GetInstance.PlayerData.MainWeapon.Properties[0].PropertyValue() / 10+1);
        BulletCount = BulletCount > MaxBulletCount ? MaxBulletCount : BulletCount;
        MoveSpeed = 20;
        PlayerController.GetInstance.LevelData.Init(PlayerController .GetInstance .PlayerData .Level , 1, 0);
    }

    //todo 扩展Bullet
    public void Shot()
    {
        var power = isDoublePower
                ? PlayerController.GetInstance.PlayerData.MainWeapon.Properties[1].PropertyValue() * 2
                : PlayerController.GetInstance.PlayerData.MainWeapon.Properties[1].PropertyValue();
        int index = isBeatBack ? 1 : 0;

        var count = isMaxShotSpeed ? MaxBulletCount : BulletCount;
        float left = -((count - 1)*bulletInterval/2);
        for (int i = 0; i < count; i++)
        {
            var obj = ObjectPool.GetInstance().GetObj("Bullet");
            var bullet = obj.GetComponent<Bullet>();
            
            Color  c =( isCrazyCoin ? Color.yellow : Color.white) +( isDoublePower ? Color.red : Color.white);
            bullet.Init(power , Muzzle.position, left+i* bulletInterval,index ,c);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            AudioManager.GetInstance.PlayAudio("Boom");
            GameController .GetInstance .OnLevelEnd(false );
        }
    }
}
