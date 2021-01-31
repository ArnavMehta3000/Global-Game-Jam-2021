using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.5f;
    public AudioSource enemySound;
    public AudioSource triggeredSound;
    public LayerMask whatIsPlayer;
    public float distance = 5f;

    [SerializeField] private Transform topCheck, bottomCheck, leftCheck, rightCheck;
    [SerializeField] bool CR_isRunning = false;
    private PlayerMovement player;
    private GameManager gameManager;


    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    private void Update()
    {
        CanSeePlayer();
    }


    void CanSeePlayer()
    {
        if (CR_isRunning)
            return;

        Vector3 currentAngle = Vector3.zero;
        RaycastHit2D hitInfo = new RaycastHit2D();

        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
                currentAngle = transform.up;
            if (i == 1)
                currentAngle = transform.right;
            if (i == 2)
                currentAngle = -transform.up;
            if (i == 3)
                currentAngle = -transform.right;


            hitInfo = Physics2D.Raycast(transform.position, currentAngle, distance, whatIsPlayer);

            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    Debug.DrawRay(transform.position, currentAngle * distance, Color.green);
                    StartCoroutine(Move(currentAngle));
                }
            }
            else
            {
                Debug.DrawRay(transform.position, currentAngle * distance, Color.red);
            }
        }
    }


    IEnumerator Move(Vector3 direction)
    {
        triggeredSound.PlayOneShot(triggeredSound.clip);

        CR_isRunning = true;
        Vector3 target = player.targetPos;
        while (CR_isRunning && transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;

        CR_isRunning = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.TriggerDeath();
    }
}
