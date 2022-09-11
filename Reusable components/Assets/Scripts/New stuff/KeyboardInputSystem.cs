using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputSystem : MonoBehaviour
{
    public float HorizontalInput
    {
        get { return Input.GetAxisRaw("Horizontal"); }
    }

    public bool Sprinting
    {
        get { return Input.GetKey(KeyCode.LeftShift); }
    }

    public bool JumpDown
    {
        get { return Input.GetKeyDown(KeyCode.Space); }
    }
    public bool JumpHold
    {
        get { return Input.GetKey(KeyCode.Space); }
    }
}