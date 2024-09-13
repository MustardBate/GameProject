using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Numerics;

public class ToolTipManager : MonoBehaviour
{
    private static ToolTipManager current;
    public TextMeshProUGUI header;
    public TextMeshProUGUI body;


    private void Awake()
    {
        current = this;
        HideToolTip();
    }
    

    private void Update()
    {
        UnityEngine.Vector2 mousePos = Input.mousePosition; 
        transform.position = mousePos;
    }


    public static void ShowToolTip(string headerText, string bodyText)
    {
        current.SetText(headerText, bodyText);
        current.gameObject.SetActive(true);
    }


    public static void HideToolTip()
    {
        current.gameObject.SetActive(false);
    }
    

    private void SetText(string headerText, string bodyText)
    {
        header.text = headerText;
        body.text = bodyText;
    }
}
