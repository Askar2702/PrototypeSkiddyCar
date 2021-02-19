using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    float horizontalInput;
    float verticalInput;
    float currentSteerAngle;
    float currentbreakForce;
    bool isBreaking;

    [SerializeField] float motorForce;
    [SerializeField] float breakForce;
    [SerializeField] float maxSteerAngle;

    [SerializeField] WheelCollider frontLeftWheelCollider;
    [SerializeField] WheelCollider frontRightWheelCollider;
    [SerializeField] WheelCollider rearLeftWheelCollider;
    [SerializeField] WheelCollider rearRightWheelCollider;

    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform frontRightWheeTransform;
    [SerializeField] Transform rearLeftWheelTransform;
    [SerializeField] Transform rearRightWheelTransform;

    
    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        if (GameManager.gameManager._gameState == GameManager.GameState.Lose ||
            GameManager.gameManager._gameState == GameManager.GameState.Win ||
            GameManager.gameManager._gameState == GameManager.GameState.Start)
        {
            isBreaking = true;
            verticalInput = 0;
            HandleMotor();
            return;
        }
        else isBreaking = false;
        GetInput();
    }
    

    private void GetInput()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0) 
        { 
            horizontalInput = 0.5f;
            if (transform.localEulerAngles.y <= 0f || transform.localEulerAngles.y >= 90f)
            {
                horizontalInput = 0f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 90f, 0f), Time.deltaTime * 3f);
            }
        }
        else
        {
            if (transform.localEulerAngles.y <= 90f)
            {
                horizontalInput = -0.5f;
            }
            else if (transform.localEulerAngles.y <= 0f || transform.localEulerAngles.y >= 90f)
            {
                horizontalInput = 0f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 3f);
            }
        }
        verticalInput = 1f;
       
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
       // Debug.LogError(frontLeftWheelCollider.motorTorque);
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
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
