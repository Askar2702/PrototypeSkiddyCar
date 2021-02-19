using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlColor : MonoBehaviour
{
    [SerializeField] Color[] colorLvl;
    Camera camera;
    private void Awake()
    {
        camera = Camera.main;
    }
    void Start()
    {
        camera.backgroundColor = colorLvl[Random.Range(0, colorLvl.Length)];
    }

    
}
