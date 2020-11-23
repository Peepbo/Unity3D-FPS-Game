using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float Vcam, Hcam;

    //GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        //head = transform.Find("Head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //카메라에서 가져오기위함
        Hcam += Input.GetAxis("Mouse X");
        Vcam -= Input.GetAxis("Mouse Y");

        if (Vcam > 70) Vcam = 70;
        else if (Vcam < -30) Vcam = -30;

        //MouseX = Input.GetAxit("Mouse X");

        //transform.Rotate(Vector3.up * 1f * Hcam);
        //transform.Rotate(Vector3.left * 1f * Vcam);
        transform.rotation = Quaternion.Euler(1f * Vcam, 1f * Hcam, 0f);
        //transform.Rotate(new Vector3(1f * Hcam, 1f * Vcam, 0f));
        //transform.rotation.z = 0f;
    }
}
