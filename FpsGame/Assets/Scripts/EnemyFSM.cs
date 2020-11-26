using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    //enemy script
    Enemy enem;

    //enemy state
    enum EnemyState
    {
        Idle, Move, Attack, Return, Damaged, Die
    }

    EnemyState state; // enemy state function

    GameObject Target;

    CharacterController cc;

    Animator anim;

    //usefull
    #region "Idle 상태에 필요한 변수들"
    #endregion

    #region "Move 상태에 필요한 변수들"
    #endregion

    #region "Attack 상태에 필요한 변수들"
    #endregion

    #region "Return 상태에 필요한 변수들"
    #endregion

    #region "Damaged 상태에 필요한 변수들"
    Coroutine damageCoru;
    #endregion

    #region "Die 상태에 필요한 변수들"
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;

        anim = transform.parent.GetComponent<Animator>();
        enem = GetComponent<Enemy>();
        Target = GameObject.Find("Player");
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }

        //print(state);
        anim.SetInteger("state", (int)state);
    }
    private void Idle()
    {
        //Idle -> Move

        // 1. 플레이어와 일정범위가 되면 이동상태로 변경 (탐지범위)
        // - 플레이어 찾기 (GameObject.Find("Player"))
        // - 일정범위 20미터 (거리비교 : Distance, magnitude 아무거나 사용)
        // - 상태변경
        // - 상태전환 출력

        //방향 구하기
        Vector3 dir = (Target.transform.position - enem.defaultPos).normalized;

        //거리 구하기
        float distance = Vector3.Distance(enem.defaultPos, Target.transform.position);
        if (distance > enem.followDistance)
        {
            Debug.DrawRay(enem.defaultPos, dir * distance, Color.green);
        }

        else
        {
            //해당 방향 바라보기
            //transform.eulerAngles = new Vector3(0, angleX, 0);
            //transform.eulerAngles = new Vector3(0, dir.y, 0);
            transform.LookAt(Target.transform.position);

            state = EnemyState.Move;
        }
    }

    private void Move()
    {
        //Move -> Attack
        //Move -> Return

        //use to Character Controller

        transform.LookAt(Target.transform.position);

        float distance = Vector3.Distance(enem.defaultPos, Target.transform.position);
        if (distance > enem.followDistance)
        {
            state = EnemyState.Return;
        }

        else
        {
            if (distance <= 1)
            {
                state = EnemyState.Attack;
            }

            else
            {
                //방향 구하기

                //이동
                Vector3 dir = (Target.transform.position - transform.position).normalized;
                cc.Move(dir * enem.speed * Time.deltaTime);

                //레이저
                Vector3 rayDir = (Target.transform.position - enem.defaultPos).normalized;
                Debug.DrawRay(enem.defaultPos, rayDir * distance, Color.yellow);
            }
        }
    }

    private void Attack()
    {
        //Attack -> Idle
        //Attack -> Return
        //Attack -> Move

        //- 공격범위 1미터
        //float distance = Vector3.Distance(transform.position, Target.transform.position);

        //if(distance > enem.followDistance)
        //{

        //}
    }

    private void Return()
    {
        //- 처음 위치에서 30미터
        transform.LookAt(enem.defaultPos);

        float distance = Vector3.Distance(enem.defaultPos, transform.position);
        if (distance < 1f)
        {
            transform.position = enem.defaultPos;
            state = EnemyState.Idle;
        }

        else
        {
            Vector3 dir = (enem.defaultPos - transform.position).normalized;
            cc.Move(dir * enem.speed * Time.deltaTime);
        }
    }

    //Any State
    private void Damaged()
    {
        //- 코루틴 사용
        //1. 에너미 체력이 1이상일 때 피격받을 수 있다.
        //2. 다시 이전상태로 변경되야함
        //anim.SetTrigger("damaged");
    }

    public void animTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    //Any State
    private void Die()
    {
        //- 코루틴 사용
        //- 체력이 0 이하가 되면 오브젝트 삭제
    }
}
