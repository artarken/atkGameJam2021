using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public bool isActive;
    public TimeMap timeMap;
    public TimeMap presentMap;

    public Sprite inSprite;
    public Sprite outSprite;

    public SpriteRenderer rend;

    public Target curentTarget;

    void Start()
    {
        rend = this.transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.transform.CompareTag("Orb Target"))
        {
            rend.sprite = outSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.transform.CompareTag("Orb Target"))
        {
            rend.sprite = inSprite;
        }
    }

    public void Recall()
    {
        timeMap.ModifiedRegions[curentTarget.regionID].SetActive(false);
        presentMap.ModifiedRegions[curentTarget.regionID].SetActive(true);
        curentTarget = null;
        isActive = false;
    }
}
