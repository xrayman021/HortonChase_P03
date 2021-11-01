using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnGameState : GameState
{
    [SerializeField] Text _playerTurnTextUI = null;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        //base.Enter();
        Debug.Log("Player turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedUp += OnPressedUp;
        StateMachine.Input.PressedDown += OnPressedDown;
    }

    public override void Exit()
    {
        //base.Exit();
        _playerTurnTextUI.gameObject.SetActive(false);
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedUp -= OnPressedUp;
        StateMachine.Input.PressedDown -= OnPressedDown;

        Debug.Log("Player Turn: Exiting...");
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<EnemyTurnGameState>();
        //Debug.Log("Attempt to enter Enemy State!");
    }

    void OnPressedUp()
    {
        StateMachine.ChangeState<WinState>();
    }

    void OnPressedDown()
    {
        StateMachine.ChangeState<LoseState>();
    }

}
