using UnityEngine;
using System.Collections;

/// <summary>
/// Ready状态
/// </summary>

public class ReadyState : StateTemplate<Player>
{
    private Vector3 target = new Vector3(0,-1.5f,0);
    private float speed=1;
    public ReadyState(PlayerState id, Player p) : base(id, p)
    {
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        if ((target - Owner.transform.position).sqrMagnitude > 0.1)
        {
            Owner.transform.position = Vector3.Lerp(Owner.transform.position, target, Time.deltaTime * speed);
        }
    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
    }
}