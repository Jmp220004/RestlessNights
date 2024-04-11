using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseZone : MonoBehaviour
{
    private GameFSM _gameFSM;

    private void Awake()
    {
        _gameFSM = GameObject.FindGameObjectWithTag("GameFSM").GetComponent<GameFSM>();
    }
    public void LoseGame()
    {
        Debug.Log("changing to lose state");
        _gameFSM.ChangeState(_gameFSM.LoseState);
    }
}
