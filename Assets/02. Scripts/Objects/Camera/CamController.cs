using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float smoothSpeed = 10f;

    private UnityEngine.Vector3 targetPos, newPos;
    public UnityEngine.Vector3 minPos, maxPos;

    [SerializeField] private Texture2D customCursor;

    private void Start()
    {
        Cursor.SetCursor(customCursor, new UnityEngine.Vector2(0, 0), CursorMode.ForceSoftware);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (transform.position != player.position)
        {
            targetPos = player.position;

            UnityEngine.Vector3 cameraBoundaryPos = new (
                Mathf.Clamp(targetPos.x, minPos.x, maxPos.x),
                Mathf.Clamp(targetPos.y, minPos.y, maxPos.y),
                Mathf.Clamp(targetPos.z, minPos.z, maxPos.z)
            );

            newPos = UnityEngine.Vector3.Lerp(transform.position, cameraBoundaryPos, smoothSpeed * Time.deltaTime);
            transform.position = newPos;
        }
    }
}
