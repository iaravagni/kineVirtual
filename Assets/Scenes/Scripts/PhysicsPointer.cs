using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Serialization;
using Unity.XR.CoreUtils;

public class PhysicsPointer : MonoBehaviour
{
    public float defaultLength = 3.0f;
    private LineRenderer lineRenderer = null;
    
    public GameObject chair;
    private GameObject foundObject;

    public GameObject player;
    public GameObject controller;
    public GameObject handR;
    public GameObject handL;

    public GameObject TV;
    public GameObject gestures;
    public GameObject alexa;
    

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLength();

        StartTreatment();
    }

    private void UpdateLength()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, CalculateEnd());
    }
    
    private Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwardRaycast();
        Vector3 endPosition = DefaultEnd(defaultLength);

        if (hit.collider)
            endPosition = hit.point;
        return endPosition;
    }

    private RaycastHit CreateForwardRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out hit, defaultLength);
        return hit;

    }

    private Vector3 DefaultEnd(float length)
    {
        return transform.position + (transform.forward * length);
    }

    private void StartTreatment()
    {
        RaycastHit hit = CreateForwardRaycast();

        foundObject = hit.collider.gameObject;

        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && (foundObject==chair))
        {
            player.GetComponent<LocomotionSystem>().enabled = false;
            player.GetComponent<ContinuousMoveProviderBase>().enabled = false;
            player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<CapsuleCollider>().enabled = false;

            player.transform.position = new Vector3 (1.278f,0.0f,1.992f);
            
            alexa.GetComponent<AudioSource>().Stop();
            TV.GetComponent<AudioSource>().Play();

            gestures.SetActive(true);

            handL.GetComponent<SkinnedMeshRenderer>().enabled=true;
            handR.GetComponent<SkinnedMeshRenderer>().enabled=true;
            controller.SetActive(false);

            
        }
    }
}   

