using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class System_Functions : MonoBehaviour
{

   private GameObject bulletSpawnPoint;


    private void awake() {

        


    }



    public void sleep(float time)
    {
       
        StartCoroutine(Sleeping(time));
   

    }

  


    public void sleepTillDestroy(float time, GameObject gumExplosion)
    {
        StartCoroutine(SleepAndDestroy(time, gumExplosion));

    }






    private IEnumerator Sleeping(float time)
    {

        //time to sleep for
        yield return new WaitForSeconds(time);
        
     

    }

    private IEnumerator SleepAndDestroy(float time, GameObject gumExplosion)
    {

        //time to sleep for
        yield return new WaitForSeconds(time);
        
        Destroy(gumExplosion);
    }



}
