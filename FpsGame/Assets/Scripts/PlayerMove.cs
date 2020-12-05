using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    CharacterController cc;

    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 이동
        Move();
    }

    private void Move()
    {
        
        //transform.Translate(dir * speed * Time.deltaTime);

        //문제점 : 충돌처리 안됨, 공중부향, 땅파고들기

        //캐릭터컨트롤러 컴포넌트를 사용한다.
        //캐릭터컨트롤러는 충돌감지만 하고 물리가 적용안된다.

        //따라서 충돌감지를 하기위해서는 반드시 캐릭터 컨트롤러
        //무브할수로 이동처리해야 한다.



        if(cc.isGrounded)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(h, 0, v).normalized;


            //카메라가 보는 방향으로 이동해야 한다.
            dir = Camera.main.transform.TransformDirection(dir);

            //회전된 방향으로 이동
            cc.Move(dir * speed * Time.deltaTime);

            if (Input.GetButton("Jump"))
            {
                moveDirection = Vector3.up * 8.0f;
            }
        }

        else
        {
            moveDirection.y -= 20.0f * Time.deltaTime;
            cc.Move(moveDirection * Time.deltaTime);
        }
    }
}
