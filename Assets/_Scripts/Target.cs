using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { orb, tunnel, door }
public class Target : MonoBehaviour
{

    public TargetType targetType;

    //Orb Target
    public int orbSize;

    //Tunnel Target
    public Target otherEnd;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
