using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement
    float horizontal;
    float vertical;
    public float movementSpeed;
    Vector2 movementVector;
    public static Vector2 globalMovementVector;
    // Start is called before the first frame update

    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        movementVector = new Vector2(horizontal, vertical);
        globalMovementVector = movementVector;
        if (movementVector.magnitude > 0)
        {
            rigidbody2D.velocity = movementSpeed * movementVector.normalized;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
        
    }
}
