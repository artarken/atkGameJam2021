using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrbType { none, past, future }
public class Player : MonoBehaviour
{
    public bool pastOrbOut;
    public bool futureOrbOut;
    public OrbType selectedOrb;

    //variables for movement
    public float maxSpeed;
    public float acceleration;

    void Start()
    {
        
    }

    void Update()
    {
        //keyboard cotrols
    }

    //used for physics movement
    private void FixedUpdate()
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
            case OrbType.future:
                break;
            case OrbType.past:
                break;
        }
    }

    void RecallOrb()
    {
        switch (selectedOrb)
        {
            case OrbType.future:
                break;
            case OrbType.past:
                break;
        }
    }
}
