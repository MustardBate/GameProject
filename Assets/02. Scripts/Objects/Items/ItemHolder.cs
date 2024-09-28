using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [HideInInspector] public ItemObjectTemplate selectedItem;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToolTipManager.HideToolTip();
            selectedItem.ApplyBuff(other.gameObject);
            Destroy(gameObject);
        }
    }
}
