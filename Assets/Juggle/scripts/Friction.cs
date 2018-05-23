using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Friction : MonoBehaviour {
    public TimeManager timeManager;


    void OnCollisionStay(Collision col)
    {
        //Slow down objects rolling on the surfae of this object
        col.gameObject.GetComponent<Rigidbody>().velocity *= 0.99f;
    }


    void OnCollisionEnter(Collision col)
    {
        //When objects collide with this surface trigger slow motion to slow down time 
        timeManager.slowMotion();

    }
    void OnCollisionExit(Collision col)
    {
        //When an object exits collision, return time back to normal
        timeManager.normal();
    }
}
