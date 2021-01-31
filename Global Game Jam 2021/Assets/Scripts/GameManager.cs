using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Generation")]
    public GameObject generator;
    public static int mapNumber;
    public Texture2D[] maps;


    [Header("Relics")]
    public int totalRelics = 0;
    public int collectedRelics = 0;

    [Header("Enemies")]
    public GameObject enemyPrefab;
    public int totalEnemies;
    
    [Header("Enemy Spawn Points")]
    private GameObject[] spawnPoints;
    private GameObject exitDoor;
    private bool hasWon = false;

    [Header("UI")]
    public TextMeshProUGUI relicsText;
    public TextMeshProUGUI totalRelicsText;
    public GameObject deathScreen;
    public GameObject winScreen;


    private void Awake()
    {
        generator.GetComponent<MazeGenerator>().map = maps[mapNumber];
        generator.SetActive(true);
    }


    public void GetData()
    {
        //Get spawn points
        spawnPoints = GameObject.FindGameObjectsWithTag("E_Spawn Point");

        //Get door
        exitDoor = GameObject.FindGameObjectWithTag("Door");

        //Shuffle array
        spawnPoints = Shuffle(spawnPoints);
    }

    GameObject[] Shuffle(GameObject[] array)
    {
        GameObject temp;
        for (int i = 0; i < array.Length; i++)
        {
            int rand = Random.Range(i, array.Length);
            temp = array[rand];
            array[rand] = array[i];
            array[i] = temp;
        }

        return array;
    }

    
    public void Update()
    {
        if ((hasWon == false) && (collectedRelics == totalRelics))
            TriggerComplete();
    }
    

    public void RelicCollected()
    {
        collectedRelics++;
        relicsText.text = "RELICS COLLECTED: " + collectedRelics.ToString();
    }


    public void GetTotalRelics()
    {
        totalRelics++;
        totalRelicsText.text = "TOTAL RELICS: " + totalRelics.ToString();
    }


    void TriggerComplete()
    {
        hasWon = true;
        exitDoor.GetComponent<BoxCollider2D>().isTrigger = true;
        exitDoor.layer = LayerMask.NameToLayer("Door");
        exitDoor.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        Debug.Log("WIN");
    }


    public void TriggerDeath()
    {
        Debug.Log("Triggered");
        deathScreen.SetActive(true);
    }

    public void TriggerWin()
    {
        if (hasWon)
        {
            winScreen.SetActive(true);
        }
    }

    public void SpawnEnemies()
    {
        if (totalRelics != totalEnemies)
            Debug.LogError("Relic and Enemy Mismatch");

        Instantiate(enemyPrefab, spawnPoints[collectedRelics].transform.position, Quaternion.identity, spawnPoints[collectedRelics].transform);
    }
}
