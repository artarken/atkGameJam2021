using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { orb, tunnel, door, sign, lever, hole }
public class Target : MonoBehaviour
{

    public TargetType targetType;
    public bool isActive = true;
    public bool hasAudio;
    AudioSource audio;

    //Orb Target
    public int regionID;

    //Tunnel Target
    public Target otherEnd;

    //Helper Text (If applicable)
    public List<string> helpersLineActive;
    public string helpersLineNotActive;
    public string helpersLineOther;

    //Door Target
    public Door thisDoor;

    //Switch Target
    public Lever thisLever;

    void Start()
    {
        if (targetType == TargetType.door)
        {
            thisDoor = this.transform.GetComponent<Door>();
        }
        else if (targetType == TargetType.lever)
        {
            thisLever = this.transform.GetComponent<Lever>();
        }
    }

    void Update()
    {
        
    }

    public void Unlock()
    {
        if (thisDoor.isLocked)
        {
            thisDoor.Unlock();
        }
    }
}
