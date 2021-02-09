
using UnityEngine;

public class Save_System : MonoBehaviour



{


    private string lastCheckPoint;
    private string lastSceneName;


    //store last check point touched and level when called
    void Save() {

  //      lastCheckPoint=gameObject.find("Player").getComponent<playerController>().lastcheckpoint;
  //      lastSceneName = gameObject.find("Player").getComponent<playerController>().lastSceneName;


    }


    //load last check point touched and level when called
    void Load() {

    //    SceneManager.LoadScene(lastSceneName);
    //    gameObject.find("Player").transform.position = gameObject.find(lastCheckPoint).transform.position;

    }

/*
 * to be placed into not destroyed on load empty object in main menu
 * 
 */


}
