using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour
{
    [HideInInspector] public ItemObjectTemplate item;

    public void OnMouseEnter()
    {
        ToolTipManager.ShowToolTip(item.name, item.itemDescription);
    }

    public void OnMouseExit()
    {
        ToolTipManager.HideToolTip();
    }
}

