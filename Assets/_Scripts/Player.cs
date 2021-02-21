﻿using System.Collections;
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

    public float reachDistance;

    //variables for movement
    public InputAction moveAction;
    public float speed;
    private Rigidbody2D rb;

    //variables for UI Interactions
    public GameObject txtOrbType;
    public GameObject txtHelper;
    public GameObject imgHelperBubble;

    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody2D>();
        moveAction.Enable();
    }

    private void FixedUpdate()
    {
        if (moveAction.ReadValue<Vector2>() != Vector2.zero)
        {
            rb.velocity = moveAction.ReadValue<Vector2>()*speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.CompareTag("Room"))
        {

        }
        else if (c.transform.CompareTag("Plate"))
        {

        }
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
        Debug.Log(selectedOrb);
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
        ShowHelperText(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()).ToString());
        Debug.Log(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        Debug.Log("Distance: " + Vector2.Distance(this.transform.position, mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue())));

        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);

        if (hit)
        {
            if (hit.transform.CompareTag("Target") || hit.transform.CompareTag("Orb Target"))
            {
                Target t = hit.transform.GetComponent<Target>();
                Debug.Log(hit.transform.name);

                switch (t.targetType)
                {
                    case TargetType.door:
                        if(Vector2.Distance(this.transform.position, mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue())) <= reachDistance)
                        {
                            if (t.thisDoor.isLocked)
                            {
                                ShowHelperText(t.helpersLineActive[0]);
                            }
                            else if (t.thisDoor.isOpen)
                            {
                                ShowHelperText(t.helpersLineNotActive);
                            }
                            else
                            {
                                t.thisDoor.Open();
                                ShowHelperText(t.helpersLineOther);
                            }
                        }
                        else
                        {
                            ShowHelperText("You can't reach that");
                        }
                        break;
                    case TargetType.lever:
                        if (Vector2.Distance(this.transform.position, mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue())) <= reachDistance)
                        {
                            if (t.thisLever.isPressed)
                            {
                                ShowHelperText(t.helpersLineNotActive);
                            }
                            else
                            {
                                UseLever(t.thisLever);
                                ShowHelperText(t.helpersLineActive[0]);
                            }
                        }
                        else
                        {
                            ShowHelperText("You can't reach that");
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
                        else if (selectedOrb == OrbType.None)
                        {
                            ShowHelperText(t.helpersLineOther);
                        }
                        else
                        {
                            ThrowOrb(t);
                            ShowHelperText(t.helpersLineActive[(int)selectedOrb]);
                        }
                        break;
                    case TargetType.sign:
                        if (Vector2.Distance(this.transform.position, mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue())) <= reachDistance)
                        {

                            ShowHelperText(t.helpersLineActive[0]);
                        }
                        else
                        {
                            ShowHelperText("You can't reach that");
                        }
                        break;
                    case TargetType.tunnel:
                        if (Vector2.Distance(this.transform.position, mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue())) <= reachDistance)
                        {
                            UseTunnel(t.otherEnd);
                        }
                        else
                        {
                            ShowHelperText("You can't reach that");
                        }
                        break;
                }
            }
        }
    }

    void UseLever(Lever l)
    {
        l.isPressed = true;
        l.unlockTarget.Unlock();
        l.Press();
    }

    void UseTunnel(Target exit)
    {
        this.transform.position = new Vector3(exit.transform.position.x, exit.transform.position.y);
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

        Invoke("ClearHelperText", 15);
    }

    void ClearHelperText()
    {
        txtHelper.GetComponent<Text>().text = "";
        txtHelper.SetActive(false);
        imgHelperBubble.SetActive(false);
    }
}
