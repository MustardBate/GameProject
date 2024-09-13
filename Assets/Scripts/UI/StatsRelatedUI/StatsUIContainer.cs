using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUIContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageUI;
    [SerializeField] private TextMeshProUGUI bulletSpeedUI;
    [SerializeField] private TextMeshProUGUI speedUI;
    [SerializeField] private TextMeshProUGUI killsUI;


    public void SetDamageUI(int newDamage)
    {
        damageUI.text = newDamage.ToString("00");
    }


    public void SetSpeedUI(int newSpeed)
    {
        speedUI.text = newSpeed.ToString("00");
    }


    public void SetBulletSpeedUI(int newBulletSpeed)
    {
        bulletSpeedUI.text = newBulletSpeed.ToString("00");
    }


    public void SetKillsUI(int newKills)
    {
        killsUI.text = newKills.ToString("00");
    }
}
