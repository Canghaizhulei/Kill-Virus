using UnityEngine;
using System.Collections;

public class NomalEnemy : Enemy {
    public override void Death()
    {
        base.Death();
        var radom = Random.Range(0, 10);
        if (radom < 1 && Radius >0.6f)
        {
            EnemyFactory .GetInstance .CreatEnemy(Life/2,Radius /2,new Vector3(1,1,0),transform .position  );
            EnemyFactory .GetInstance .CreatEnemy(Life/2,Radius /2,new Vector3(1,-1,0),transform .position  );
        }
    }
}
