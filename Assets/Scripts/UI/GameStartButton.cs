using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    [SerializeField] private GameFSM _gameFSM;

    public void onClick()
    {
        _gameFSM.ChangeState(_gameFSM.WaveState);
    }
}
