using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RingManager.Instance.AddRing(1);
            AudioManager.Instance.PlaySFX(clip);
            Destroy(gameObject);
            
        }
    }
}
