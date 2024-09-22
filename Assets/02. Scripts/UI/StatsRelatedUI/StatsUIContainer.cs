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
    private int kills = 0;


    public void SetDamageUI(float newDamage)
    {
        damageUI.text = newDamage.ToString("0.00");
    }


    public void SetSpeedUI(float newSpeed)
    {
        speedUI.text = newSpeed.ToString("0.00");
    }


    public void SetBulletSpeedUI(float newBulletSpeed)
    {
        bulletSpeedUI.text = newBulletSpeed.ToString("0.00");
    }


    public void SetKillsUI(int newKills)
    {
        kills += newKills;
        killsUI.text = kills.ToString("00");
    }
}
