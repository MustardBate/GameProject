using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    

    private void Awake()
    {
        text.color = Color.black;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth; 
    }


    //Upgrade max health (NOT HEAL)
    public void SetNewMaxHealth(int newMaxHealth, int currentHealth)
    {
        slider.maxValue = newMaxHealth;
        slider.value = currentHealth;
        if (slider.value > newMaxHealth) slider.value = newMaxHealth;

        text.text = slider.value.ToString("00") + "/" + slider.maxValue.ToString("00");
    }


    //Heal 
    public void SetHealth(int health)
    {
        slider.value = health;
        if (slider.value > slider.maxValue) slider.value = slider.maxValue;
    }

    
    //Text UI
    public void SetHealthNumber(int maxHealth, int health)
    {
        if (health > maxHealth) health = maxHealth;
        text.text = health.ToString("00") + "/" + maxHealth.ToString("00");
    }
}
