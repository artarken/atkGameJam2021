using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { orb, tunnel, door, sign, lever }
public class Target : MonoBehaviour
{

    public TargetType targetType;
    public bool isActive;

    //Orb Target
    public int regionID;

    //Tunnel Target
    public Target otherEnd;

    //Helper Text (If applicable)
    public string helpersLineActive;
    public string helpersLineNotActive;

    //Door Target
    public Door thisDoor;

    //Switch Target
    public Lever thisSwitch;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
