using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is to manage the door controls
/// </summary>
public class doorControl : MonoBehaviour
{

    public bool active;
    public bool animateTrigger;
    private Animator anim;
    

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            anim.SetBool("open", true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            anim.SetBool("open", false);
        }
    }
}
