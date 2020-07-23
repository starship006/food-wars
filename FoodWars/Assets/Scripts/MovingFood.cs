using UnityEngine;

public class MovingFood : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;
    Rigidbody2D rigidbody2d;

    public int moveSpeed;

    // Update is called once per frame

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        FindEnemy();
    }
    void FixedUpdate()
    {        
        Vector3 moveVector = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        rigidbody2d.MovePosition(moveVector);
    }
    
    void FindEnemy()
    {
        target = GameObject.FindGameObjectWithTag("Enemy");
    }
}
