
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject playerObject;
    [SerializeField]
    private int speed;
    private int damage;
    private Rigidbody2D rbody;
    [SerializeField]
    private string Effect;
    private float TrajectileFall;
    private Vector2? Trajectory;
    private GameObject particles;
    private bool projDir; //true if projectile travels left otherwise false if travels right

    private void Awake()
    {


    }


    // Start is called before the first frame update
    void Start()
    {

        
        playerObject = GameObject.Find("Player");
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = transform.right * speed;



        
        //List Effects Under Here
        if (Effect != null)
        {
            if (Effect == "GumBounce")
            {
                rbody.AddForce(Trajectory.Value * speed);
            }

            if (Effect == "Nacho")
            {
               
             
                playerObject.GetComponent<System_Functions>().sleepTillDestroy(0.07f, gameObject);

            }



        }




    }

    // Update is called once per frame
    void Update()
    {
        float DistanceX = playerObject.transform.position.x - rbody.transform.position.x;

        float DistanceY = playerObject.transform.position.y - rbody.transform.position.y;


        if (TrajectileFall != 0)
        {
            
            if (DistanceX + DistanceY < 5 || DistanceX + DistanceY < -5)
            {
         
                rbody.AddForce(-transform.up * speed / TrajectileFall);
            }
        }



    }

    public void setDamage(int pdamage)
    {


        damage = pdamage;

    }

    public void setTrajectory(Vector2? bTrajectory)
    {

        Trajectory = bTrajectory;
    }

    public void setTrajectileFall(float bTrajectilefall)
    {

        TrajectileFall = bTrajectilefall;
    }

    public void setEffect(string bEffect)
    {

        Effect = bEffect;
    }

    public void setProjectileColour(Color ProjectileColour1)
    {

        gameObject.GetComponent<SpriteRenderer>().color = ProjectileColour1;
    }


    public void setProjectileSprite(Sprite bSprite)
    {


        if (bSprite != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bSprite;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }



    void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag == "PowerUp") {
            return;
        }

        else if (col.gameObject.tag == "Destroyable")
        {
            Destroy(col.gameObject);

        }

        else if (col.gameObject.tag== "Enemy")
        {
            col.gameObject.GetComponent<EnemyController>().EnemyDamaged(damage);

            int enemyHealth = col.gameObject.GetComponent<EnemyController>().getEnemyHealth();

            if (enemyHealth < 1)
            {
                Destroy(col.gameObject);
            }
 

        }
        if (Effect == "GumBounce")
        {
            Instantiate(Resources.Load("Prefabs/Gum Explosion"), rbody.transform.position, rbody.transform.rotation);
        }




        Destroy(gameObject);
    }

}
