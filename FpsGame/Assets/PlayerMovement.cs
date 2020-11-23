using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    CharacterController cc;

    Transform head;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion dir = transform.rotation;
        if (Input.GetKey(KeyCode.W))
        {
            cc.Move(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
