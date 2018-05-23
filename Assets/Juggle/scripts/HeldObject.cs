using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script should get applied to any object that is holdable

public class HeldObject : MonoBehaviour {
    //AudioSource type variables to hold AudioSource components which can be used to play sound
    public AudioSource hit;
    public AudioSource toss;


    //Track which controller is currently holding this object
    public Controller parent;

    //An array to hold all the audioSources of this object
    //private AudioSource[] aSources;

    private void Start()
    {
        //Get all AudioSources from this component
        var aSources = (AudioSource[]) this.GetComponents<AudioSource>();
        //Assign them to the variables
        hit = aSources[0];
        toss = aSources[1];


    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Hit! Play sound");
        //Play hit. Toss is not used right now, but keeping this example for multiple sounds sample
        hit.Play();
    }
    

    
}
