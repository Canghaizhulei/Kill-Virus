using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    protected float Speed = 1;
    public Vector3 MoveDir;
    public float Radius;
    public bool IsEnterZone;
    public Vector3 BornPosition;
    public ulong Life;

    //state
    protected bool isUnbeatable;

    Text lifeTxt;

    public void Init(ulong life, float radius,Vector3 dir, Vector3 positon)
    {
        this.Life = life;
        this.Radius = radius;
        transform.localScale = new Vector3(Radius * 2, Radius * 2, Radius * 2);
        BornPosition = positon;
        transform.position = BornPosition;
        MoveDir = dir;
        lifeTxt =transform .Find("Canvas/Image/Text"). GetComponent<Text>();
        lifeTxt.text = Util.ToString(Life);
    }
    public void Init(ulong life, float radius)
    {
        Init(life,radius, new Vector3(Random.Range(-1, 1f), -Random.Range(0.3f, 1f), 0), new Vector3(Random.Range(CamData.GetInstance.LeftBorder + 2 * Radius, CamData.GetInstance.RightBorder - 2 * Radius), CamData.GetInstance.TopBorder + CamData.GetInstance.Height / 3 + Radius, 0));
    }
    

    public virtual void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        if (!IsEnterZone && transform.localPosition.y - Radius <= CamData.GetInstance.TopBorder)
        {
            IsEnterZone = true;
        }
        transform.Translate(MoveDir * Time.deltaTime * Speed);
        //上 如果物体的Y轴和屏幕Y轴相等那么就是证明到达边界
        if (transform.localPosition.y + Radius >= CamData.GetInstance.TopBorder && IsEnterZone)
        {
            transform.position = new Vector3(transform.localPosition.x, CamData.GetInstance.TopBorder - Radius, 0);
            //计算物体到达边界之后又按原来角度弹回去
            MoveDir = Vector3.Reflect(MoveDir, Vector3.down);
        }
        //下
        if (transform.localPosition.y + Radius * 2 <= CamData.GetInstance.DownBorder)
        {
            //位置重置
            transform.position = BornPosition;
            IsEnterZone = false;
        }
        //左
        if (transform.localPosition.x - Radius <= CamData.GetInstance.LeftBorder)
        {
            transform.position = new Vector3(CamData.GetInstance.LeftBorder + Radius, transform.localPosition.y, 0);
            MoveDir = Vector3.Reflect(MoveDir, Vector3.right);
        }
        //右
        if (transform.localPosition.x + Radius >= CamData.GetInstance.RightBorder)
        {
            transform.position = new Vector3(CamData.GetInstance.RightBorder - Radius, transform.localPosition.y, 0);
            MoveDir = Vector3.Reflect(MoveDir, Vector3.left);
        }
    }

    public virtual void EneryDamage(ulong bulletPower)
    {
        if (isUnbeatable||!IsEnterZone) return;
        if (Life <= bulletPower)
        {
            Death();
            return;
        }
        Life -= bulletPower;
        lifeTxt.text = Util.ToString(Life);
    }

    public virtual void Death()
    {
        var r = Random.value;
        if (r < 0.5)
        {
            for (int i = 0; i < Random .Range(1,5); i++)
            {
                var pos = Camera.main.WorldToScreenPoint(transform.position);
                var obj = ObjectPool.GetInstance().GetObj("Coin");
                obj.transform.SetParent(UIManager.Instance.CanvasTransform,true);
                obj.GetComponent<Coin>().Init(pos);
            }
        }
        EnemyFactory .GetInstance .OnEnemyDeath(this);
        AudioManager.GetInstance.PlayAudio("SmallBoom",0.5f,false);
        ObjectPool .GetInstance() .RecycleObj(gameObject);
    }
}
