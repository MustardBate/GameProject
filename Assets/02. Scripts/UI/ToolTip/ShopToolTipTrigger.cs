using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopToolTipTrigger : MonoBehaviour
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
