using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask wall;
    public Transform playerGFX;

    private bool isMoving;
    [HideInInspector] public  Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;

    [SerializeField] private Transform topCheck, bottomCheck, leftCheck, rightCheck;


    bool wallTop, wallBottom, wallLeft, wallRight;


    void Update()
    {
        wallTop = CheckWall(topCheck);
        wallBottom = CheckWall(bottomCheck);
        wallLeft = CheckWall(leftCheck);
        wallRight = CheckWall(rightCheck);


        if (Input.GetKey(KeyCode.W) && !isMoving && !wallTop)
        {
            playerGFX.eulerAngles = new Vector3(0, 0, 0);
            StartCoroutine(MovePlayer(Vector3.up));
        }

        if (Input.GetKey(KeyCode.A) && !isMoving && !wallLeft)
        {
            playerGFX.eulerAngles = new Vector3(0, 0, 90);
            StartCoroutine(MovePlayer(Vector3.left));
        }

        if (Input.GetKey(KeyCode.S) && !isMoving && !wallBottom)
        {
            playerGFX.eulerAngles = new Vector3(0, 0, 180);
            StartCoroutine(MovePlayer(Vector3.down));
        }

        if (Input.GetKey(KeyCode.D) && !isMoving && !wallRight)
        {
            playerGFX.eulerAngles = new Vector3(0, 0, 270);
            StartCoroutine(MovePlayer(Vector3.right));
        }
    }

    bool CheckWall(Transform checkPos)
    {
        //If hit something return true

        if (Physics2D.OverlapCircle(checkPos.position, 0.1f, wall))
            return true;
        else
            return false;
    }

    private IEnumerator MovePlayer (Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0f;
        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(topCheck.position, 0.1f);
        Gizmos.DrawWireSphere(leftCheck.position, 0.1f);
        Gizmos.DrawWireSphere(rightCheck.position, 0.1f);
        Gizmos.DrawWireSphere(bottomCheck.position, 0.1f);
    }
}
