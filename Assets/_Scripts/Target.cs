using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { orb, tunnel, door, sign, lever }
public class Target : MonoBehaviour
{

    public TargetType targetType;

    //Orb Target
    public int regionID;

    //Tunnel Target
    public Target otherEnd;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
