using System.Collections;
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
    private GameObject endGameScreen;

    [SerializeField]
    private Text postGameStats;

    [SerializeField]
    private GameObject stats;

    private int spawnRange = 50;
    private int oxygenSpawnAmount = 120;
    private int propellantSpawnAmount = 200;
    private int collectableSpawnAmount = 160;
    private int debriSpawnAmount = 450;

    private int minDebriSize = 1;
    private int maxDebriSize = 4;

    private GameObject playerInstance, keyInstance, shipInstance;

    private List<GameObject> itemInstances = new List<GameObject>(), debriInstances = new List<GameObject>(),entityInstances = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    private void SpawnCollectables()
    {
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
            item.GetComponent<SpriteRenderer>().color = new Color[] { Color.magenta, Color.white, Color.gray }[cIndex];
            itemInstances.Add(g);
            entityInstances.Add(g);
        }
    }

    private void SpawnPropellants()
    {
        for (int i = 0; i < propellantSpawnAmount; i++)
        {
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
    }

    private void SpawnPlayer()
    {
        playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        entityInstances.Add(playerInstance);
    }

    private void SpawnDebri()
    {
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
            g.transform.localScale = new Vector3(size, size, 1);
            g.GetComponent<Rigidbody2D>().mass *= size;
            debriInstances.Add(g);
            entityInstances.Add(g);
        }
    }

    private void SpawnKeys()
    {
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
        entityInstances.Add(keyInstance);
    }

    private void SpawnOxygen()
    {
        for (int i = 0; i < oxygenSpawnAmount; i++)
        {
            Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            while (!checkForOpenSpawn(v))
            {
                v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            }
            GameObject g = Instantiate(ItemPreab, v, Quaternion.identity);
            ItemManager item = g.GetComponent<ItemManager>();
            g.transform.parent = transform;
            item.Type = ItemTypes.Oxygen;
            item.OxygenReplinishAmount = 20;
            item.GetComponent<SpriteRenderer>().color = Color.blue;
            itemInstances.Add(g);
            entityInstances.Add(g);
        }
    }

    public void restartGame()
    {
        foreach(GameObject g in entityInstances)
        {
           Destroy(g);
        }
        endGameScreen.SetActive(false);
        startGame();
    }

    private void startGame()
    {
        SpawnOxygen();
        SpawnCollectables();
        SpawnPropellants();
        SpawnDebri();
        SpawnPlayer();
        SpawnKeys();
        SpawnShip();
    }

    private void SpawnShip()
    {
        int shipEdge = Mathf.RoundToInt(Random.Range(0, 4));

        // 0:up, 1:right, 2:down, 3:left
        switch (shipEdge)
        {
            case 0:
                shipInstance = Instantiate(shipPrefab, new Vector2(Random.Range(-55, 55), 60), Quaternion.identity);
                break;

            case 1:
                shipInstance = Instantiate(shipPrefab, new Vector2(60, Random.Range(-55, 55)), Quaternion.identity);
                break;

            case 2:
                shipInstance = Instantiate(shipPrefab, new Vector2(Random.Range(-55, 55), -60), Quaternion.identity);
                break;

            case 3:
                shipInstance = Instantiate(shipPrefab, new Vector2(-60, Random.Range(-55, 55)), Quaternion.identity);
                break;
        }
        entityInstances.Add(shipInstance);
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
        if (playerInstance != null)
        {
            RectTransform R = oxygenBar.GetComponent<RectTransform>();
            R.sizeDelta = new Vector2(600f * (playerInstance.GetComponent<PlayerManager>().oxygenAmount / 100), R.sizeDelta.y);

            stats.GetComponent<Text>().text = "" +
            "Oxygen Efficiency: " + (2 - playerInstance.GetComponent<PlayerManager>().oxygenTickRate) * 100 + "%\n" +
            "Armor : " + playerInstance.GetComponent<PlayerMovement>().colResistance + "\n" +
            "Propulsion Calibration: " + playerInstance.GetComponent<Rigidbody2D>().drag + "\n" +
            "Total Distance Traveled: " + playerInstance.GetComponent<PlayerManager>().distanceTraveled.ToString("F2") + "m";

       
            if (playerInstance.GetComponent<PlayerManager>().currentPropellant != PropellantTypes.Suit)
            {
                RectTransform r = fuelBar.GetComponent<RectTransform>();
                r.sizeDelta = new Vector2(600f * (playerInstance.GetComponent<PlayerManager>().propellantFuel / 100), r.sizeDelta.y);
            }
            else
            {
                RectTransform r = fuelBar.GetComponent<RectTransform>();
                r.sizeDelta = new Vector2(0, r.sizeDelta.y);
            }

            if (playerInstance.GetComponent<PlayerManager>().oxygenAmount < 0)
            {
                PlayerManager p = playerInstance.GetComponent<PlayerManager>();
                playerInstance.SetActive(false);
                endGameScreen.SetActive(true);
                postGameStats.text = "" +
                "Number of Collisions: " + p.numberOfCollisions + "\n" +
                "Number of Propellants Found: " + p.propellantsPickedUp + "\n" +
                "Number of Collectables Found: " + p.collectablesPickedUp + "\n" +
                "Number of Oxygen Sources Found: " + p.oxygenPickedUp + "\n";
                foreach (KeyValuePair<PropellantTypes,float> pair in p.distanceLibrary)
                {
                    postGameStats.text += "Distance Traveled by " + pair.Key + ": " + pair.Value.ToString("F2") + "m\n";
                }
            }
        }
    }
}
