using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlColor : MonoBehaviour
{
    [SerializeField] private Color[] _colorLvl;
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }
    void Start()
    {
        _camera.backgroundColor = _colorLvl[Random.Range(0, _colorLvl.Length)];
    }

    
}
