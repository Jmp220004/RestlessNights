using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaveState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GameWaveState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter() {
        base.Enter();

        Debug.Log("STATE: Game Wave");

        // Disables everything on the canvas
        // Iterate through all child GameObjects
        /*foreach (Transform child in _controller.UI.canvas.transform)
        {
            // Set each child GameObject to inactive
            child.gameObject.SetActive(false);
        }*/

        // Activate canva elems
        _controller.UI.waveStartObj.SetActive(false);
        _controller.UI.stateName.text = "Wave State";
    }

    public override void Update()
    {
        base.Update();

        // //check for tap input
        // if(Input.GetMouseButtonDown(0)) {
        //     _stateMachine.ChangeState(_stateMachine.BuildState);
        // }

        // update wave number
    }

    public override void Exit() {
        base.Exit();
    }
}