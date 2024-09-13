using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHolder : MonoBehaviour
{
    private Collider2D col;
    [SerializeField] private ShopPedestal pedestal;
    [HideInInspector] public ItemObjectTemplate selectedItem;
    [HideInInspector] public string itemRarity;
    [HideInInspector] public bool isBuyable;
    private PlayerMoney playerMoney;
    private int itemPrice;

    
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isBuyable == true)
        {
            //Decrease player's money count
            itemPrice = pedestal.itemPrice;
            playerMoney.money -= itemPrice;
            playerMoney.SetMoneyCountUI(playerMoney.money);

            //Apply buff to player
            ToolTipManager.HideToolTip();
            selectedItem.ApplyBuff(other.gameObject);
            Destroy(gameObject);
        }
    }
}
