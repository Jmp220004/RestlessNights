using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCharge : MonoBehaviour
{
    public PowerLine ChargeLine;
    public int ChargeLinePosition; //The current position in the ChargeLine array that the power charge is on
    public float ChargeSpeedStart;
    public float ChargeSpeedDecay; //The amount the speed of charge decays over distance over time as a percentage
    private float _chargeSpeedCurrent;
    public float ChargeDumpDecay; //The amount the decay increases as charge reaches a tower
    public int ChargeDirection;
    public int ChargeAmount; //The amount of charge imparted onto towers once the charge object reaches its tile

    private void Start()
    {
        _chargeSpeedCurrent = ChargeSpeedStart;

        if(checkDespawnConditions())
        {
            onDespawn();
        }
    }

    private void FixedUpdate()
    {
        Vector3 destination = ChargeLine._powerSegments[ChargeLinePosition + ChargeDirection].CurrentTile.transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, _chargeSpeedCurrent * Time.fixedDeltaTime);
        _chargeSpeedCurrent -= _chargeSpeedCurrent * ChargeSpeedDecay * Time.fixedDeltaTime;
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if(distance <= 0.05)
        {
            ChargeLinePosition += ChargeDirection;
            onTileArrival();
        }


        //Despawn the current charge if it gets too slow
        if(_chargeSpeedCurrent <= 0.1)
        {
            onDespawn();
        }
    }

    private void onTileArrival()
    {
        //Code relating to adding charge to towers
        Tile checkTile = ChargeLine._powerSegments[ChargeLinePosition].CurrentTile;
        GameObject tileObject = checkTile.getPlacedObject();
        if(tileObject != null)
        {
            BaseTower tower = tileObject.GetComponent<BaseTower>();
            if(tower != null)
            {
                //THIS REDUCTION CODE MAY NEED TO BE ALTERED IF THE COLOR SYSTEM IS ADDED LATER
                ChargeSpeedDecay += ChargeDumpDecay;
                tower.AddCharge(ChargeAmount);
            }
        }

        if (checkDespawnConditions())
        {
            onDespawn();
        }
    }

    private void onDespawn()
    {
        Destroy(gameObject);
    }

    private bool checkDespawnConditions()
    {
        int nextChargeLinePosition = ChargeLinePosition + ChargeDirection;

        if(nextChargeLinePosition >= ChargeLine._powerSegments.Count || nextChargeLinePosition < 0)
        {
            return true;
        }

        return false;
    }
}
