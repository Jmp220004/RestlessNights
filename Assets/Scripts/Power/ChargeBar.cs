using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] private Image _fillBar;
    [SerializeField] private float _fillSpeed;
    private float _targetFill = 0;

    private float _delayTimer;
    private bool _delayActive;
    private float _delayFill;

    private void Update()
    {
        _fillBar.fillAmount = Mathf.Lerp(_fillBar.fillAmount, _targetFill, _fillSpeed * Time.deltaTime);
        if(_delayActive)
        {
            _delayTimer -= Time.deltaTime;
            if(_delayTimer <= 0)
            {
                _delayActive = false;
                _delayTimer = 0;
                _targetFill = _delayFill;
            }
        }
    }

    public void updateCharge(float currentCharge, float maximumCharge)
    {
        _targetFill = currentCharge/maximumCharge;
    }

    public void updateChargeDelayed(float currentCharge, float maximumCharge, float timeDelayed)
    {
        _delayFill = currentCharge / maximumCharge;
        _delayTimer = timeDelayed;
        _delayActive = true;
    }
}
