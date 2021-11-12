using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTokenCommand : ICommand
{
    public SpawnTokenCommand()
    {

    }

    public void Execute()
    {
        Debug.Log("Spawn Token");
    }
}
