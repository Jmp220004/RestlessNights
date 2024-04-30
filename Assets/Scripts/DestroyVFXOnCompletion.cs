using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DestroyVFXOnCompletion : MonoBehaviour
{
    private bool _hasStarted = false;
    private VisualEffect _vfx;

    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_vfx.aliveParticleCount > 0)
        {
            _hasStarted = true;
        }

        if(_vfx.aliveParticleCount == 0 && _hasStarted)
        {
            Destroy(gameObject);
        }
    }
}
