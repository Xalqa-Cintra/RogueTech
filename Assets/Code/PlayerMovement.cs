using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    public float rotationSpeed;
    public float maxSpeed;
    public Rigidbody rb;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    bool isOnGround;

    [Header("Stats")]
    public float currentBulletTimer, maxBulletTimer, timeSlow, jumpForce, walkSpeed, runSpeed, fireRate;
    public int bulletMag;

    [Header("TP System")]
    public GameObject[] teleports;

    [Header("Randomisers")]
    public int roomTP, maxRooms, lootRarity, roomsCleared;

    public Transform LootEntrance;
    // Start is called before the first frame update
    void Start()
    {
        teleports = GameObject.FindGameObjectsWithTag("Room Spawn");
        maxRooms += (teleports).Count();
    }

    // Update is called once per frame
    void Update()
    {   //movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection * maxSpeed * Time.deltaTime, Space.World);
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);

        //bulletTime
        if (Input.GetMouseButtonDown(1)) 
        {
            BulletTime();
        

        }
        if (Time.timeScale == timeSlow)
        {
            currentBulletTimer += Time.deltaTime;
        }
        else if (Time.timeScale == 1 && currentBulletTimer > 0) { currentBulletTimer -= Time.deltaTime; }
        if (currentBulletTimer > maxBulletTimer)
        {
            currentBulletTimer = 0;
            Time.timeScale = 1;
        }
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            maxSpeed = runSpeed;
        } else
        {
            maxSpeed = walkSpeed;
        }
    }
    
    private void BulletTime()
    {
       
            if (Time.timeScale == 1) {Time.timeScale = timeSlow; }
            else {Time.timeScale = 1; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Loot Door")
        {
            transform.position = LootEntrance.position;
            roomsCleared++;
        }
        if (collision.gameObject.tag == "Room Door")
        {
            roomTP = Random.Range(0, maxRooms);
            transform.position = teleports[roomTP].transform.position;
        }
    }

    private void Gun()
    {

        //for bullet system, have a bullet mag, instantiate when clicked, bullet count limits amount spawned, 
    }

}
