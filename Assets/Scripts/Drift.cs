using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    [SerializeField] TrailRenderer[] trailRenderer;
    [SerializeField] ParticleSystem[] steam;

    private void Update()
    {
        StartDrift();
        #region Mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                particlePlay();
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                particlePlay();
            }
        }
        #endregion
        #region UNITYEDITOR
        if (Input.GetMouseButtonDown(0))
        {
            particlePlay();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            particlePlay();
        }
        #endregion
    }
    private void particlePlay()
    {
        foreach (ParticleSystem particle in steam)
        {
            particle.Play();
        }
    }
    private void StartDrift()
    {
        foreach(TrailRenderer T in trailRenderer)
        {
            T.emitting = true;
        }
    }
   
   
}
