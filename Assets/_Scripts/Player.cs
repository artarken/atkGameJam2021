using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum OrbType { None = -1, Past, Future }
public enum AudioIndex { door, lever, locked, plate, vent, important}
public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject mainWorld;
    public GameObject thisSprite;
    public List<AudioSource> audioSources;

    public bool pastOrbOut;
    public bool futureOrbOut;
    public OrbType selectedOrb;
    public List<Orb> Orbs;

    [SerializeField]
    //private bool canChangeOrb = true;

    public float reachDistance;

    //variables for movement
    public InputAction moveAction;
    public float speed;
    private Rigidbody2D rb;

    //variables for UI Interactions
    public GameObject txtOrbType;
    public GameObject txtHelper;
    public GameObject imgHelperBubble;
    public GameObject contGameOver;
    public Text txtFinishTime;

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
            audioSources[(int)AudioIndex.plate].Play();
            if (c.transform.GetComponent<Target>().thisLever.isPressed)
            {
                ShowHelperText(c.transform.GetComponent<Target>().helpersLineNotActive);
            }
            else
            {
                UseLever(c.transform.GetComponent<Target>().thisLever);
                ShowHelperText(c.transform.GetComponent<Target>().helpersLineActive[0]);
            }
        }
        else if (c.transform.CompareTag("Finish"))
        {
            audioSources[(int)AudioIndex.vent].Play();
            moveAction.Disable();
            contGameOver.SetActive(true);
            txtFinishTime.text = Time.timeSinceLevelLoad + " Seconds";
        }
    }

    //Input controls
    public void OnExit()
    {
        Application.Quit();
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
                                audioSources[(int)AudioIndex.locked].Play();
                                ShowHelperText(t.helpersLineActive[0]);
                            }
                            else if (t.thisDoor.isOpen)
                            {
                                ShowHelperText(t.helpersLineNotActive);
                            }
                            else
                            {
                                audioSources[(int)AudioIndex.door].Play();
                                t.thisDoor.Open();
                                ShowHelperText(t.helpersLineOther);
                            }
                        }
                        else
                        {
                            audioSources[(int)AudioIndex.important].Play();
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
                                audioSources[(int)AudioIndex.lever].Play();
                                UseLever(t.thisLever);
                                ShowHelperText(t.helpersLineActive[0]);
                            }
                        }
                        else
                        {
                            audioSources[(int)AudioIndex.important].Play();
                            ShowHelperText("You can't reach that");
                        }
                        break;
                    case TargetType.orb:
                        LayerMask mask = LayerMask.GetMask("Wall");

                        if (Physics2D.Linecast(transform.position, t.transform.position, mask))
                        {
                            audioSources[(int)AudioIndex.important].Play();
                            ShowHelperText("The Orbs can't move through solid walls");
                        }
                        else if (t.isActive)
                        {
                            audioSources[(int)AudioIndex.important].Play();
                            ShowHelperText("There is already an orb over there, you need to recall it to replace it.");
                        }
                        else if (selectedOrb == OrbType.None)
                        {
                            ShowHelperText(t.helpersLineOther);
                        }
                        else if (Orbs[(int)selectedOrb].isActive)
                        {
                            audioSources[(int)AudioIndex.important].Play();
                            ShowHelperText("You have to recall the orb before you can use it again");
                        }
                        else
                        {
                            audioSources[(int)AudioIndex.important].Play();
                            ThrowOrb(t);
                            ShowHelperText(t.helpersLineActive[(int)selectedOrb]);
                        }
                        break;
                    case TargetType.sign:
                        if (Vector2.Distance(this.transform.position, mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue())) <= reachDistance)
                        {
                            audioSources[(int)AudioIndex.important].Play();
                            ShowHelperText(t.helpersLineActive[0]);
                        }
                        else
                        {
                            audioSources[(int)AudioIndex.important].Play();
                            ShowHelperText("You can't reach that");
                        }
                        break;
                    case TargetType.tunnel:
                        if (Vector2.Distance(this.transform.position, hit.transform.position) <= 0.5f)
                        {
                            audioSources[(int)AudioIndex.vent].Play();
                            UseTunnel(t.otherEnd);
                        }
                        else
                        {
                            audioSources[(int)AudioIndex.important].Play();
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
        Orb o = Orbs[(int)selectedOrb];
        //presentMap.ModifiedRegions[target.regionID].SetActive(false);
        //timeMap.ModifiedRegions[target.regionID].SetActive(true);
        o.curentTarget = target;
        o.isActive = true;
        o.moveTarget = target.transform.position;
        o.isMoving = true;
        o.transform.SetParent(mainWorld.transform);
        //canChangeOrb = true;
    }

    void RecallOrb()
    {
        
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
