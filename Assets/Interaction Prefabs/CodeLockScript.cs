using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Text;

public class CodeLockScript : MonoBehaviour
{
    public UnityEvent<string> onRightAnswer;
    public UnityEvent<string> onWrongAnswer;

    public int CodeLength = 4;

    public string CorrectCode = "1234";

    TextMeshProUGUI displayText; 

    [HideInInspector]
    public string code = "";

    public string doorTag = "Door"; // Tag for door GameObjects
    public string doorOpenTrigger = "Open"; // Trigger name for the door opening animation
    public string playerTag = "Player"; // Tag for player GameObject

    void Start()
    {
        displayText = transform.Find("Display").GetComponentInChildren<TextMeshProUGUI>();

        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(
                button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text
            ));
        }
    }

    void OnButtonClick(string name) {
        if (name == "<") {
            if (code.Length > 0) {
                code = code.Substring(0, code.Length - 1);
                UpdateDisplay();
            }
        }
        else if (name == "Ok") {
            CloseCodeLock();
            HandleCallback((code == CorrectCode), code);            
        }
        else if (name == "Close") {
            CloseCodeLock();
        }
        else if (code.Length < CodeLength) {
            code = code + name;
            UpdateDisplay();
        }
    }

    void HandleCallback(bool answerWasRight, string code) {
        if (answerWasRight) {
            onRightAnswer.Invoke(code);
            Animator doorAnimator = FindClosestDoorAnimator();
            if (doorAnimator != null) {
                doorAnimator.SetTrigger(doorOpenTrigger); // Trigger the door opening animation
            }
        } else {
            onWrongAnswer.Invoke(code);
        }
    }

    Animator FindClosestDoorAnimator() {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null) {
            Debug.LogWarning("No player found with tag: " + playerTag);
            return null;
        }

        GameObject[] doors = GameObject.FindGameObjectsWithTag(doorTag);
        GameObject closestDoor = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject door in doors) {
            float distance = Vector3.Distance(player.transform.position, door.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestDoor = door;
            }
        }

        if (closestDoor != null) {
            return closestDoor.GetComponent<Animator>();
        }

        Debug.LogWarning("No doors found with tag: " + doorTag);
        return null;
    }

    void CloseCodeLock() {
        Canvas parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null) {
            parentCanvas.gameObject.SetActive(false);
        }
    }

    void UpdateDisplay() {
        string paddedCode = code.PadRight(CodeLength, '_');

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < paddedCode.Length; i++) {
            stringBuilder.Append(paddedCode[i]);
            if (i != paddedCode.Length - 1)
                stringBuilder.Append(' ');
        }

        displayText.text = stringBuilder.ToString();
    }
}