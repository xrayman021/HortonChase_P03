using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    [SerializeField] BoardSpawner _boardSpawner = null;

    Camera _camera = null;
    RaycastHit _hitInfo;

    CommandInvoker _commandInvoker = new CommandInvoker();

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GetNewMouseHit();
            SpawnToken();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }

    }

    void GetNewMouseHit()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out _hitInfo, Mathf.Infinity))
        {
            Debug.Log("Ray hit: " + _hitInfo.transform.name);
        }

    }

    void SpawnToken()
    {
        ICommand spawnTokenCommand =
            new SpawnTokenCommand(_boardSpawner, _hitInfo.point);
        _commandInvoker.ExecuteCommand(spawnTokenCommand);
    }

    public void Undo()
    {
        _commandInvoker.UndoCommand();
    }

}
