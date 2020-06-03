using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;
        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        // public void Init()
        // {
        //     Debug.Log("Player Init Hp"  + curHealth + maxHealth);
        //     curHealth = maxHealth;
        // }
    }
    // [SerializeField]
    // private StatusIndicator statusIndicator;

    public PlayerStats stats;// = new PlayerStats();

    Animator animator;

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(AttackAnim());
        }
    }
    IEnumerator AttackAnim()
    {
        Debug.Log("Attack start");
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.08f);
        Debug.Log("Attack End");

        animator.SetBool("Attack", false);
    }


    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        animator = GetComponent<Animator>();


        stats = new PlayerStats();
        stats.curHealth = stats.maxHealth;
        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }
   // float timeSinceLastCall = 0;
    public void DamagePlayer(int damage)
    {
      //  timeSinceLastCall += Time.deltaTime*100;
      //  Debug.Log("Time.deltaTime"+ Time.deltaTime);
      //  Debug.Log("timeSinceLastCall" + timeSinceLastCall);

       // if (timeSinceLastCall >= 1)
       // {
            Debug.Log("DamagePlayer");

            stats.curHealth = stats.curHealth - damage;
            if (stats.curHealth <= 0)
            {
                Debug.Log("KillPlayer");

                GameMaster.KillPlayer(this);
            }
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);

       //     timeSinceLastCall = 0;   // reset timer back to 0
       // }

       

    }
    // public void Update()
    // { 
    //  if (Input.GetButtonDown("Fire1"))
    //         {
    //             DamagePlayer(25);
    //         }
    // }
}
