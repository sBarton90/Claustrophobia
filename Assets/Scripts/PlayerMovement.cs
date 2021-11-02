using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform Player;

    float JumpPower = 4;
    float MoveSpeed = 2;

    void Start()
    {
        
    }

    void Update()
    {
        // Movement controls //
        // W - Up
        // S - Down (Not in use unless we want to make player fall faster when "s" is pressed)
        // A - Left
        // D - Right

        //Get the Rigidbody of the player to apply forces on it
        Rigidbody2D Body = Player.GetComponent<Rigidbody2D>();

        //Get horizontal and vertical speed for fixing fall speed and preventing gliding while falling
        Vector3 Speed = Body.velocity;

        //GetKeyDown runs code once when pressed. GetKey runs the code repeatedly while key is pressed. (GetKey could be used for autojumping)
        if (Input.GetKeyDown("w")) 
        {
            //Work-around since you can't specifically set x and y of velocity directly... 
            Vector3 Temp = Speed;
            Temp.y = JumpPower;

            Body.velocity = Temp; //Body.velocity sets a constant velocity so the player doesn't speed up out of control
        }

        //Move Left
        if (Input.GetKey("a"))
        {
            Vector3 Temp = Speed;
            Temp.x = -MoveSpeed;
            Body.velocity = Temp;
            
            if (Input.GetKeyDown("w")) //Code for up is required in "a" and "d" so the y velocity isn't overwritten by the Body.velocity
            {
                Vector3 Temp2 = Speed;
                Temp2.y = JumpPower;
                Body.velocity = Temp2; 
            }

        }
        
        //Move Right
        if (Input.GetKey("d"))
        {
            Vector3 Temp = Speed;
            Temp.x = MoveSpeed;
            Body.velocity = Temp;
            
            if (Input.GetKeyDown("w"))
            {
                Vector3 Temp2 = Speed;
                Temp2.y = JumpPower;
                Body.velocity = Temp2;
            }
        }

    }

}
