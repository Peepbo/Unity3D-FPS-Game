using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    //enemy script
    Enemy enem;

    //enemy state
    enum EnemyState
    {
        Idle, Move, Attack, Damaged, Die
    }

    EnemyState state; // enemy state function

    GameObject Target;

    CharacterController cc;

    Animator anim;

    bool isAtk = false;

    NavMeshAgent nav;

    //usefull
    #region "Idle 상태에 필요한 변수들"
    #endregion

    #region "Move 상태에 필요한 변수들"
    #endregion

    #region "Attack 상태에 필요한 변수들"
    Coroutine atkCoru;
    #endregion

    #region "Damaged 상태에 필요한 변수들"
    Coroutine damageCoru;
    #endregion

    #region "Die 상태에 필요한 변수들"
    #endregion

    public void EnemDie()
    {
        anim.SetTrigger("die");
        state = EnemyState.Die;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;

        anim = GameObject.Find("PA_Warrior").GetComponent<Animator>();
        
        enem = GetComponent<Enemy>();
        Target = GameObject.Find("Player");
        cc = GetComponent<CharacterController>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {

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
            case EnemyState.Damaged:
                Damaged();
                break;
        }

        //print(state);
        //anim.SetInteger("state", (int)state);
        //nav.SetDestination(Target.transform.position);
    }

    public void DamagedAnim()
    {
        anim.SetTrigger("hit");
        state = EnemyState.Damaged;
    }

    public bool isDamaged()
    {
        return state == EnemyState.Damaged;
    }

    private void Damaged()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 >= 0.9f)
        {
            anim.SetInteger("state", 0);
            state = EnemyState.Idle;
        }
    }

    private void Idle()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PA_WarriorIdle_Clip") == false) return;
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

            //transform.LookAt(Target.transform.position);
            anim.SetInteger("state", 1);
            state = EnemyState.Move;
        }
    }

    private void Move()
    {
        float distance = Vector3.Distance(enem.defaultPos, Target.transform.position);

        //follow
        if (distance < enem.followDistance)
        {

            float targetDistance = (transform.position - Target.transform.position).sqrMagnitude;
            if(targetDistance < 30f)
            {
                anim.SetInteger("state", 2);
                state = EnemyState.Attack;
            }

            //transform.LookAt(Target.transform);

            //Vector3 dir = (Target.transform.position - transform.position).normalized;
            //cc.Move(dir * enem.speed * Time.deltaTime);

            nav.SetDestination(Target.transform.position);
        }

        //unfollow
        else
        {
            //transform.LookAt(enem.defaultPos);

            //Vector3 dir = (enem.defaultPos - transform.position).normalized;
            //cc.Move(dir * enem.speed * Time.deltaTime);

            nav.SetDestination(enem.defaultPos);

            if (Vector3.Distance(transform.position, enem.defaultPos) < 1.0f)
            {
                transform.position = enem.defaultPos;
                transform.rotation = Quaternion.identity;

                anim.SetInteger("state", 0);
                state = EnemyState.Idle;
            }
        }

        if (cc.isGrounded == false) cc.Move(Vector3.down * 50f);
    }

    void fire()
    {
        isAtk = true;

        enem.Fire();
        float distance = Vector3.Distance(transform.position, Target.transform.position);
        print(distance);
        
        if (distance > 5.5f)
        {
            anim.SetInteger("state", 1);
            state = EnemyState.Move;
            atkCoru = null;
        }
    }

    private void Attack()
    {
        nav.velocity = Vector3.zero;

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 >= 0.5f
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 <= 0.6f)
        {
            if(!isAtk) fire();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 >= 0.95f) isAtk = false;
    }
}
