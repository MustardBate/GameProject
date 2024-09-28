using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHolder : MonoBehaviour
{
    [SerializeField] private ShopPedestal pedestal;
    [HideInInspector] public ItemObjectTemplate selectedItem;
    [HideInInspector] public bool isBuyable;
    private PlayerMoney playerMoney;

    
    // Start is called before the first frame update
    void Start()
    {
        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isBuyable == true)
        {
            //Decrease player's money count
            playerMoney.money -= pedestal.itemPrice;
            playerMoney.SetMoneyCountUI(playerMoney.money);

            //Apply buff to player
            ToolTipManager.HideToolTip();
            selectedItem.ApplyBuff(other.gameObject);
            Destroy(gameObject);
        }
    }
}
