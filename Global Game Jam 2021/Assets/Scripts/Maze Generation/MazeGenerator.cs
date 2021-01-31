using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Generator")]
    [HideInInspector] public Texture2D map;
    public ColorToPrefab[] colorMappings;


    private void OnEnable()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }


    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
            return;

        foreach (ColorToPrefab mapping in colorMappings)
        {
            if (mapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(mapping.prefab, position, Quaternion.identity, transform);

                //If spawning relic
                if (mapping.name.Equals("Relic"))
                {
                    gameManager.GetTotalRelics();
                }

                //If spawning enemy
                if (mapping.name.Equals("Enemy"))
                {
                    //Change name to random
                    mapping.prefab.name = "Spawn Point" + Random.Range(-100, 100).ToString();
                    gameManager.totalEnemies++;
                }
            }
        }

        gameManager.GetData();
    }
}
