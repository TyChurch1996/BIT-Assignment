using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalPretzelController : MonoBehaviour
{
    enum Direction
    {
        right,
        left
    }
    [SerializeField]
    private float speed;
    [SerializeField]
    private int RPHealth;
    [SerializeField]
    private int eDamage;
    [SerializeField]
    private Direction direction = Direction.right;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject dashTargetNear;
    [SerializeField]
    private GameObject dashTargetFar;
    [SerializeField]
    private GameObject RPTop;
    [SerializeField]
    private GameObject RPBottom;
    [SerializeField]
    private GameObject RPRight;
    [SerializeField]
    private GameObject RPLeft;
    //Royal Pretzel to face player and try to attack when not currently performing a maneuver
    [SerializeField]
    private bool isActive;
    [SerializeField]
    private bool maneuvering;

    void Update( )
    {
        //test for direction of player, face them or panic if underneath
        if (player.transform.position.x < RPLeft.transform.position.x) //may change to RP.pos +- [offset width]
        {
            Face(1);
        }
        else if (player.transform.position.x > RPRight.transform.position.x)
        {
            Face(0);
        }
        else if (RPLeft.transform.position.x < player.transform.position.x && player.transform.position.x < RPRight.transform.position.x)
        {
            Panic( );
        }

        //test for player within dash range to select dash attack angle
        if (dashTargetFar.transform.position.x < player.transform.position.x && player.transform.position.x < dashTargetNear.transform.position.x)
        { //last line works for direction = left
            if (player.transform.position.y < RPBottom.transform.position.y)
            {
                DashAttack(-1);
            }
            else if (player.transform.position.y > RPTop.transform.position.y)
            {
                DashAttack(0);
            }
            else
            {
                DashAttack(1);
            }
        }
        else if (player.transform.position.x > dashTargetNear.transform.position.x)
        {
            MeleeAttack( );
        }
        else if (player.transform.position.x < dashTargetFar.transform.position.x)
        {
            RangedAttack( );
        }
    }

    void Face(int direction)
    {
        Debug.Log("Face direction " + direction);
        // turn Royal Pretzel towards the player (Direction)
    }

    void Panic()
    {
        if (!maneuvering){
            Debug.Log("panic");
            // rapidly (not too fast) switch between facing left and right, occasional jump
            // after 4(?) seconds dodges to a random side
        }

    }

    void DashAttack(int angle)
    {
        if(!maneuvering) // using a sword
        {
            Debug.Log("dash attack " + angle);
            // set RP as immune, animation to dash, move towards player either up at 30 deg, level, or down at 30 deg, 
            // set speed to 1.4x normal, wait for dash duration, animation back to normal, set RP as !immune, wait for
            // dash delay, end maneuver
        }
    }

    void MeleeAttack( ) // using a sword
    {
        if (!maneuvering)
        {
            Debug.Log("melee attack");
            // animation to melee, "step" towards player, set weapon to swing in arc [from vertical to horizontal], 
            // wait for melee duration, animation back to normal, wait for melee delay, end maneuver
            // if health is low, melee delay goes down for faster attacks
        }
    }

    void RangedAttack() // using salt grains
    {
        if (!maneuvering)
        {
            Debug.Log("range attack");
            // animation to ranged, "step" away from player, set salt ammo in arc towards player, 
            // wait for Ranged duration, animation back to normal, wait for ranged delay, end maneuver
            // if health is low, ranged delay goes down for faster attacks
        }
    }
}
    
