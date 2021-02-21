using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateToMouse : MonoBehaviour
{
    void Update()
    {
        Vector3 mPos = Mouse.current.position.ReadValue();
        mPos.z = 0f;

        mPos.x -= Camera.main.WorldToScreenPoint(transform.position).x;
        mPos.y -= Camera.main.WorldToScreenPoint(transform.position).y;

        float ang = Mathf.Atan2(mPos.y, mPos.x) * Mathf.Rad2Deg;
        ang -= 90;
        transform.rotation = Quaternion.Euler(0, 0, ang);
    }
}
