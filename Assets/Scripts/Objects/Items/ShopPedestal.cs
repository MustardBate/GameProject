using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPedestal : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject itemHolder;
    [SerializeField] private List<ItemObjectTemplate> itemsPool;
    [SerializeField] private TextMeshProUGUI priceTag;
    private ItemObjectTemplate selectedItem;
    private string rarity;
    public readonly int itemPrice = 15;
    [SerializeField] private int playerMoneyTracker;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        selectedItem = GetItem();
        rarity = selectedItem.itemRarity.ToString();
        
        playerMoneyTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>().money;
        priceTag.text = itemPrice.ToString() + "$";

        itemHolder.GetComponent<ShopItemHolder>().selectedItem = selectedItem;
        itemHolder.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;
        itemHolder.GetComponent<ShopItemHolder>().itemRarity = rarity;
        SetRarityOutline();
    }


    void Update()
    {
        MoneyCheck();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Spawning different item");
            selectedItem = GetItem();
            rarity = selectedItem.itemRarity.ToString();

            itemHolder.GetComponent<ShopItemHolder>().selectedItem = selectedItem;
            itemHolder.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;
            itemHolder.GetComponent<ShopItemHolder>().itemRarity = rarity;
            SetRarityOutline();
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


    private void SetRarityOutline()
    {
        Color purple = new (60, 0, 255, 255);
        Color orange = new (255, 111, 0, 255);

        if (rarity == "Common") itemHolder.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
        else if (rarity == "Uncommon") itemHolder.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
        else if (rarity == "Rare") itemHolder.GetComponent<SpriteRenderer>().material.SetColor("_Color", purple);
        else if (rarity == "Component") itemHolder.GetComponent<SpriteRenderer>().material.SetColor("_Color", orange);
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
                priceTag.color = new Color(1, 0, 0, .5f);
            }

            else
            {
                itemHolder.GetComponent<ShopItemHolder>().isBuyable = true;
                itemHolder.GetComponent<Collider2D>().isTrigger = true;
                priceTag.color = new Color(1, 1, 1, 1);
            }
        }

        else priceTag.color = new Color(0, 0, 0, .5f);
    }
}
