using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    [SerializeField] private Image _fillBar;
    [SerializeField] private healthNew _hp;
    [SerializeField] private float _fillSpeed;
    private float _targetFill = 0;

    // Update is called once per frame
    void Update()
    {
        _fillBar.fillAmount = Mathf.Lerp(_fillBar.fillAmount, _targetFill, _fillSpeed * Time.deltaTime);
    }

    public void updateBar()
    {
        _targetFill = _hp.getHealthRatio();
    }
}
