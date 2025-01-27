using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            GameO();
        }
    }

    public void GameO()
    {
        gameOverUI.SetActive(true);
        
        Time.timeScale = 0f;
    }
}
