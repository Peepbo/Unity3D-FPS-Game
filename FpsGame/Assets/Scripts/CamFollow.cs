using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //카메라가 플레이어 따라다니기
    //플레이어한테 자식으로 붙여서 이동해도 상관없다.

    //하지만 게임에 따라서 드라마틱한 연출이 필요할때라던지
    //게임 기획에 따라 1인칭 또는 3인칭 등 변경이 필요할 수 있다.

    //지금은 우리 눈 역할을 할거라서 그냥 순간이동 시킨다.

    public Transform target3st; // 카메라가 따라다닐 대상
    public float speed = 10.0f; // 카메라 이동속도
    public Transform target1st;
    public bool isFPS = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) isFPS = true;
        if (Input.GetKeyDown(KeyCode.Alpha3)) isFPS = false;

        //if (isFPS) transform.position = target1st.position;
        //else transform.position = target3st.position;

        //FollowTarget();
    }

    private void LateUpdate()
    {
        if (isFPS) transform.position = target1st.position;
        else transform.position = target3st.position;
    }

    private void FollowTarget()
    {
        //방향 구하기
        Vector3 dir = target3st.position - transform.position;
        dir.Normalize();

        transform.Translate(dir * speed * Time.deltaTime);

        //문제점 : 타겟에 도착하면 덜덜덜 거림
        //거리를 구해서 고정시키면 된다

        //최적화 관련 면접 질문으로 종종 나온다

        //float x = x2 - x1;
        //float y = y2 - y1;
        //Math.Sqrt(x * x +  y * y);

        //1.벡터안의 Distance() 함수 사용 

        //2.벡터안의 magnitude 속성 사용

        //3.벡터안의 sqrMagnitude 속성 사용

        //1. Distance()
        //if (Vector3.Distance(target.position, transform.position) < 1)
        //{
        //    transform.position = target.position;
        //}

        //2. magnitude
        //float distance0 = (target.position - transform.position).magnitude;
        //if (distance0 < 1.0f) transform.position = target.position;

        //3. sqrMagnitude (정확한 값은 아니고 크기 비교만 할 때 사용)
        //성능상 유리, 루트연산을 하지 않는다
        float distance1 = (target3st.position - transform.position).sqrMagnitude;
        if (distance1 < 2.0f) transform.position = target3st.position;
    }
}
