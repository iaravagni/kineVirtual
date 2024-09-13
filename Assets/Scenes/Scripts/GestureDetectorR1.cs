using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityEngine.Video;


public class GestureDetectorR1 : MonoBehaviour
{

    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    private List<OVRBone> fingerBones;
    private Gesture previousGesture;
    public VideoClip video0;
    public VideoClip video1;
    public VideoClip video2;
    public VideoClip video3;
    public VideoClip video4;
    public VideoClip video5;
    public GameObject TVCanvas;
    public GameObject TV;
    private VideoPlayer videosource;
    private AudioSource checkSound;
    public GameObject hand;
    public Material checkMat;
    private Material origMat;
    private bool v0 = true;
    private bool v1 = true;
    private bool v2 = true;


    IEnumerator Start(){

        videosource = TVCanvas.GetComponent<VideoPlayer>();
        checkSound = TVCanvas.GetComponent<AudioSource>();
        
        

        while (skeleton.Bones.Count == 0) 
        {
            yield return null;
        }

        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
        origMat = hand.GetComponent<SkinnedMeshRenderer>().material;
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }

        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());

        //Debug.Log(videosource.isPlaying + "1");
        //Debug.Log(v0);
        //Debug.Log(v1);
        if (v0)
        {
            TV.GetComponent<AudioSource>().Stop();
            videosource.clip = video0;
            videosource.isLooping = false;
            videosource.Play();
            //Debug.Log(videosource.isPlaying + "2");
            v0 = false;
            
        }
        else if (!videosource.isPlaying && v1)
        {
            //Debug.Log("Segundo video");
            videosource.clip = video1;
            videosource.isLooping = true;
            videosource.Play();
            
            v1 = false;
        }
        /*
        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            if (currentGesture.name == "thumb down" && v1 && !v2)
            {
                checkSound.Play();
                videosource.clip = video2;
                videosource.Play();
                videosource.isLooping = true;
                while (checkSound.isPlaying)
                {
                    hand.GetComponent<SkinnedMeshRenderer>().material = checkMat;
                }
                hand.GetComponent<SkinnedMeshRenderer>().material = origMat;
                v2 = false;
            }

        }
        */

    }

    void Save(){
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach (var bone in fingerBones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }
        g.fingerDatas = data;
        gestures.Add(g);
    }

    Gesture Recognize()
    {
        Gesture currentgesture = new Gesture();
        float currentMin = Mathf.Infinity;
        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);
                if (distance>threshold)
                {
                    isDiscarded = true;
                    break;
                }
                sumDistance += distance;
            }

            if(!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentgesture = gesture;

            }

        }
        return currentgesture;
    }
}
