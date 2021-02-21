using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isPressed = false;
    public bool isPressurePlate;

    public Sprite pressedSprite;

    public SpriteRenderer rend;

    public Target unlockTarget;

    private void Start()
    {
        rend = this.transform.GetComponent<SpriteRenderer>();
    }

    public void Press()
    {
        isPressed = true;
        rend.sprite = pressedSprite;
    }
}
