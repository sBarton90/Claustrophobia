using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform Player;

    float JumpPower = 4;
    float MoveSpeed = 2;

    float Grounded; //Will be 1 or 1.75, Grounded / not Grounded (Nerfs air movement)
    Vector3 Speed;
    
    void Start()
    {

    }

    void Jump()
    {
        if (Grounded == 1)
        {
            Rigidbody2D Body = Player.GetComponent<Rigidbody2D>();
            //Work-around since you can't specifically set x and y of velocity directly... 
            Vector3 Temp = Speed;
            Temp.y = JumpPower;
            Body.velocity = Temp; //Body.velocity sets a constant velocity so the player doesn't speed up out of control
        }
    }

   

    void Update()
    {
     
        // Movement controls //
        // W - Up (Replaced W with Spacebar)
        // S - Down (Not in use unless we want to make player fall faster when "s" is pressed)
        // A - Left
        // D - Right

        //Get the Rigidbody of the player to apply forces on it
        Rigidbody2D Body = Player.GetComponent<Rigidbody2D>();

        //Get horizontal and vertical speed for fixing fall speed and preventing gliding while falling
        Vector3 Speed = Body.velocity;

        //Raycast to detect if the player is on the ground
        RaycastHit2D Down = Physics2D.Raycast(Player.transform.position - new Vector3(0, 0.7f), Player.up * -1, 0.05f);
        if (Down) { 
            Grounded = 1;
        } else {
            Grounded = 1.75f;
        }

        //GetKeyDown runs code once when pressed. GetKey runs the code repeatedly while key is pressed. (GetKey could be used for autojumping)
        if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            //Move Left
            if (Input.GetKey("a"))
            {
                //Disable moving left if the player can't go further left. This stops the player from pushing against the wall and floating on it.
                RaycastHit2D Left = Physics2D.Raycast(Player.transform.position - new Vector3(0.5f, 0.66f), Player.right * -1, 0.00001f);

                if (!Left)
                {
                    Vector3 Temp = Speed;
                    Temp.x = -MoveSpeed / Grounded;
                    Body.velocity = Temp;

                    if (Input.GetKeyDown(KeyCode.Space)) 
                    {
                        Jump();
                    }
                }
            }

            //Move Right
            if (Input.GetKey("d"))
            {
                RaycastHit2D Right = Physics2D.Raycast(Player.transform.position + new Vector3(0.5f, -0.66f), Player.right, 0.00001f);
                if (!Right)
                {
                    Vector3 Temp = Speed;
                    Temp.x = MoveSpeed / Grounded;
                    Body.velocity = Temp;

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Jump();
                    }
                }
            }
        }

    }


