using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private UIManager _uIManager;
    private void Awake()
    {
        _uIManager = FindObjectOfType<UIManager>();
    }
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 10f, 0f), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<CarController>())
        {
            _uIManager.GoldPlus();
            Destroy(gameObject);
        }
    }
}
