using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public GameObject player;
    private Quaternion cameraRotation;
    private Vector3 cameraOffSet;
    // Start is called before the first frame update
    private void Awake()
    {

        transform.parent = null;
    }
    void Start()
    {
        //cameraRotation = this.transform.rotation;
        cameraOffSet = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.localRotation = Quaternion.Euler(60, 0, 0);
        //gameObject.transform.LookAt(player.transform);
        //transform.rotation = cameraRotation;
        Vector3 newpos = player.transform.position - cameraOffSet;
        transform.position = newpos;
    }
}
