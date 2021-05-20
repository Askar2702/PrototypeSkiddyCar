using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;
    [Range(0.01f, 1.0f)]
    [SerializeField] private float _smoothFactor;

    private void Start()
    {
        _offset = transform.position - _target.position;
    }
    private void LateUpdate()
    {
        Vector3 newPos = _target.position + _offset;
        transform.position = Vector3.Slerp(transform.position, newPos, _smoothFactor);
    }

    
}
