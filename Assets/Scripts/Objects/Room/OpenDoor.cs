using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Collider2D col;
    [SerializeField] private GameObject door;
    

    private void Awake() 
    {
        col = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (door.activeSelf)
        {
            col.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
