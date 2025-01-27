using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticaleDelete : MonoBehaviour
{
    public float cleanupDistance = 10f; 
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerTransform == null) return;

       
        if (transform.position.z < playerTransform.position.z - cleanupDistance)
        {
            Destroy(gameObject);
        }
    }
}
