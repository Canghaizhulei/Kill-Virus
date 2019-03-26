using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 状态机器类：由Player控制。完成状态的存储，切换，和状态的保持
/// </summary>

public class StateMachine
{

    //用来存储当前机器所控制的所有状态
    public Dictionary<PlayerState, StateBase> StateCache;

    //定义上一个状态
     StateBase prviousState;
    //定义当前状态
     StateBase currentState;

    //机器初始化时，没有上一个状态
    public StateMachine(StateBase beginState)
    {
        prviousState = null;
        currentState = beginState;

        StateCache = new Dictionary<PlayerState, StateBase>();
        //把状态添加到集合中
        AddState(beginState);
        currentState.OnEnter();
    }

    public void AddState(StateBase state)
    {
        if (!StateCache.ContainsKey(state.ID))
        {
            StateCache.Add(state.ID, state);
            state.machine = this;
        }
    }

    //通过Id来切换状态
    public void TranslateState(PlayerState id)
    {
        if (!StateCache.ContainsKey(id))
        {
            return;
        }
        if (prviousState != null)
        {
        prviousState .OnExit();
        }
        prviousState = currentState;
        currentState = StateCache[id];
        currentState.OnEnter();
    }

    //状态保持
    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnStay();
        }
    }

    public PlayerState GetCurrentState()
    {
        return currentState.ID;
    }
}