using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public void PlayAgainButton()
    {
        SceneManager.LoadScene("PreStory");
    }
    
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
