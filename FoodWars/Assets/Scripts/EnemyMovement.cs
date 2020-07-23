using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    float timerCount;
    public float moveSpeed;
    Vector2 moveDirection;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {        
        if(timerCount < 0)
        {
            ChangeDirection();
            timerCount = 4;
        }
        else
        {
            timerCount -= Time.deltaTime;
        }
        transform.position += (Vector3)moveDirection * Time.deltaTime * moveSpeed;
    }

    void ChangeDirection()
    {
        moveDirection = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)).normalized;
    }
}
