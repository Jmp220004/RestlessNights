using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    private GameController _controller;
    
    // state instances
    public GameSetupState SetupState { get; private set; }
    public GameWinState WinState { get; private set; }
    public GameLoseState LoseState { get; private set; }
    // public GamePauseState PauseState { get; private set; }
    public GameWaveState WaveState { get; private set; }
    public GamePlacementState PlacementState { get; private set; }

    private void Awake() {
        _controller = GetComponent<GameController>();

        // create states
        SetupState = new GameSetupState(this, _controller);
        WinState = new GameWinState(this, _controller);
        LoseState = new GameLoseState(this, _controller);
        // PauseState = new GamePauseState(this, _controller);
        WaveState = new GameWaveState(this, _controller);
        PlacementState = new GamePlacementState(this, _controller);
    }

    private void Start() {
        ChangeState(SetupState);
    }
}