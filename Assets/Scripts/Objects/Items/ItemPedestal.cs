using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestal : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject itemHolder;
    [SerializeField] private List<ItemObjectTemplate> itemsPool;
    private ItemObjectTemplate selectedItem;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        selectedItem = GetItem();

        itemHolder.GetComponent<ItemHolder>().selectedItem = selectedItem;
        itemHolder.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Spawning different item");
            selectedItem = GetItem();

            itemHolder.GetComponent<ItemHolder>().selectedItem = selectedItem;
            itemHolder.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;
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
    // void ScriptableObject GetItem()
    // {
    //     int randomNumber = Random.Range(1, 101);
    //     List<ScriptableObject> possibleItems = new ();

    //     foreach (ScriptableObject item in itemsPool)
    //     {
            
    //     }
    // }
}
