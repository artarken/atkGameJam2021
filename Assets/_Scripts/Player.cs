using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum OrbType { None = -1, Past, Future }
public class Player : MonoBehaviour
{
    public Camera mainCamera;

    public bool pastOrbOut;
    public bool futureOrbOut;
    public OrbType selectedOrb;
    public List<Orb> Orbs;

    //variables for movement
    public float maxSpeed;
    public float acceleration;

    //variables for UI Interactions
    public GameObject txtOrbType;
    public GameObject txtHelper;
    public GameObject imgHelperBubble;

    void Start()
    {
        
    }

    //Input controls
    public void OnMove()
    {

    }
    
    public void OnOrbSwitch()
    {
        switch (selectedOrb)
        {
            case OrbType.None:
                selectedOrb = OrbType.Past;
                break;
            case OrbType.Past:
                selectedOrb = OrbType.Future;
                break;
            case OrbType.Future:
                selectedOrb = OrbType.None;
                break;
        }
        txtOrbType.GetComponent<Text>().text = selectedOrb.ToString();
    }

    public void OnRecall()
    {
        if (selectedOrb != OrbType.None)
        {
            if (Orbs[(int)selectedOrb].isActive)
            {
                Orbs[(int)selectedOrb].Recall();
            }
        }
    }

    public void OnUse()
    {
        Debug.Log(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);

        if (hit)
        {
            if (hit.transform.CompareTag("Target") || hit.transform.CompareTag("Orb Target"))
            {
                Target t = hit.transform.GetComponent<Target>();

                switch (t.targetType)
                {
                    case TargetType.door:
                        break;
                    case TargetType.lever:
                        if (t.thisLever.isPressed)
                        {
                            ShowHelperText(t.helpersLineNotActive);
                        }
                        else
                        {
                            UseLever(t.thisLever);
                            ShowHelperText(t.helpersLineActive);
                        }
                        break;
                    case TargetType.orb:
                        LayerMask mask = LayerMask.GetMask("Wall");

                        if (Physics2D.Linecast(transform.position, t.transform.position, mask))
                        {
                            ShowHelperText("The Orbs can't move through solid walls");
                        }
                        else if (t.isActive)
                        {
                            ShowHelperText("There is already an orb over there, you need to recall it to replace it.");
                        }
                        else
                        {
                            ThrowOrb(t);
                            ShowHelperText(t.helpersLineActive);
                        }
                        break;
                    case TargetType.sign:
                        ShowHelperText(t.helpersLineActive);
                        break;
                    case TargetType.tunnel:
                        UseTunnel(t.otherEnd);
                        break;
                }
            }
        }
    }

    void UseLever(Lever l)
    {
        l.isPressed = true;
        l.unlockTarget.Unlock();
    }

    void UseTunnel(Target exit)
    {

    }

    void ThrowOrb(Target target)
    {
        switch (selectedOrb)
        {
            case OrbType.Future:
                break;
            case OrbType.Past:
                break;
        }
    }

    void RecallOrb()
    {
        switch (selectedOrb)
        {
            case OrbType.Future:
                break;
            case OrbType.Past:
                break;
        }
    }

    public void ShowHelperText(string helperText)
    {
        txtHelper.SetActive(true);
        imgHelperBubble.SetActive(true);
        txtHelper.GetComponent<Text>().text = helperText;
    }

    void ClearHelperText()
    {
        txtHelper.GetComponent<Text>().text = "";
        txtHelper.SetActive(false);
        imgHelperBubble.SetActive(false);
    }
}
