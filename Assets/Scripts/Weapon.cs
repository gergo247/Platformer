using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask whatToHit;

    public Transform TrailPrefab;
    

    float timeToFire = 0;
    Transform firePoint;
    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint");
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Shoot();

        //single hit
        if (fireRate == 0)
        {
            //mouse fire
         //   if (Input.GetKeyDown//(KeyCode.Mouse1.ToString()))
         //   {
         //       Shoot();
         //
         //   }

            //basic fire
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        //cont hit
        else
        {
            if (Input.GetButton("Fire1")&& Time.time >timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2((Camera.main.ScreenToWorldPoint(Input.mousePosition).x),(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

        Effect();
        

        //  Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition),Color.cyan);
        if (hit.collider != null)
        {
          //  Debug.Log("Enemy hit");
            //  Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemy enemy = hit.collider.GetComponent<Enemy>();

         //   bool letezik = (enemy != null);
          //  Debug.Log();

            if (enemy != null)
            {
                enemy.DamageEnemy(Damage);
                //     Debug.Log(string.Format("Enemy Damaged, Damage [0], Health rem[1]", Damage, enemy.stats.Health));
            }
            else
            {
                Destroy(TrailPrefab);
            }
        }
        
    }
    void Effect()
    {
        //todo bal jobb
        Instantiate(TrailPrefab, firePoint.position, firePoint.rotation);
    }
}
