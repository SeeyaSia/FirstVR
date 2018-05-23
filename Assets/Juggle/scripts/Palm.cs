using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm : MonoBehaviour {
    //public Hand hand;
    public TimeManager timeManager;
    private Vector3 tossVector;

    private void Start()
    {   
        //hand = GetComponent<Hand>();
        //this.gameObject.transform = this.GetComponentInParent()
    }

    void OnCollisionEnter(Collision col)
    {
        //When objects collide with this surface trigger slow motion to slow down time 
        //timeManager.slowMotion();

        //var joint = gameObject.AddComponent<FixedJoint>();
        //joint.connectedBody = col.rigidbody;
        //col.gameObject.transform.position = gameObject.GetComponent<Transform>().position ;
        //Debug.Log("Joint Connected");
    }
    void OnCollisionStay(Collision col)
    {
        // Debug.Log("test");
        var handVelocity = transform.parent.gameObject.GetComponent<Hand>().getVelocity();
        var handMagnitude = handVelocity.magnitude;

        //When object is on the palm, if the velocity of the palm gets bigger than x it applies a force to the object (Toss without letting go)
        if (handMagnitude > 1f && col.gameObject.GetComponent<HeldObject>().parent == null )
        {


            //Drop held object
            //Destroy(gameObject.GetComponent<FixedJoint>());
            col.gameObject.GetComponent<Rigidbody>().isKinematic = false;


            //Method 1: Adding force to toss
            tossVector = handVelocity;
            Debug.Log(tossVector.y);
            //Help error correct by pushing y towards 1.
            if(tossVector.y < 0.9f)
            {
                tossVector.y += tossVector.y * 0.1f;
            }
            else if (tossVector.y > 1.1f)
            {
                tossVector.y -= tossVector.y * 0.1f;
            }
            else
            {
                tossVector.y = 1f;
            }
            Debug.Log(tossVector.y);
            col.gameObject.GetComponent<Rigidbody>().AddForce(handVelocity*30);
            //col.gameObject.GetComponent<Rigidbody>().AddTorque(hand.angularVelocity() * 25);
            

            //Method 2: Set the velocity and angular velocity of the object to the controller at the time of release to toss
            //col.gameObject.GetComponent<Rigidbody>().velocity = hand.getVelocity() * 1.5f;
            //col.gameObject.GetComponent<Rigidbody>().angularVelocity = hand.angularVelocity() * 1.5f;
        }
            
    }
}
