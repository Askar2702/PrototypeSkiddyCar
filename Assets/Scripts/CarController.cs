using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private float _horizontalInput;
    private float _verticalInput;
    private float _currentSteerAngle;
    private float _currentbreakForce;
    private bool _isBreak;

    [SerializeField] float _motorForce;
    [SerializeField] float _breakForce;
    [SerializeField] float _maxSteerAngle;

    [SerializeField] WheelCollider _frontLeftWheelCollider;
    [SerializeField] WheelCollider _frontRightWheelCollider;
    [SerializeField] WheelCollider _rearLeftWheelCollider;
    [SerializeField] WheelCollider _rearRightWheelCollider;

    [SerializeField] Transform _frontLeftWheelTransform;
    [SerializeField] Transform _frontRightWheeTransform;
    [SerializeField] Transform _rearLeftWheelTransform;
    [SerializeField] Transform _rearRightWheelTransform;

    
    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        if (GameManager.Manager.CurrentStateOfTheGame == GameManager.GameState.Win )
        {
            _isBreak = true;
            _verticalInput = 0;
            GetComponent<Rigidbody>().isKinematic = true;
            return;
        }
        else _isBreak = false;
        GetInput();
    }
    

    private void GetInput()
    {
        if (GameManager.Manager.CurrentStateOfTheGame != GameManager.GameState.Play) return;
        if (Input.GetMouseButton(0) || Input.touchCount > 0) 
        { 
            _horizontalInput = 0.5f;
            if (transform.localEulerAngles.y <= 0f || transform.localEulerAngles.y >= 90f)
            {
                _horizontalInput = 0f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 90f, 0f), Time.deltaTime * 3f);
            }
        }
        else
        {
            if (transform.localEulerAngles.y <= 90f)
            {
                _horizontalInput = -0.5f;
            }
            else if (transform.localEulerAngles.y <= 0f || transform.localEulerAngles.y >= 90f)
            {
                _horizontalInput = 0f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 3f);
            }
        }
        _verticalInput = 1f;
       
    }

    private void HandleMotor()
    {
        _frontLeftWheelCollider.motorTorque = _verticalInput * _motorForce;
        _frontRightWheelCollider.motorTorque = _verticalInput * _motorForce;
        _currentbreakForce = _isBreak ? _breakForce : 0f;
       // Debug.LogError(frontLeftWheelCollider.motorTorque);
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        _frontRightWheelCollider.brakeTorque = _currentbreakForce;
        _frontLeftWheelCollider.brakeTorque = _currentbreakForce;
        _rearLeftWheelCollider.brakeTorque = _currentbreakForce;
        _rearRightWheelCollider.brakeTorque = _currentbreakForce;
    }

    private void HandleSteering()
    {
        _currentSteerAngle = _maxSteerAngle * _horizontalInput;
        _frontLeftWheelCollider.steerAngle = _currentSteerAngle;
        _frontRightWheelCollider.steerAngle = _currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateSingleWheel(_frontRightWheelCollider, _frontRightWheeTransform);
        UpdateSingleWheel(_rearRightWheelCollider, _rearRightWheelTransform);
        UpdateSingleWheel(_rearLeftWheelCollider, _rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
; wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
