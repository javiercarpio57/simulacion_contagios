using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownianMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }

    private void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime * speed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        var wall = collision.gameObject.GetComponent<Wall>();

        if (wall != null)
        {
            if ((wall.side == WallSide.LEFT) || (wall.side == WallSide.RIGHT))
            {
                moveDirection.x = moveDirection.x * -1;
            }
            else if ((wall.side == WallSide.DOWN) || (wall.side == WallSide.UP))
            {
                moveDirection.z = moveDirection.z * -1;
            }
        }

        var person = collision.gameObject.GetComponent<Person>();
        if (person != null)
        {
            moveDirection = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
    }
}
