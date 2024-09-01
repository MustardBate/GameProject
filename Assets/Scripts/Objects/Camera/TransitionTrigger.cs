using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    public UnityEngine.Vector3 newCamPos, newPlayerPos;
    private bool hasEntered;

    CamController camControl;

    private void Start()
    {
        camControl = Camera.main.GetComponent<CamController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            camControl.minPos += newCamPos;
            camControl.maxPos += newCamPos;

            other.transform.position += newPlayerPos;
        }
    }
}
