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
    public Player player;

    //orb movement
    [SerializeField]
    private float outSpeed;
    [SerializeField]
    private float inSpeed;
    public Vector2 moveTarget;
    public bool isMoving;
    public bool isReturning;

    void Start()
    {
        rend = this.transform.GetComponent<SpriteRenderer>();
        player = this.transform.parent.GetComponent<Player>();
    }

    void Update()
    {
        if (isMoving)
        {
            //Debug.Log("Moving: " + transform.position);

            float spd = outSpeed * Time.deltaTime;
            if (Vector2.Distance(transform.position, moveTarget) <= spd)
            {
                rend.sprite = outSprite;
                transform.position = moveTarget;
                isMoving = false;
                presentMap.ModifiedRegions[curentTarget.regionID].SetActive(false);
                timeMap.ModifiedRegions[curentTarget.regionID].SetActive(true);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, moveTarget, spd);
            }
        }
        if (isReturning)
        {
            //Debug.Log("Returning: " + transform.position);

            float spd = inSpeed * Time.deltaTime;
            moveTarget = player.transform.position;
            if(Vector2.Distance(transform.position, moveTarget) <= spd)
            {
                this.transform.SetParent(player.transform);
                transform.localPosition = Vector3.zero;
                isReturning = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, moveTarget, spd);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log(c.transform.tag.ToString());
        if(c.transform.CompareTag("Orb Target"))
        {
            rend.sprite = outSprite;
            if(c.transform.GetComponent<Target>() == curentTarget)
            {
                presentMap.ModifiedRegions[curentTarget.regionID].SetActive(false);
                timeMap.ModifiedRegions[curentTarget.regionID].SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        Debug.Log("I left");
        if (c.transform.CompareTag("Orb Target"))
        {
            rend.sprite = inSprite;
            if (c.transform.GetComponent<Target>() == curentTarget)
            {
                timeMap.ModifiedRegions[curentTarget.regionID].SetActive(false);
                presentMap.ModifiedRegions[curentTarget.regionID].SetActive(true);
            }
        }
    }

    public void Recall()
    {
        timeMap.ModifiedRegions[curentTarget.regionID].SetActive(false);
        presentMap.ModifiedRegions[curentTarget.regionID].SetActive(true);
        curentTarget = null;
        isActive = false;
        isMoving = false;
        isReturning = true;
        rend.sprite = inSprite;
    }
}
