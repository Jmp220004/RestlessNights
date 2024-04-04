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

        // Disables everything on the canvas
        // Iterate through all child GameObjects
        /*foreach (Transform child in _controller.UI.canvas.transform)
        {
            // Set each child GameObject to inactive
            child.gameObject.SetActive(false);
        }*/

        // Activate canva elems
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