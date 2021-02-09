using UnityEngine;

public class DashAbility : MonoBehaviour
{
    //If that player has the ability equipped or not
    public bool equipped;
    public float dashDuration;
    private float dashCooldown; 
    //dashCooldownLength is a forced time between dashing
    public float dashCooldownLength;
    //Keep multiplier small, about 1.1 / 1.2
    public float dashSpeedMultiplier;
    //Used to time the dash duration
    private float dashTimer; 
    private bool dashActive; 
    //Keep a reference to the daulft speed
    private float defaultSpeed;
    Rigidbody2D player;

    // Start is called before the first frame update
    void Start()
    {
        dashActive = false;
        dashTimer = dashDuration;
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (equipped)
        {
            if (!dashActive && dashCooldown <= 0)
            {
                //If player is not currently dashing and dash is off cooldown
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    defaultSpeed = player.GetComponent<PlayerController>().speed;
                    dashActive = true;
                    dashCooldown = dashCooldownLength;
                    dashTimer = dashDuration;
                }

            }
            else
            {
                //Player is dashing 
                //While active multiply the players velocity
                if (dashTimer > 0 && dashCooldown >= 0)
                {
                    dashTimer -= Time.deltaTime;
                    player.GetComponent<PlayerController>().speed *= dashSpeedMultiplier;
                }
                else if (dashTimer <= 0)
                {
                    //reset
                    dashActive = false;
                    player.GetComponent<PlayerController>().speed = defaultSpeed;
                    if (dashCooldown >= 0)
                    {
                        //Wait for cooldown before enabling dash
                        dashCooldown -= Time.deltaTime;
                    }

                }
                
            }

        }
       

    }

}
