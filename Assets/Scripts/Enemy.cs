using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{

    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        public int ScoreValue = 10;
        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 20;

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public EnemyStats stats = new EnemyStats();
    

    public Transform deathParticles;
    public float deathShakeAmount = 0.1f;
    public float deathShakeLength = 0.1f;

    public int killReward = 10;
    private bool collided = false;
    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
        if (deathParticles == null)
        {
          //  Debug.LogError("No death particles referenced on Enemy");
        }
    }
    

    public void DamageEnemy(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }
   // float timeSinceLastCall = 0;
    void OnCollisionEnter2D(Collision2D _colInfo)
    {

       //timeSinceLastCall += Time.deltaTime;
       //Debug.Log("Time.deltaTime" + Time.deltaTime);
       //Debug.Log("timeSinceLastCall" + timeSinceLastCall);
       // if (timeSinceLastCall >= 0.01f)
       if (!collided)
        {
            Player _player = _colInfo.collider.GetComponent<Player>();
            if (_player != null)
            {
                collided = true;

                Debug.Log("OnCollisionEnter2D");
                _player.DamagePlayer(stats.damage);
                DamageEnemy(99999);
            }
       //     timeSinceLastCall = 0;   // reset timer back to 0
        }
    }
    
}