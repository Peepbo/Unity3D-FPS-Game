using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    GameObject target;
    Transform angle;

    MouseController Mc;
    float camX, camY;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        Mc = target.GetComponent<MouseController>();
        angle = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0, Mc.Vcam, 0f);

        //transform.rotation = angle.rotation;
    }
}
