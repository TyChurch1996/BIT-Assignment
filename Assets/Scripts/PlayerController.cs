using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    enum Direction
    {
        right,
        left
    }

    //Serialize Field will show the variables in the Unity Inspector and is generally used whilst developing to allow for quick alterations to variable values
    // before hard-coding it once it is discovered what values feel good.
    public float speed;
    [SerializeField]
    private float jumpHeight;
    private Rigidbody2D playerRigidBody;
    private Animator anim;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private float jumpDuration;
    [SerializeField]
    private bool isSlippery;
    private Direction playerFacing;
    [SerializeField]
    private Image[] healthpeanut;
    [SerializeField]
    private int playerHealth;
    public bool damageImmune;
    public float damageDuration;
    private float durationTimer;
    private SpriteRenderer playerSprite;
    private Color defaultColor;

    public GameObject CheckPoint;


    // Component referencing can be done in Awake, which is called upon boot-up of the program.
    void Awake()
    {
        playerHealth = 3;
        playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGrounded = true;
        isSlippery = false;
        damageImmune = false;
        playerSprite = this.GetComponent<SpriteRenderer>();
        defaultColor = playerSprite.color;


        healthpeanut = new Image[3];
        healthpeanut[0] = gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        healthpeanut[1] = gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<Image>(); 
        healthpeanut[2] = gameObject.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Image>(); 
    }

    // Start is called during the first frame that *this* script is active.
    // It's good to initialize variables in Start.
    private void Start()
    {
        UpdateHealth();
    }

    // Fixed Update is used for anything physics related. Call all methods that manipulate or use physics here.
    // Fixed Update, like "Regular" Update, is called every frame.
    //input checking should not be performed under fixed update, only the actual physics here
    private void FixedUpdate()
    {
        Movement();

        if (Input.GetButton("Jump") && isGrounded == true && isJumping == false)
        {

            Jump();

        }

        if (isJumping == true)
        {

            jumpDuration -= Time.deltaTime;

            if (jumpDuration <= 0)
            {
                isJumping = false;
            }
        }

    }

    //public function so we can damage player from anywhere
    public void PlayerDamaged(int Damage)
    {
        //Prevents the player from taking damage while
        //Immune and therfore flashing
        if(damageImmune == false)
        {
            playerHealth -= Damage;
            damageImmune = true;
            durationTimer = damageDuration;         
        }
    }

    //public function so we can damage player from anywhere
    public void PlayerHealed(int Damage)
    {
        playerHealth += Damage;

    }

    //updates players health gui
    void UpdateHealth()
    {
        if (playerHealth > 3)
        {
            playerHealth = 3;
        }
        else if (playerHealth < 0)
        {
            playerHealth = 0;
        }



        switch (playerHealth)
        {
            case 1: // player has 1 health
                healthpeanut[0].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Filled)");
                healthpeanut[1].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Empty)");
                healthpeanut[2].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Empty)");
                break;
            case 2: //  player has 2 health

                healthpeanut[0].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Filled)");
                healthpeanut[1].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Filled)");
                healthpeanut[2].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Empty)");
                break;
            case 3: //  player has 3 health
                    // healthpeanut[2];
                healthpeanut[0].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Filled)");
                healthpeanut[1].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Filled)");
                healthpeanut[2].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Filled)");
                break;
            default:
                //player has died

                healthpeanut[0].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Empty)");
                healthpeanut[1].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Empty)");
                healthpeanut[2].sprite = Resources.Load<Sprite>("Art/Peanut HUD (Empty)");
                break;

        }

    }



    // "Regular" Update is used for anything *not* physics related. Call all methods that have nothing to do with physics here.
    // Like Fixed Update, "Regular Update" is called every frame.
    private void Update()
    {
        Animate();
        Flip();
        DamageFlash();

        if(playerHealth <= 0)
        {
            playerDied();
        }
    }

    private void DamageFlash()
    {
        //If player has taken damage, change him red and change his 
        //Alpha value to emulate flashing
        if (damageImmune)
        {
            
            if (durationTimer > damageDuration * 0.66f)
            {
                playerSprite.color = new Color(1, 0.09f, 0.09f, 0f);
            }
            else if (durationTimer > damageDuration * 0.33f)
            {
                playerSprite.color = new Color(1, 0.09f, 0.09f, 1f);
            }
            else if (durationTimer > 0)
            {
                playerSprite.color = new Color(1, 0.09f, 0.09f, 0f);
            }
            else
            {
                //Change him back to default and remove immunity
                playerSprite.color = defaultColor;
                damageImmune = false;
            }

            durationTimer -= Time.deltaTime;

        }
    }

    // Sets a temporary float to whatever the Horizontal input of the player is, multiplies it by the speed value, and then assigns
    // the resulting value to our RigidBody's X velocity. We also ensure our Vertical isn't effected by simply stating that the value is
    // what ever the current value is on the Y axis.
    //addforce if on a slippery tilemap
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;
        playerRigidBody.velocity = new Vector2(horizontal * Time.deltaTime, playerRigidBody.velocity.y);

        if (isSlippery == true)
        {
            if (Input.GetAxis("Horizontal") > 0.1)
            {
                playerRigidBody.AddForce(transform.right * 4, ForceMode2D.Force);

            }
            if (Input.GetAxis("Horizontal") < -0.1)
            {
                playerRigidBody.AddForce(-transform.right * 4, ForceMode2D.Force);

            }
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        UpdateHealth();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateHealth();
        if (collision.gameObject.CompareTag("Ground") || (collision.gameObject.CompareTag("slippery")))
        {
            isGrounded = true;
            if (collision.gameObject.CompareTag("slippery"))
            {
                isSlippery = true;
            }
        }

        if(collision.gameObject.CompareTag("Checkpoint"))
        {
            CheckPoint = collision.gameObject;
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<Collider2D>());
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        UpdateHealth();
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("slippery"))
        {
            isSlippery = false;
        }
    }

    // Similar to Movement, it handles the Jumping via applying a value to the Y axis of the Rigidbody.
    void Jump()
    {
        if (isGrounded == true)
        {
            playerRigidBody.AddForce(transform.up * jumpHeight / 100, ForceMode2D.Impulse);

            isJumping = true;
            jumpDuration = 0.6f;
        }
    }

    // Checks if the player moves the character left or right and flips the graphics to the appropriate orientation.
    void Flip()
    {
        //Checks to see if the player is already facing the right way
        //If not, rotate him. The rotate is need for the Bullet Spawn Point. If we just create a new vector
        //The spawn point will not rotate and you can only shoot right
        if (Input.GetAxis("Horizontal") > 0.1 && playerFacing != Direction.right)
        {
            playerFacing = Direction.right;
            transform.Rotate(0, 180, 0);

        }
        else if (Input.GetAxis("Horizontal") < -0.1 && playerFacing != Direction.left)
        {
            playerFacing = Direction.left;
            transform.Rotate(0, 180, 0);

        }

    }

    private void playerDied()
    {
        this.transform.position = CheckPoint.transform.position;
        PlayerHealed(3);
    }

    void Animate()
    {
        if (playerRigidBody.velocity.x != 0 && isGrounded)
        {
            anim.Play("Player_Run");
        }
        else if (playerRigidBody.velocity.x == 0 && isGrounded)
        {
            anim.Play("Player_Idle");
        }

        else if(!isGrounded)
        {
            anim.Play("Player_Jump");
        }
    }
}
