using Codice.Client.BaseCommands.Merge.Restorer.Finder;
using LTA.Animation;
using LTA.NonEntity;
using LTA.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class StatesInfo
//{
//    public NonEntityInfo currentState;
//}

[ComponentType(typeof(StatesController), typeof(NonEntityInfo), "state", "NonEntities")]
public class StatesController : NonEntityController,ISetInfo
{
    protected NonEntityInfo info;

    public object Info
    {
        set
        {
            info = (NonEntityInfo)value;
            NonEntityInfo = info;
        }
    }

    List<StateController> states = new List<StateController>();

    StateController currentState;

    StateController defaultState;

    IAnimation ianimation;

    IAnimation IAnimation
    {
        get
        {
            if (ianimation == null)
            {
                ianimation = GetComponent<DisplayAnimation>().Animation;
            }
            return ianimation;
        }
    }

    public StateController CurrentState
    {
        set
        {
            if (currentState == value)
            {
                return;
            }
            if (currentState != null)
            {
                //Debug.Log(currentState.NonEntityInfo.name + " " + currentState.IsEndAction);
                if (!currentState.IsEndAction)
                    currentState.IsEndAction = true;
                //Debug.Log(currentState.NonEntityInfo.name + " " + currentState.IsEndAction);
            }
            currentState = value;
            StateInfo stateInfo = CurrentState.StateInfo;
            if (!stateInfo.isLoop)
            {
                IAnimation.SetEndAnim(() =>
                {
                    //StateController nextState = DefaultState;
                    try
                    {
                        StateController checkNextState = GetNonEntity(stateInfo.nextState) as StateController;
                        if (checkNextState != null && checkNextState.IsAllowAction)
                        {
                            checkNextState.Action();
                        }
                    }
                    catch (Exception ex)
                    {
                        foreach (StateController state in states)
                        {
                            if (state.IsAllowAction)
                            {
                                state.Action();
                                return;
                            }
                        }
                        DefaultState.Action();
                    }
                });
            }
            else
            {
                IAnimation.SetEndAnim(null);
            }
            IAnimation.Play(stateInfo.anim);

        }
        get
        {
            return currentState;
        }
    }

    public StateController DefaultState
    {
        set
        {
            defaultState = value;
        }
        get
        {
            return defaultState;
        }
    }

    protected override void EditNonEntityController(string key, NonEntityController nonEntityController)
    {
        if (key == "states")
        {
            if (nonEntityController is StateController)
            {
                states.Add((StateController)nonEntityController);
            }
        }
        base.EditNonEntityController(key, nonEntityController);
    }

    public bool CheckState(StateController state)
    {
        if (CurrentState == null) return true;
        StateInfo stateInfo = currentState.StateInfo;
        if (CurrentState == state) return stateInfo.isLoop;
        return state.StateInfo.priority >= stateInfo.priority || !currentState.IsAllowAction;
    }

    protected override void Clear()
    {
        states.Clear();
        base.Clear();
    }
}
