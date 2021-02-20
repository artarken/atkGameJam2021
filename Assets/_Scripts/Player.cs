using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum OrbType { None, Past, Future }
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
            if (hit.transform.CompareTag("Target"))
            {
                Target t = hit.transform.GetComponent<Target>();

                switch (t.targetType)
                {
                    case TargetType.door:
                        break;
                    case TargetType.lever:
                        break;
                    case TargetType.orb:
                        break;
                    case TargetType.sign:
                        break;
                    case TargetType.tunnel:
                        UseTunnel(t.otherEnd);
                        break;
                }
            }
        }
    }

    void UseSwitch(Lever s)
    {

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
}
