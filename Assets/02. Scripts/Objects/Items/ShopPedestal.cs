using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPedestal : MonoBehaviour
{
    [SerializeField] private GameObject itemHolder;
    [SerializeField] private List<ItemObjectTemplate> itemsPool;
    [SerializeField] private TextMeshProUGUI priceTagUI;
    [SerializeField] private Sprite emptyPedestal;
    [SerializeField] private ToolTipTrigger toolTip;
    [HideInInspector] public ItemObjectTemplate selectedItem;
    private string rarity;
    [HideInInspector] public int itemPrice;
    private int playerMoneyTracker;

    
    // Start is called before the first frame update
    void Start()
    {
        selectedItem = GetItem();
        rarity = selectedItem.itemRarity.ToString();
        
        playerMoneyTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>().money;
        priceTagUI.text = itemPrice.ToString() + "$";

        itemHolder.GetComponent<ShopItemHolder>().selectedItem = selectedItem;
        itemHolder.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;

        toolTip.item = selectedItem;

        SetRarityOutlineAndPrice();
    }


    void Update()
    {
        MoneyCheck();

        //DEBUGGING
        if (Input.GetKeyDown(KeyCode.E) && itemHolder != null)
        {
            Debug.Log("Spawning different item");
            selectedItem = GetItem();
            rarity = selectedItem.itemRarity.ToString();

            itemHolder.GetComponent<ShopItemHolder>().selectedItem = selectedItem;
            itemHolder.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;

            toolTip.item = selectedItem;

            SetRarityOutlineAndPrice();
        }
    }


    private ItemObjectTemplate GetItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<ItemObjectTemplate> possibleItems = new ();

        foreach (ItemObjectTemplate item in itemsPool)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            ItemObjectTemplate choosenItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return choosenItem;
        }

        else if (possibleItems.Count == 0)
        {
            return itemsPool[0];
        }

        return null;
    }


    private void SetRarityOutlineAndPrice()
    {
        Color purple = new (60, 0, 255, 255);
        Color orange = new (255, 111, 0, 255);

        if (rarity == "Common")
        {
            itemHolder.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
            itemPrice = 10;
            priceTagUI.text = itemPrice.ToString() + "$";
        } 
        else if (rarity == "Uncommon")
        {
            itemHolder.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
            itemPrice = 20;
            priceTagUI.text = itemPrice.ToString() + "$";
        }
        else if (rarity == "Rare") 
        {
            itemHolder.GetComponent<SpriteRenderer>().material.SetColor("_Color", purple);
            itemPrice = 30;
            priceTagUI.text = itemPrice.ToString() + "$";
        }
        else if (rarity == "Component") 
        {
            itemHolder.GetComponent<SpriteRenderer>().material.SetColor("_Color", orange);
            itemPrice = 45;
            priceTagUI.text = itemPrice.ToString() + "$";
        }
    }

    
    private void MoneyCheck()
    {
        playerMoneyTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>().money;
        if (itemHolder != null)
        {
            if (playerMoneyTracker < itemPrice)
            {
                itemHolder.GetComponent<ShopItemHolder>().isBuyable = false;
                itemHolder.GetComponent<Collider2D>().isTrigger = false;
                priceTagUI.color = new Color(1f, 1f, 1f, .5f);
            }

            else
            {
                itemHolder.GetComponent<ShopItemHolder>().isBuyable = true;
                itemHolder.GetComponent<Collider2D>().isTrigger = true;
                priceTagUI.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else 
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = emptyPedestal;
            priceTagUI.text = "";
        }
    }
}
