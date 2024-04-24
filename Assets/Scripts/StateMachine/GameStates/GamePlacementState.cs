using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlacementState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GamePlacementState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter() {
        base.Enter();

        Debug.Log("STATE: Game Placement");

        _stateMachine.WaveNumber++;

        _controller.UI.waveStartObj.SetActive(true);
        _controller.UI.pauseBtn.SetActive(true);
        // Disables everything on the canvas
        // Iterate through all child GameObjects
        /*foreach (Transform child in _controller.UI.canvas.transform)
        {
            // Set each child GameObject to inactive
            child.gameObject.SetActive(false);
        }*/

        // Activate canva elems
        _controller.UI.stateName.text = "Placement State";
        _controller.UI.waveNum.text = "Wave #: " + _stateMachine.WaveNumber;
    }

    public override void Update()
    {
        base.Update();

        // //check for tap input
        // if(Input.GetMouseButtonDown(0)) {
        //     _stateMachine.ChangeState(_stateMachine.BuildState);
        // }
    }

    public override void Exit() {
        base.Exit();
    }
}