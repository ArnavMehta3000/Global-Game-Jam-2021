using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private GameManager manager;

    private void Awake()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (manager == null)
            Debug.LogError("Game Manager not found");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Relic")
        {
            manager.SpawnEnemies();
            manager.RelicCollected();
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Door")
            manager.TriggerWin();
    }
}
