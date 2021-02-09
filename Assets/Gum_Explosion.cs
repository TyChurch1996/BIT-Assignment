using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Gum_Explosion : MonoBehaviour
{
    Boolean timeCheck;

    // Start is called before the first frame update
    void Start()
    {
      GameObject.Find("Player").GetComponent<System_Functions>().sleepTillDestroy(0.5f,this.gameObject);

     


    }



}
