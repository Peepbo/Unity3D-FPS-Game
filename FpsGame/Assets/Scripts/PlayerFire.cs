using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject effect;

    public Animator gun_anim;

    public GameObject bomb_prefab;
    public Transform firePoint;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 30f, Color.green);

        Fire(); 
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100f))
            {
                gun_anim.SetTrigger("Fire");
                print(hit.collider.name);

                Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));

                if(hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<Enemy>().Damaged();
                    hit.collider.GetComponent<EnemyFSM>().animTrigger("damaged");
                }
            }

            //유니티 최적화
            //1. 오브젝트 풀링
            //2. 정점수 줄이기 LOD
            //3. 파티클에서 스프라이트 사용하기
            //4. 비어있는 함수 제거하기
            //5. 레이어 마스크 사용 충돌처리

            //유니티 내부적으로 속도향상을 위해 비트연산 처리가 된다.
            //총 32비트를 사용하기 때문에 레이어도 32개까지 추가 가능함

            //int layer = gameObject.layer;
            //layer = 1 << 8 | 1 << 9 | 1 << 12;

            //0000 0001 0000 0000 => Player
            //0000 0010 0000 0000 => Enemy
            //0000 0100 0000 0000 => Boss
            //0000 0111 0000 0000 => P, E, B 모두 충돌처리 가능

            //if(Physics.Raycast(ray, out hit, 100, layer))
            //if(Physics.Raycast(ray, out hit, 100, ~layer))

            //if(플레이어와 충돌)
            //if(에너미와 충돌)
            //if(보스와 충돌)

            //이런식이면 if문이 더 많이 들어가게 된다.
        }

        if(Input.GetButtonDown("Fire2"))
        {
            //수류탄 생성
            GameObject bomb = Instantiate(bomb_prefab, firePoint.position, Quaternion.identity);
            Rigidbody rigid = bomb.GetComponent<Rigidbody>();

            //rigid.AddForce(Camera.main.transform.forward * 25f, ForceMode.Impulse);

            //45도 각도로 발사
            Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up * 2;
            dir.Normalize();
            rigid.AddForce(dir * 25f, ForceMode.Impulse);
        }
    }
}
