using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float fireRate, maxFireRate, reloadTime, bulletSpeed;
    public int bulletMag, maxMag, maxRicochet, burstAmt, maxBurst;

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        bulletMag = maxMag;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fireRate <= 0)
        {
            GunShoot();
            if (bulletMag == 0)
            {
                fireRate = reloadTime;
            }
        }
        if (bulletMag < maxMag && fireRate >= 0)
        {
            fireRate -= Time.deltaTime;
        }
        if (fireRate <= 0 && bulletMag <= 0) { bulletMag = maxMag; }
    }
    private void GunShoot()
    {
        if (bulletMag >= 0 && fireRate <= 0)
        {
            if (maxBurst == 1)
            {
                Instantiate(bullet, (transform.position + transform.forward), transform.rotation);
            }
            else if (maxBurst > 1)
            {
                StartCoroutine("Burst");
            }
            bulletMag--;
            if (burstAmt >= maxBurst) { fireRate = maxFireRate; }
        }
    }
    IEnumerator Burst()
    {
        for (burstAmt = 0; burstAmt < maxBurst; burstAmt++)
        {
            Instantiate(bullet, (transform.position + transform.forward), transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
