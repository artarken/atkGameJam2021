using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked;
    public bool isOpen;

    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Sprite openSprite;

    public SpriteRenderer rend;
    public BoxCollider2D coll;

    private void Start()
    {
        rend = this.transform.GetComponent<SpriteRenderer>();
        coll = this.transform.GetComponent<BoxCollider2D>();
    }

    public void Unlock()
    {
        rend.sprite = unlockedSprite;
        isLocked = false;
    }

    public void Open()
    {
        rend.sprite = openSprite;
        coll.offset = new Vector2(0.4f, 0f);
        coll.size = new Vector2(0.25f, 1f);
    }
}
