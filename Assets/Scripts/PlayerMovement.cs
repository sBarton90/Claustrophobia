using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform Player;

    float JumpPower = 4;
    float MoveSpeed = 2.5f;

    float Grounded; //Will be 1 or 1.75, Grounded / not Grounded (Nerfs air movement)
    Vector3 Speed;

    void Jump()
    {
        RaycastHit2D Down = Physics2D.Raycast(Player.position - new Vector3(0, 1.2f), -Player.up, 0.1f);
        if (Down)
        {
            Debug.Log("Hit " + Down.collider.gameObject.name);
            Rigidbody2D Body = Player.GetComponent<Rigidbody2D>();
            //Work-around since you can't specifically set x and y of velocity directly... 
            Vector3 Temp = Speed;
            Temp.y = JumpPower;
            Body.velocity = Temp; //Body.velocity sets a constant velocity so the player doesn't speed up out of control
            new WaitForSeconds(0.1f); // Wait for the player to be in the air before saying the player isn't grounded
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

        //GetKeyDown runs code once when pressed. GetKey runs the code repeatedly while key is pressed. (GetKey could be used for autojumping)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //Move Left
        if (Input.GetKey("a"))
        {
            RaycastHit2D Left = Physics2D.Raycast(Player.transform.position - new Vector3(0.55f, 0.66f), -Player.right, 0.1f);
            //Debug.DrawRay(Player.transform.position - new Vector3(0.55f, 0.66f), -Player.right * 0.1f, Color.red);

            Vector3 Temp = Speed;
            Temp.x = -MoveSpeed;
            Body.velocity = Temp;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Left)
            {
                Vector3 Temp2 = Speed;
                Temp.x = 0;
                Body.velocity = Temp;
            }

        }

        //Move Right
        if (Input.GetKey("d"))
        {
            RaycastHit2D Right = Physics2D.Raycast(Player.transform.position + new Vector3(0.5f, -0.66f), Player.right, 0.1f);
            //Debug.DrawRay(Player.transform.position + new Vector3(0.5f, -0.66f), Player.right * 10, Color.red);

            Vector3 Temp = Speed;
            Temp.x = MoveSpeed;
            Body.velocity = Temp;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Right) // Cancel out velocity
            {
                Vector3 Temp2 = Speed;
                Temp.x = 0;
                Body.velocity = Temp;
            }

        }

    }

}


