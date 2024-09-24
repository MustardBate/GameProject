using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HubExit : MonoBehaviour
{
    [SerializeField] private GameObject popUp;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            popUp.SetActive(true);
            // col.enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
