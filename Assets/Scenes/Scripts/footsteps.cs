using UnityEngine;
using System.Collections;

public class footsteps : MonoBehaviour
{
       CharacterController cc;

       void Start () 
       {
              cc = GetComponent<CharacterController>();
       }
       
       void Update () 
       {
              if (cc.isGrounded == true && cc.velocity.magnitude > 0.5f && GetComponent<AudioSource>().isPlaying == false)
              {
                     GetComponent<AudioSource>().Play();
              }
       }
}