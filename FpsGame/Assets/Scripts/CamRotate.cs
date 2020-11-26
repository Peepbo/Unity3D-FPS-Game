using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    //카메라를 마우스 움직이는 방향으로 회전하기
    public float speed = 150; //회전속도 (Time.deltaTime을 통해서 1초에 150도 회전)

    float angleX, angleY; // 직접 제어
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(-v, h, 0);
        //transform.Rotate(dir * speed * Time.deltaTime);

        //유니티엔진에서 제공해주는 함수를 사용함에 있어서
        //Translate함수는 그래도 사용하는데 큰 불편함이 없지만
        //회전을 담당하는 Rotote함수를 사용하면
        //우리가 회전처리를 제어하기 힘들다

        //transform.eulerAngles += dir * speed * Time.deltaTime;

        //카메라 상하회전각도를 제한해줘야 한다.
        //안그러면 백덤블링을 한다.

        Vector3 angle = transform.eulerAngles;
        angle += dir * speed * Time.deltaTime;

        //angle.x = Mathf.Clamp(angle.x, -60, 60);
        //transform.eulerAngles = angle;

        //여기에는 또 문제가 있다
        //유니티엔진 내부적으로 -각도는 360도를 더해서 처리된다.
        //내가 만든 각도를 가지고 계샃너리를 하자

        angleX += h * speed * Time.deltaTime;
        angleY += v * speed * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, -60, 60);

        transform.eulerAngles = new Vector3(-angleY, angleX, 0);
    }
}
