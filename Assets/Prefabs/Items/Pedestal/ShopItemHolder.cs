using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHolder : MonoBehaviour
{
    private Collider2D col;
    [HideInInspector] public ItemObjectTemplate selectedItem;
    [HideInInspector] public string itemRarity;
    [HideInInspector] public bool isBuyable;
    private PlayerMoney playerMoney;
    private int priceTag;
    
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();

        priceTag = GetComponentInParent<ShopPedestal>().itemPrice;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isBuyable == true)
        {
            playerMoney.money += priceTag;
            playerMoney.SetMoneyCountUI(playerMoney.money);
            selectedItem.ApplyBuff(other.gameObject);
            Destroy(gameObject);
        }
    }
}
