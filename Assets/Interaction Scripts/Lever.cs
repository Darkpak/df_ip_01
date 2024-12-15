using UnityEngine;

public class Lever : PlayerActivatable
{
    public bool isActivated = false;
    public LeverManager manager;

    protected override void OnActivate()
    {
        if (!isActivated)
        {
            isActivated = true;
            Debug.Log("Lever: Activated " + gameObject.name);
            // Notify the manager that this lever has been activated
            manager.CheckLevers();
        }
    }

    public override bool IsAvailable()
    {
        return !isActivated;
    }
}