using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIController _ui;
    [SerializeField] private AudioController _audioController;
    public UIController UI => _ui;
    public AudioController audioController => _audioController;

    public event Action OnChangeState = delegate { };
}
