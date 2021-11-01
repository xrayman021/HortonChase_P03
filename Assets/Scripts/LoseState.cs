using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseState : GameState
{
    [SerializeField] Text _playerLoseTextUI = null;



    public override void Enter()
    {
        //base.Enter();
        Debug.Log("Player has lost!");
        _playerLoseTextUI.gameObject.SetActive(true);

        _playerLoseTextUI.text = "Your army is defeated, Game Over.  Press Spacebar to return to menu.";
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        //base.Exit();
        _playerLoseTextUI.gameObject.SetActive(false);
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
