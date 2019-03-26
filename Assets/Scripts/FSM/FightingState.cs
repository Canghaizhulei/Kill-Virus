using UnityEngine;
using System.Collections;

/// <summary>
/// Attack状态
/// </summary>
public class FightingState : StateTemplate<Player>
{
    private float intervalTime = 0.06f;
    private float timer = 0;
    private PlayerMove move;
    public FightingState(PlayerState id, Player p) : base(id, p)
    {
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        Owner .Init();
        if (move == null)
        {
            move = new PlayerMove();
        }
        move .Init(Owner.transform.position);
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, move.Target,
            Owner.MoveSpeed*Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > intervalTime)
        {
            timer = 0;
            Owner.Shot();
        }

        //change state
        if (Input.touchCount == 0)
        {
            //Owner .Machine.TranslateState(PlayerState.Stop);
        }
        move .Update();
    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
    }

}