using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Generator Settings")]
    [SerializeField] private float _spawnTimerMax;
    private float _spawnTimerCurrent = 0f;
    public bool CanGenerate;
    [Space]
    [Header("Fill References")]
    [SerializeField] private Placeable _placeable;
    [SerializeField] private GameObject _chargeObject;

    private GameFSM _gameFSM;

    private void Awake()
    {
        _gameFSM = GameObject.FindGameObjectWithTag("GameFSM").GetComponent<GameFSM>();

        if (_gameFSM != null)
        {
            _gameFSM.OnStateChange += OnGameStateChange;
        }
    }

    private void OnDestroy()
    {
        _gameFSM.OnStateChange -= OnGameStateChange;
    }

    private void FixedUpdate()
    {
        if(CanGenerate)
        {
            _spawnTimerCurrent += Time.fixedDeltaTime;

            if(_spawnTimerCurrent >= _spawnTimerMax)
            {
                _spawnTimerCurrent = 0f;
                if(checkSpawnConditions())
                {
                    spawnCharge();
                }
            }
        }
    }

    private bool checkSpawnConditions()
    {
        PowerSegment tileSegment = _placeable.CurrentTile.getPowerSegment();
        if(tileSegment != null)
        {
            return true;
        }

        return false;
    }

    private void spawnCharge()
    {
        //Spawn 2 charge objects meant to go both directions on the power line

        GameObject newCharge1 = Instantiate(_chargeObject, gameObject.transform.position, Quaternion.identity);
        GameObject newCharge2 = Instantiate(_chargeObject, gameObject.transform.position, Quaternion.identity);

        setupChargeObject(newCharge1, 1);
        setupChargeObject(newCharge2, -1);
    }

    private void setupChargeObject(GameObject charge, int direction)
    {
        charge.transform.position = _placeable.CurrentTile.transform.position;
        PowerCharge chargeObject = charge.GetComponent<PowerCharge>();
        if(chargeObject != null)
        {
            chargeObject.ChargeLine = _placeable.CurrentTile.getPowerSegment().CurrentLine;
            chargeObject.ChargeLinePosition = chargeObject.ChargeLine._powerSegments.IndexOf(_placeable.CurrentTile.getPowerSegment());
            chargeObject.ChargeDirection = direction;
        }
        else
        {
            Debug.Log("Tower tried to spawn charge without PowerCharge script attached. Please add a charge to the prefab.");
        }
    }

    public void OnGameStateChange(string newStateName)
    {
        Debug.Log(newStateName);
        switch (newStateName)
        {
            case "GamePlacementState":
                CanGenerate = false;
                break;

            case "GameLoseState":
                CanGenerate = false;
                break;

            case "GameWaveState":
                CanGenerate = true;
                break;
        }
    }
}
