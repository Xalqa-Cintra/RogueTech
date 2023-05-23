using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int collisions;
    public GameObject bulletTarget, player;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        rb.AddRelativeForce(Vector3.forward * player.GetComponent<PlayerMovement>().bulletSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisions >= player.GetComponent<PlayerMovement>().maxRicochet) { Destroy(gameObject); }
        rb.AddRelativeForce(Vector3.forward * (player.GetComponent<PlayerMovement>().bulletSpeed/2), ForceMode.Impulse);
        collisions++;
    }
}
