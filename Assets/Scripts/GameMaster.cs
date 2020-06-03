using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    [SerializeField]
    private int maxLives = 3;

    private static int _remainingLives;
    public static int RemainingLives { get { return _remainingLives; } }

    private static int _score = 0;
    public static int Score { get { return _score; } }




    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    void Start()
    {
        _remainingLives = maxLives;
        _score = 0;
    }
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnParticles;
    public Transform spawnPrefab;

    [SerializeField]
    private GameObject gameOverUI;




    public void EndGame()
    {
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
    }
    public IEnumerator _RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);



        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        //GameObject clone = Instantiate(spawnParticles, spawnPoint.position, spawnPoint.rotation) as GameObject;
        //Destroy(clone, 3f);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        _remainingLives--;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._killEnemy(enemy);

        _score += enemy.stats.ScoreValue;

    }

    public void _killEnemy(Enemy _enemy)
    {
        Destroy(_enemy.gameObject);
     //   Instantiate(_enemy.enemyDeathParticles, _enemy.transform.position, Quaternion.identity);
    }


}
