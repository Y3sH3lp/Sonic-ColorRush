using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverbtn : MonoBehaviour
{
    public void LoadSelectScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("selection"); 
    }
}
