using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{

    public Joystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;

    public int dirX = 0;
    public int dirY = 0;

    float stickX = 0;
    float stickY = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        dirX = 0;
        dirY = 0;

        stickX = movementJoystick.Direction.x;
        stickY = movementJoystick.Direction.y;

        // Top right quadrant
        if (movementJoystick.Direction.x > 0 && movementJoystick.Direction.y > 0)
        {
            if (movementJoystick.Direction.x > movementJoystick.Direction.y)
            {

                //rb.linearVelocity = new Vector2(dirX * playerSpeed, dirY);

                dirX = 1;
                dirY = 0;
            }
            else
            {

                dirX = 0;
                dirY = 1;
            }
        }

        // Bottom right quadrant
        if (movementJoystick.Direction.x > 0 && movementJoystick.Direction.y < 0)
        {
            if (stickX > -stickY)
            {
                dirX = 1;
                dirY = 0;
            }
            else
            {
                dirX = 0;
                dirY = -1;
            }
        }

        // Bottom left quadrant
        if (movementJoystick.Direction.x < 0 && movementJoystick.Direction.y < 0)
        {
            if (stickX > stickY)
            {


                dirX = 0;
                dirY = -1;
            }
            else
            {
                dirX = -1;
                dirY = 0;
            }
        }

        // Top left quadrant
        if (movementJoystick.Direction.x < 0 && movementJoystick.Direction.y > 0)
        {
            if (-stickX < stickY)
            {


                dirX = 0;
                dirY = -1;
            }
            else
            {
                dirX = -1;
                dirY = 0;
            }
        }
    }
}
