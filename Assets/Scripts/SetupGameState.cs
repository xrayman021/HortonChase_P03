using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGameState : GameState
{
    [SerializeField] int _startingUnitNumber = 10;
    [SerializeField] int _numberOfPlayers = 1;

    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Creating army with " + _startingUnitNumber + " units.");

        _activated = false;
    }

    public override void Tick()
    {
        if(_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnGameState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exiting...");
    }

}
