using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    //Strength of the slow down
    public float slowdownFactor = 0.05f;
    //For how long should the slow down occure
    public float slowdownLength = 2f;

    private bool TimeEffect = false;

    private void Update()
    {
        //Time.deltaTime gets the time it took between updates
        //1 second divided by length, * the time it took between frames (ideally 0.02 for 50fps)
        if (Time.timeScale <= 1 && this.TimeEffect == true)
        {
            //unscaledDeltaTime is calculated without timeScale, deltaTime is calculated with timeScale
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        }
        else if(this.TimeEffect == true){
            this.TimeEffect = false;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
       
        
        //Debug.Log("timeScale: " + Time.timeScale);
        //Debug.Log("deltaTime: " + Time.deltaTime);
        //Debug.Log("fixedDeltaTime: " + Time.fixedDeltaTime);
    }

    public void slowMotion()
    {
        this.TimeEffect = true;
        //timeScale takes value 0-1 where 1 is normal speed
        Time.timeScale = slowdownFactor;
        //This updates the number of times update function is ran (FPS) so it will look like lag if we use timeScale to slow down update too much
        //we need to update FixedUpdate to run about 50fps. 1/50 = .02
        //Instead of changing FixedDeltaTime select Interpelate in Rigid body. (?)
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void normal()
    {
       // Time.timeScale = 1;
    }
}
