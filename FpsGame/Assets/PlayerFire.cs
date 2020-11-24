using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject effect;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 30f, Color.green);

        if(Input.GetButtonDown("Fire1"))
        {
            //print("발사");

            RaycastHit hit;

            if(Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward,out hit,100f))
            {
                print(hit.collider.name);

                Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
