using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    HealthBar checkpointPos;
    public Transform respawnPoint;
    Collider2D coll;

    private void Awake()
    {
        checkpointPos = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            checkpointPos.UpdateCheckpoint(respawnPoint.position);
            coll.enabled = false;
        }
    }
}
