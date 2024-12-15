using System.Collections;
using UnityEngine;
using TMPro;

public class StorySceneManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public TextMeshProUGUI popupText;
    public string fullText;
    public float textSpeed = 0.05f;

    private bool isTextComplete = false;

    void Start()
    {
        StartCoroutine(ShowTextGradually());
    }

    void Update()
    {
        if (isTextComplete && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
        }
    }

    IEnumerator ShowTextGradually()
    {
        storyText.text = "";
        foreach (char c in fullText)
        {
            storyText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTextComplete = true;
        StartCoroutine(ShowPopupText());
    }

    IEnumerator ShowPopupText()
    {
        popupText.gameObject.SetActive(true);
        Color popupColor = popupText.color;
        popupColor.a = 0;
        popupText.color = popupColor;

        
        while (popupText.color.a < 1)
        {
            popupColor.a += Time.deltaTime;
            popupText.color = popupColor;
            yield return null;
        }
    }

    void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
