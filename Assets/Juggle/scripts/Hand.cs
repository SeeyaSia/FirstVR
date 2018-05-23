using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is the Hand object, represents one hand
//A hand requires a controller, require a controller type object
[RequireComponent(typeof(Controller))]

public class Hand : MonoBehaviour {

    GameObject heldObject;
    Controller controller;
    public Palm palm;

    public AudioSource letgo;

    //Used to store heldObject toss
    //private AudioSource[] aSources;

    //Button Stats
    private Valve.VR.EVRButtonId gripbutton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;

    private Valve.VR.EVRButtonId triggerbutton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    // Use this for initialization
    void Start () {
        //get the controller from Controller script (using it's getter function)
        controller = GetComponent<Controller>();
         
        letgo = GetComponent<AudioSource>();

    }
    public Vector3 getVelocity()
    {
        return controller.controller.velocity;
    }
    public Vector3 angularVelocity()
    {
        return controller.controller.angularVelocity;
    }
    public void TossHeld()
    {

        Destroy(heldObject.GetComponent<FixedJoint>());
        //Drop held object
        heldObject.transform.parent = null;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<HeldObject>().parent = null;

        //Set the velocity and angular velocity of the object to the controller at the time of release to toss
        heldObject.GetComponent<Rigidbody>().velocity = controller.controller.velocity;
        heldObject.GetComponent<Rigidbody>().angularVelocity = controller.controller.angularVelocity;

        var aSources = (AudioSource[])heldObject.GetComponents<AudioSource>();
        aSources[1].Play();
        //heldObject.GetComponent<Rigidbody>().AddForce(controller.controller.velocity * 50, ForceMode.Impulse);

        //Play hand let go sound
        letgo.Play();

        heldObject = null;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (controller == null)
        {
            Debug.Log("Controller null");
            return;
        }
        //Down happens once when you press
        gripButtonDown = controller.controller.GetPressDown(gripbutton);
        //Happens once when you release
        gripButtonUp = controller.controller.GetPressUp(gripbutton);
        //Stays true until released
        gripButtonPressed = controller.controller.GetPress(gripbutton);

        //Trigger Button Stats
        triggerButtonDown = controller.controller.GetPressDown(triggerbutton);
        triggerButtonUp = controller.controller.GetPressUp(triggerbutton);
        triggerButtonPressed = controller.controller.GetPress(triggerbutton);
        Debug.Log(palm.gameObject.activeSelf);
        
        if (gripButtonDown)
        {
            if(palm.gameObject.activeSelf == true)
            {
               // Debug.Log("Deactivating");
                palm.gameObject.SetActive(false);
            }
            else
            {
                //Debug.Log("Activating");
                palm.gameObject.SetActive(true);
            }
            
        }


        //Check to see if hand is empty, or held object is set to something
        if (heldObject)
        {
            //Debug.Log(controller.controller.velocity);
            //If we have something held, check to see if finger is removed from trigger
            if (triggerButtonUp)
            {
                TossHeld();
            }
     
        }
        else
        {
            //Check to see if trigger is pressed
            if (triggerButtonDown)
            {
                //Create an array of colliders with 0.1 radius around this hand.
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);


                foreach (Collider col in cols)
                {
                    //No current object is being held && The col has the HeldObject script attached && No other controller is currently holding the col object.
                    if (heldObject == null && col.GetComponent<HeldObject>() && col.GetComponent<HeldObject>().parent == null)
                    {
                        //Pick up Object in hand

                        //Set the held object to the this col
                        heldObject = col.gameObject;
                        //Set the transform of the object to follow the hand
                        heldObject.transform.parent = this.transform;
                        heldObject.transform.localPosition = Vector3.zero;
                        //heldObject.transform.localRotation = Quaternion.identity;
                        //Make object Kinematic so no force effects it, including gravity
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        //Set the parent variable on the held object so it knows it's being held and identifies it's parent
                        heldObject.GetComponent<HeldObject>().parent = controller;


                    }
                }
            }
        }
	}
}
