using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyLoot : MonoBehaviour
{
    [SerializeField] private int moneyWorth;
    public int lootDropChance;
    private Collider2D col;

    
    private void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMoney>().money += moneyWorth;
            other.gameObject.GetComponent<PlayerMoney>().SetMoneyCountUI(other.gameObject.GetComponent<PlayerMoney>().money);
            Destroy(gameObject);
        }
    }
}
