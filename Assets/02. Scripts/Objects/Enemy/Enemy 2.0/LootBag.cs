using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [Header("Item pools")]
    [SerializeField] private List<GameObject> healthLootPool;
    [SerializeField] private List<GameObject> moneyLootPool;
    private GameObject possibleHealthLoot;
    private List<GameObject> possibleMoneyLoot;

    
    private void Start()
    {
        possibleHealthLoot = GetDroppedHealthLoot();
        possibleMoneyLoot = GetDroppedMoneyLoots();

        // Debug.Log("Health: " + possibleHealthLoot);
        // Debug.Log("Money: ");
        // foreach (GameObject loot in possibleMoneyLoot)
        // {
        //     Debug.Log(loot);
        // }
    }


    private List<GameObject> GetDroppedMoneyLoots()
    {
        int random = Random.Range(1, 101);
        List<GameObject> possibleLoot = new ();
        List<GameObject> actualLoot = new ();

        foreach (GameObject loot in moneyLootPool)
        {
            if (random <= loot.GetComponent<MoneyLoot>().lootDropChance)
            {
                possibleLoot.Add(loot);
            }
        }

        if (possibleLoot.Count > 0)
        {
            int lootBagSize = UnityEngine.Random.Range(1, 4);
            for (int i = 0; i < lootBagSize; i++)
            {
                actualLoot.Add(possibleLoot[Random.Range(0, possibleLoot.Count)]);
            }
            return actualLoot;
        }
        
        else return new List<GameObject>();
    }


    private GameObject GetDroppedHealthLoot()
    {
        int random = Random.Range(1, 101);
        List<GameObject> possibleLoot = new ();

        foreach (GameObject loot in healthLootPool)
        {
            if (random <= loot.GetComponent<HealthLoot>().lootDropChance)
            {
                possibleLoot.Add(loot);
            }
        }

        if (possibleLoot.Count > 0) return possibleLoot[Random.Range(0, possibleLoot.Count)];

        return null;
    }


    public void InstantiateLoot(UnityEngine.Vector3 spawnPos)
    {
        possibleHealthLoot = GetDroppedHealthLoot();
        possibleMoneyLoot = GetDroppedMoneyLoots();

        if (possibleHealthLoot == null || possibleMoneyLoot.Count > 0)
        {
            // Debug.Log("Loot bag only has money");
            foreach (GameObject loot in possibleMoneyLoot)
            {
                var droppedMoneyLoot = Instantiate(loot, spawnPos, UnityEngine.Quaternion.identity);

                float dropForce = 75f;
                UnityEngine.Vector2 dropDirection = new (Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                droppedMoneyLoot.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            }
        }


        else if (possibleMoneyLoot.Count == 0 && possibleHealthLoot == null)
        {
            // Debug.Log("Loot bag has nothing lol");
        }


        else if (possibleHealthLoot != null && (possibleMoneyLoot.Count == 0 || possibleMoneyLoot.Count > 0))
        {
            // Debug.Log("Loot bag has health");   
            Instantiate(possibleHealthLoot, spawnPos, UnityEngine.Quaternion.identity);
        }
    }
}
