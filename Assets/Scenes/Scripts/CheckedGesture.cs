using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckedGesture : MonoBehaviour
{
    public GameObject TVCanvas;
    private AudioSource checkSound;
    public GameObject hand;
    public Material checkMat;
    private Material origMat;
    private float t0;
    private float t1;   

    public void Start()
    {
        origMat = hand.GetComponent<SkinnedMeshRenderer>().material;
        checkSound = TVCanvas.GetComponent<AudioSource>();
        checkSound.Play();
        t0 = Time.time;
    }

    public void Update()
    {
        t1 = Time.time;

        if (t1-t0 < checkSound.clip.length)
        {
            hand.GetComponent<SkinnedMeshRenderer>().material = checkMat;
        }
        else
        {
            hand.GetComponent<SkinnedMeshRenderer>().material = origMat;

        }
        
        
    }
}
