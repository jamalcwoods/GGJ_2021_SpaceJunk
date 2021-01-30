﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject ItemPreab, playerPrefab, debriPrefab, shipPrefab;

    [SerializeField]
    private GameObject oxygenBar,fuelBar;

    [SerializeField]
    private GameObject stats;

    private int spawnRange = 50;
    private int oxygenSpawnAmount = 120;
    private int propellantSpawnAmount = 120;
    private int collectableSpawnAmount = 80;
    private int debriSpawnAmount = 450;

    private int minDebriSize = 1;
    private int maxDebriSize = 4;

    private GameObject playerInstance, keyInstance;

    private List<GameObject> itemInstances = new List<GameObject>(), debriInstances = new List<GameObject>(),entityInstances = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < oxygenSpawnAmount; i++) {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange),0);
            while (!checkForOpenSpawn(v))
            {
                v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            }
            GameObject g = Instantiate(ItemPreab, v,Quaternion.identity);
            ItemManager item = g.GetComponent<ItemManager>();
            g.transform.parent = transform;
            item.Type = ItemTypes.Oxygen;
            item.OxygenReplinishAmount = 20;
            item.GetComponent<SpriteRenderer>().color = Color.blue;
            itemInstances.Add(g);
            entityInstances.Add(g);
        }

        for (int i = 0; i < propellantSpawnAmount; i++) {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            while (!checkForOpenSpawn(v))
            {
                v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            }
            GameObject g = Instantiate(ItemPreab, v, Quaternion.identity);
            g.transform.parent = transform;
            ItemManager item = g.GetComponent<ItemManager>();
            item.Type = ItemTypes.Propellant;
            int pIndex = Random.Range(0, 4);
            item.PType = (PropellantTypes)pIndex;
            //red - fireExt, green - jetpack, yellow - solarsail, cyan - popper
            item.GetComponent<SpriteRenderer>().color = new Color[] { Color.red, Color.green, Color.yellow, Color.cyan }[pIndex];
            itemInstances.Add(g);
            entityInstances.Add(g);
        }

        for (int i = 0; i < collectableSpawnAmount; i++)
        {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            while (!checkForOpenSpawn(v))
            {
                v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            }
            GameObject g = Instantiate(ItemPreab, v, Quaternion.identity);
            g.transform.parent = transform;
            ItemManager item = g.GetComponent<ItemManager>();
            item.Type = ItemTypes.Collectable;
            int cIndex = Random.Range(0, 3);
            item.CType = (CollectableTypes)cIndex;
            // magenta - rebreather, white - armor, gray - propulsion computer
            item.GetComponent<SpriteRenderer>().color = new Color[] { Color.magenta, Color.white, Color.gray}[cIndex];
            itemInstances.Add(g);
            entityInstances.Add(g);
        }

        for (int i = 0; i < debriSpawnAmount; i++)
        {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            while (!checkForOpenSpawn(v))
            {
                v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            }
            GameObject g = Instantiate(debriPrefab, v, Quaternion.identity);
            g.transform.parent = transform;
            int size = Random.Range(minDebriSize, maxDebriSize);
            g.transform.localScale = new Vector3(size,size,1);
            g.GetComponent<Rigidbody2D>().mass *= size;
            debriInstances.Add(g);
            entityInstances.Add(g);
        }


        playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Vector3 V = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
        while (!checkForOpenSpawn(V))
        {
            V = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
        }
        keyInstance = Instantiate(ItemPreab, V, Quaternion.identity);
        ItemManager Item = keyInstance.GetComponent<ItemManager>();
        Item.Type = ItemTypes.Collectable;
        Item.CType = CollectableTypes.Keys;
        Item.GetComponent<SpriteRenderer>().color = Color.yellow;
        Item.transform.localScale = new Vector3(3, 3, 1);

        SpawnShip();
    }

    private void SpawnShip()
    {
        int shipEdge = Mathf.RoundToInt(Random.Range(0, 4));

        // 0:up, 1:right, 2:down, 3:left
        switch (shipEdge)
        {
            case 0:
                Instantiate(shipPrefab, new Vector2(Random.Range(-55, 55), 60), Quaternion.identity);
                break;

            case 1:
                Instantiate(shipPrefab, new Vector2(60, Random.Range(-55, 55)), Quaternion.identity);
                break;

            case 2:
                Instantiate(shipPrefab, new Vector2(Random.Range(-55, 55), -60), Quaternion.identity);
                break;

            case 3:
                Instantiate(shipPrefab, new Vector2(-60, Random.Range(-55, 55)), Quaternion.identity);
                break;
        }
    }

    bool checkForOpenSpawn(Vector3 v)
    {
        bool valid = true;
        foreach(GameObject g in entityInstances)
        {
            if(Vector3.Distance(g.transform.position,v) < 2)
            {
                valid = false;
                break;
            }
        }
        return valid;
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform R = oxygenBar.GetComponent<RectTransform>();
        R.sizeDelta = new Vector2(600f * (playerInstance.GetComponent<PlayerManager>().oxygenAmount / 100), R.sizeDelta.y);

        stats.GetComponent<Text>().text = "" +
        "Oxygen Efficiency: " + (2 - playerInstance.GetComponent<PlayerManager>().oxygenTickRate) * 100 + "%\n" +
        "Armor : " + playerInstance.GetComponent<PlayerMovement>().colResistance + "\n" +
        "Propulsion Calibration: " + playerInstance.GetComponent<Rigidbody2D>().drag;

        if(playerInstance.GetComponent<PlayerManager>().currentPropellant != PropellantTypes.Suit)
        {
            RectTransform r = fuelBar.GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(600f * (playerInstance.GetComponent<PlayerManager>().propellantFuel / 100), r.sizeDelta.y);
        } else
        {
            RectTransform r = fuelBar.GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(0, r.sizeDelta.y);
        }
    }
}
