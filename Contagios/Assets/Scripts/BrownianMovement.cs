using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownianMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 0.5f;
    public float rateContagio = 0.05f;

    private Rigidbody rb;

    private Person me;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDirection = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        me = GetComponent<Person>();
    }

    private void FixedUpdate()
    {
        if ((me.status == PersonStatus.HEALTHY) || (me.status == PersonStatus.SICK) || (me.status == PersonStatus.RECOVERED))
        {
            rb.velocity = moveDirection * speed;
        }
        else
        {
            rb.velocity = moveDirection * 0;
        }
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
            if (((person.status == PersonStatus.SICK) || (person.status == PersonStatus.TREATMENT)) && (me.status == PersonStatus.HEALTHY))
            {
                if (Random.value < rateContagio)
                {
                    me.ChangeStatus(PersonStatus.SICK);
                }
            }
            moveDirection = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
    }
}
