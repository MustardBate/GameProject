using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    private Collider2D col;
    public ItemObjectTemplate selectedItem;
    public string itemRarity;
    
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            selectedItem.ApplyBuff(other.gameObject);
            Destroy(gameObject);
        }
    }
}
