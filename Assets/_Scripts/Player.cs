using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum OrbType { None, Past, Future }
public class Player : MonoBehaviour
{
    public bool pastOrbOut;
    public bool futureOrbOut;
    public OrbType selectedOrb;

    //variables for movement
    public float maxSpeed;
    public float acceleration;

    //variables for UI Interactions
    public GameObject txtOrbType;

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

        }
    }

    public void OnUse(Target target)
    {

    }

    void UseSwitch(Switch s)
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
