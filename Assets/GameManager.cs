using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    void Update()
    {
        // Check if the dialogue system has the quest state variable
        if (dialogueRunner.VariableStorage.TryGetValue("$quest", out string quest))
        {
            // If the quest is complete, change the scene
            if (quest == "complete")
            {
                SceneManager.LoadScene("EndGame");
            }
        }
    }
}
    
