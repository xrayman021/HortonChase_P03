using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGameState : GameState
{
    [SerializeField] int _startingUnitNumber = 10;
    [SerializeField] int _numberOfPlayers = 1;

    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Creating army with " + _startingUnitNumber + " units.");
    }

    public override void Exit()
    {
        Debug.Log("Setup: Exiting...");
    }

}
