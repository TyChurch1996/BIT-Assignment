using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private int healAmount;
    [SerializeField]
    private int damageAmount;
    [SerializeField]
    private string Effect;
    [SerializeField]
    private Vector2? tempVector;
    [SerializeField]
    private int TrajectileHeight;
    [SerializeField]
    private float TrajectileFall;
    [SerializeField]
    private float AttackSpeed;
    [SerializeField]
    private Sprite BulletSprite;

    //item effects happen here
    void OnTriggerEnter2D(Collider2D col)
    {

        //set heal amount in editor if item heals
        if (col.gameObject.tag == "Player")
        {
            if (healAmount != 0)
            {
                col.gameObject.GetComponent<PlayerController>().PlayerHealed(healAmount);
            }

            //set damage amount in editor if item damages
            if (damageAmount != 0)
            {
                col.gameObject.GetComponent<PlayerController>().PlayerDamaged(damageAmount);
            }

            //apply programable effects here by checking if conditions met
            if (Effect != null)
            {


                 if (Effect == "Salt")
                {
                    col.gameObject.transform.GetChild(0).gameObject.GetComponent<PlayerWeapon>().updateProjectile(Effect, 2, Color.white, new Vector2(0, TrajectileHeight), TrajectileFall, AttackSpeed, BulletSprite);

                }


                //needs sprite changes for bullet
                else if (Effect == "GumBounce")
                {
                    col.gameObject.transform.GetChild(0).gameObject.GetComponent<PlayerWeapon>().updateProjectile(Effect,3, Color.red, new Vector2(0, TrajectileHeight), TrajectileFall,AttackSpeed, BulletSprite);

                }

                //needs sprite changes for bullet
                else if (Effect == "WatermelonSeed")
                {
                    col.gameObject.transform.GetChild(0).gameObject.GetComponent<PlayerWeapon>().updateProjectile(Effect, 1, Color.white, AttackSpeed, BulletSprite);

                }

                //needs sprite changes for bullet
                else if (Effect == "Nacho")
                {
                    col.gameObject.transform.GetChild(0).gameObject.GetComponent<PlayerWeapon>().updateProjectile(Effect, 1, Color.white, AttackSpeed,BulletSprite);

                }

            }





            this.gameObject.SetActive(false);
        }


    }

}
