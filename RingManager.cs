using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingManager : MonoBehaviour
{
    public static RingManager Instance;

    [Header("Ring Settings")]
    public int ringCount = 0;
    public Text ringCounterUI;
    public GameObject ringPrefab;
    public Transform playerTransform; 

    [Header("Recovery Settings")]
    public float invincibilityDuration = 3f; 
    private bool isInvincible = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddRing(int amount)
    {
        ringCount += amount;
        UpdateUI();
    }

    public void OnPlayerHit()
    {
        if (isInvincible) return; 

        if (ringCount > 0)
        {
            RemoveRingsOnHit(ringCount);
            
            StartCoroutine(InvincibilityCoroutine());
        }
        else
        {
            GameOver();
        }
    }

    private void RemoveRingsOnHit(int amount)
    {
        int lostRings = Mathf.Min(amount, ringCount);
        ringCount -= lostRings;
        UpdateUI();

        // Drop the rings in the scene
        StartCoroutine(DropRings(lostRings));
    }

    private void UpdateUI()
    {
        if (ringCounterUI)
            ringCounterUI.text = "Rings: " + ringCount;
    }

    private IEnumerator DropRings(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject ring = Instantiate(ringPrefab, playerTransform.position, Quaternion.identity);
            Rigidbody rb = ring.GetComponent<Rigidbody>();
            if (rb)
            {
                Vector3 randomDirection = Random.insideUnitSphere + Vector3.up;
                rb.AddForce(randomDirection * 5f, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(0.05f); 
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        
        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isInvincible = false;
    }

    private void GameOver()
    {
        Debug.Log("Game Over! No rings left.");
        Time.timeScale = 0f; 
    }
}
