using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorManagment : MonoBehaviour
{
      [Header("UI Panels")]
    public GameObject bluePanel; 
    public GameObject redPanel; 
    public GameObject greenPanel; 

    [Header("Game Settings")]
    public float initialSwitchInterval = 5f; 
    public float switchIntervalDecrease = 0.1f; 
    public float minSwitchInterval = 1f; 
    public float pauseBetweenSwitches = 2f; 
    public KeyCode blueKey = KeyCode.B; 
    public KeyCode redKey = KeyCode.R; 
    public KeyCode greenKey = KeyCode.G; 

    [Header("Game Over Settings")]
    public GameObject gameOverUI; 

    private string currentColor = "";
    private float currentSwitchInterval;
    private bool isGameOver = false;

    private void Start()
    {
        currentSwitchInterval = initialSwitchInterval;
        StartCoroutine(ColorSwitchRoutine());
    }

    private void Update()
    {
        if (isGameOver) return;
        
        if (Input.GetKeyDown(blueKey) && currentColor == "Blue")
        {
            CorrectColorSelected();
        }
        else if (Input.GetKeyDown(redKey) && currentColor == "Red")
        {
            CorrectColorSelected();
        }
        else if (Input.GetKeyDown(greenKey) && currentColor == "Green")
        {
            CorrectColorSelected();
        }
    }

    private IEnumerator ColorSwitchRoutine()
    {
        while (!isGameOver)
        {
            currentColor = GetRandomColor();
            
            ShowColorPanel(currentColor);
            
            float timer = currentSwitchInterval;
            bool correctColorPressed = false;

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                if (currentColor == "") 
                {
                    correctColorPressed = true;
                    break;
                }

                yield return null;
            }
            
            HideAllPanels();

            if (!correctColorPressed)
            {
                TriggerGameOver(); 
                yield break;
            }
            
            yield return new WaitForSeconds(pauseBetweenSwitches);
            
            currentSwitchInterval = Mathf.Max(minSwitchInterval, currentSwitchInterval - switchIntervalDecrease);
        }
    }

    private string GetRandomColor()
    {
        string[] colors = { "Blue", "Red", "Green" };
        return colors[Random.Range(0, colors.Length)];
    }

    private void ShowColorPanel(string color)
    {
        HideAllPanels(); 

        switch (color)
        {
            case "Blue":
                bluePanel.SetActive(true);
                break;
            case "Red":
                redPanel.SetActive(true);
                break;
            case "Green":
                greenPanel.SetActive(true);
                break;
        }
    }

    private void HideAllPanels()
    {
        bluePanel.SetActive(false);
        redPanel.SetActive(false);
        greenPanel.SetActive(false);
    }

    private void CorrectColorSelected()
    {
        currentColor = ""; 
        HideAllPanels(); 
    }

    private void TriggerGameOver()
    {
        isGameOver = true; 
        
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }
}
