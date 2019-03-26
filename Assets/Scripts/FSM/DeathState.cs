using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : StateTemplate<Player>
{
    
    public DeathState(PlayerState id, Player p) : base(id, p)
    {
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        Owner.gameObject.SetActive(false);
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);

    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
    }
}
