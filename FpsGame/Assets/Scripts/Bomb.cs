using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //수류탄의 역할?
    //슈팅게임의 총알은 생성하면 스스로 날아가다 충돌하면 터지도록 구현됨
    //하지만 수류탄은 생성되자마자 스스로 이동하면 안된다.
    //수류탄은 플레이어가 직접 던져야한다
    //수류탄이 다른 오브젝트들과 충돌하면 터지고 자신도 사라져야한다.
    
    public GameObject fxFactory; // 이펙트 프리팹 

    private void OnCollisionEnter(Collision collision)
    {
        //폭발이펙트 보여주기

        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;

        //다른 오브젝트 삭제
        Destroy(gameObject);
    }

}