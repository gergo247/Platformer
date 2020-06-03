using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    //update path /s
    public float updateRate = 2f;


    //caching
    private Seeker seeker;
    private Rigidbody2D rb;
    // calculated path
    public Path path;

    //AI's speed /s
    public float speed = 300f;
    public ForceMode2D fMode;


    [HideInInspector]
    public bool pathIsEnded = false;

    //
    public float nextWayPointDistance = 3;


    //current waypoint we re moving towards
    private int currentWaypoint = 0;

    private bool searchingForPlayer = false;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }


            return;
        }

        //start new path to target
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator SearchForPlayer()
    {
        GameObject sResult =  GameObject.FindGameObjectWithTag("Player");
        if (sResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            target = sResult.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }
        else
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(UpdatePath());
        }
    }

    public void OnPathComplete(Path p)
    {
    //    Debug.Log("We got a path. error: " + p.error);

        //ok
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    //for physics
    void FixedUpdate()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        //TODO: look at player

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
          //  Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;
        //direction to next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        
        if (transform.position.x > target.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180,0);
        }


        //we re in fixed update
        dir *= speed * Time.fixedDeltaTime;

        //move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (dist < nextWayPointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
