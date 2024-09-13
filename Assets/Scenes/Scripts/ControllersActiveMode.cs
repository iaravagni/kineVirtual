using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersActiveMode : MonoBehaviour
{   
    public GameObject ray;

    void Update()
    {
         if (OVRInput.GetActiveController() == OVRInput.Controller.None)
         {
            ray.SetActive(false);
            //ray.GetComponent<PhysicsPointer>().enabled = false;
            //ray.GetComponent<LineRenderer>().enabled = false;
         }else
         {
            ray.SetActive(true);
            //ray.GetComponent<PhysicsPointer>().enabled = true;
            //ray.GetComponent<LineRenderer>().enabled = true;
         }
    }
}

