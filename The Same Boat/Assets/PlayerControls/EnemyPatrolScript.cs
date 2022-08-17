using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolScript : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";

    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDistance;

    string facingDirection;

    Vector3 baseScale;

    Rigidbody2D rb;
    float moveSpeed = 5;

    void Start()
    {
        baseScale = transform.localScale;

        facingDirection = RIGHT;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float vX = moveSpeed;

        if(facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }

        rb.velocity = new Vector2(vX, rb.velocity.y);

        if (IsHittingWall())
        {
            if (facingDirection == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else if (facingDirection == RIGHT)
            {
                ChangeFacingDirection(LEFT);
            }
        }
    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
    }


    bool IsHittingWall()
    {
        bool val = false;

        float castDistance = baseCastDistance;

        //Define the cast distance for left and right
        if (facingDirection == LEFT)
        {
            castDistance = -baseCastDistance;
        }
        else
        {
            castDistance = baseCastDistance;
        }

        //Determine the target destination based on the cast distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDistance;

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Obstacle")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
}
