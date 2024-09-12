using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int money = 0;
    private GameObject text;
    private TextMeshProUGUI textUI;


    void Start()
    {
        text = GameObject.FindGameObjectWithTag("MoneyCount");
        textUI = text.GetComponent<TextMeshProUGUI>();

        textUI.text = money.ToString();
    }

    public void SetMoneyCountUI(int newMoney)
    {
        textUI.text = newMoney.ToString();
    }

    // DEBUGGING
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         Debug.Log("Add 20 to money");
    //         money += 20;
    //         SetMoneyCountUI(money);
    //     }

    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         Debug.Log("Add 50 to money");
    //         money += 50;
    //         SetMoneyCountUI(money);
    //     }

    //     if (Input.GetKeyDown(KeyCode.Alpha3))
    //     {
    //         Debug.Log("Add 100 to money");
    //         money += 100;
    //         SetMoneyCountUI(money);
    //     }
    // }
}
