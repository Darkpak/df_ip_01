using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public List<Lever> levers;
    public GameObject door;
    private Animator doorAnimator;

    void Start()
    {
        foreach (var lever in levers)
        {
            lever.manager = this;
        }
        doorAnimator = door.GetComponent<Animator>();
    }

    public void CheckLevers()
    {
        foreach (var lever in levers)
        {
            if (!lever.isActivated)
            {
                return;
            }
        }
        OpenDoor();
    }

    private void OpenDoor()
    {
        // Trigger the door opening animation
        doorAnimator.SetTrigger("Open");
    }
}