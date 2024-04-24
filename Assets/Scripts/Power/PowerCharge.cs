using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCharge : MonoBehaviour
{
    public PowerLine ChargeLine;
    public int ChargeLinePosition; //The current position in the ChargeLine array that the power charge is on
    public float ChargeSpeedStart;
    public float ChargeDecay; //The amount the charge decays over distance over time as a percentage
    public float ChargeSizeDecay;
    private float _chargeSpeedCurrent;
    public int ChargeDirection;
    public float ChargeAmount; //The amount of charge imparted onto towers once the charge object reaches its tile

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
                //ALTER IF THE COLOR SYSTEM IS ADDED
                tower.AddCharge(ChargeAmount);
                ChargeAmount *= ChargeDecay;
                transform.localScale = new Vector3(transform.localScale.x - ChargeSizeDecay, transform.localScale.y - ChargeSizeDecay, transform.localScale.z - ChargeSizeDecay);
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

        if(ChargeLine == null)
        {
            return true;
        }

        return false;
    }
}
