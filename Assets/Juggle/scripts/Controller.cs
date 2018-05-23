using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    //Create a variable called controller of type seam device and a getter function for it
    public SteamVR_Controller.Device controller
    {
        get
        {
            //Return the index of this tracked Real life object/Controller
            //GetComponent can be used to access other scripts or game object components. It is an expensinve task, perfom in setup or as little as possible in update.
            return SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
        }
    }
}
