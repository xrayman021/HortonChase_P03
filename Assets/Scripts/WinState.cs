using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinState : GameState
{
    [SerializeField] Text _playerWinTextUI = null;

    

    public override void Enter()
    {
        //base.Enter();
        Debug.Log("Player has won!");
        _playerWinTextUI.gameObject.SetActive(true);

        _playerWinTextUI.text = "You Win!  Press Spacebar to return to menu.";
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        //base.Exit();
        _playerWinTextUI.gameObject.SetActive(false);
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;

        Debug.Log("Player Turn: Exiting...");
    }

    void OnPressedConfirm()
    {
        Debug.Log("Returning to Main Menu.");
        SceneManager.LoadScene("MainMenu");
        //Debug.Log("Attempt to enter Enemy State!");
    }
}
