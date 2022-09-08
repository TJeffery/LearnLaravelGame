using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorControl : MonoBehaviour
{

    public bool active;
    public bool animateTrigger;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
