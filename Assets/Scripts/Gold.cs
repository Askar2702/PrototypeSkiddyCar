using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    UIManager uIManager;
    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
    }
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 10f, 0f), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<CarController>())
        {
            uIManager.GoldPlus();
            Destroy(gameObject);
        }
    }
}
