using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject canvas;
    private GameFSM _stateMachine;
    private GameController gameController;

    public GameObject pauseBtn;

    [Header("State Indicators")]
    public GameObject stateIndicatorObj;
    public TMP_Text stateName;

    [Header("Placement State Dependencies")]
    public GameObject waveStartObj;
    public TMP_Text waveNum;

    [Header("Menus")]
    public GameObject LoseMenu;

    private void Awake() {
        _stateMachine = GetComponentInParent<GameFSM>();
        gameController = GetComponentInParent<GameController>();
    }

    // when button is pressed, will change to wave state
    public void SwitchStates(string str) {
        switch (str)
        {
            case "Wave":
                _stateMachine.ChangeState(_stateMachine.WaveState);
                break;
            case "Pause":
                // _stateMachine.ChangeState(_stateMachine.PauseState);
                break;
            default:
                break;
        }
        
    }
}
