using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCatMovement : PlayerMovement
{
    protected override void FixedUpdate()
    {
        bool jumpPressedLocal = jumpPressed;
        jumpPressed = false;
        base.FixedUpdate();
        jumpPressed = jumpPressedLocal;
        if (jumpPressed)
        {

        }
    }
}
