using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    public int moveSpeed = 5;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.fixedDeltaTime * moveSpeed);
        Destroy(this.gameObject, 1);
    }
}
