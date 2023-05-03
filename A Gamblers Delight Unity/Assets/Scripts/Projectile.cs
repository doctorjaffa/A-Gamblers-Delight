using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Public variables
    public float force;

    // Private variables
    private Rigidbody2D rigidbody = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        // Set velocity of projectile
        rigidbody.velocity = new Vector2(transform.position.x, transform.position.y).normalized * force;
    }
}
