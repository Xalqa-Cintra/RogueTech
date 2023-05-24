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
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * player.GetComponent<Gun>().bulletSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisions >= player.GetComponent<Gun>().maxRicochet) { Destroy(gameObject); }
        rb.AddRelativeForce(Vector3.forward * (player.GetComponent<Gun>().bulletSpeed/2), ForceMode.Impulse);
        collisions++;
    }
}
