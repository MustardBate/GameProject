using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestal : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject itemHolder;
    [SerializeField] private List<ItemObjectTemplate> itemsPool;
    [SerializeField] private ToolTipTrigger toolTip;
    [HideInInspector] public ItemObjectTemplate selectedItem;
    [SerializeField] private Sprite emptyPedestal;
    private string rarity;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        toolTip = itemHolder.GetComponent<ToolTipTrigger>();
        selectedItem = GetItem();
        rarity = selectedItem.itemRarity.ToString();

        itemHolder.GetComponent<ItemHolder>().selectedItem = selectedItem;
        itemHolder.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;
        itemHolder.GetComponent<ItemHolder>().itemRarity = rarity;

        toolTip.item = selectedItem;

        SetRarityOutline();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Spawning different item");
            selectedItem = GetItem();
            rarity = selectedItem.itemRarity.ToString();

            itemHolder.GetComponent<ItemHolder>().selectedItem = selectedItem;
            itemHolder.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;
            itemHolder.GetComponent<ItemHolder>().itemRarity = rarity;

            toolTip.item = selectedItem;

            SetRarityOutline();
        }

        if (itemHolder == null) gameObject.GetComponent<SpriteRenderer>().sprite = emptyPedestal;
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
}
