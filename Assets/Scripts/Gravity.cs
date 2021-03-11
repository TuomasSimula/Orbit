using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private const float GRAV = 6.67408f;
    private GameObject[] gravityBodies;

    public float mass;
    public Vector2 startingPoint;
    public Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = (Vector3)startingPoint;
        rb.velocity = (Vector3)velocity;
        rb.mass = mass;

        gravityBodies = GameObject.FindGameObjectsWithTag("GravityObject");
    }

    // FixedUpdate is called on every physics update
    private void FixedUpdate()
    {
        rb.AddForce(CalculateGravity());
    }

    private Vector2 CalculateGravity ()
    {
        Vector2 netGravity = Vector2.zero;
        foreach(GameObject gravityBody in gravityBodies)
        {
            if (gravityBody != gameObject)
            {
                Gravity grav2 = gravityBody.GetComponent<Gravity>();
                Rigidbody2D rb2 = gravityBody.GetComponent<Rigidbody2D>();

                Vector2 distanceVector = rb2.transform.position - rb.transform.position;
                float r = distanceVector.magnitude;
                float gravMagnitude = (GRAV * grav2.mass) / (r * r);
                Vector2 gravVector = gravMagnitude * distanceVector.normalized;

                //Debug.Log(gravMagnitude * distanceVector.normalized);

                netGravity += gravVector;
            }
        }
        Debug.Log(netGravity);
        return netGravity;
    }
}