using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameSM))]
public class GameState : State
{
    protected GameSM StateMachine { get; private set; }

    void Awake()
    {
        StateMachine = GetComponent<GameSM>();
    }
}
