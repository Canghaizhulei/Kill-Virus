using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState : StateTemplate<Player>
{

    public StopState(PlayerState id, Player p) : base(id, p)
    {
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        Time.timeScale = 0.2f;
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        //change state
        if (Input.touchCount > 0)
        {
            Owner.Machine.TranslateState(PlayerState.Fighting);
        }

    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
        Time.timeScale = 1;
    }
}
