using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bullet;

    private Transform atkPos;
    private Animator anim;

    public int hp = 20;
    public float speed = 5f;
    public Vector3 defaultPos;
    public float followDistance;

    bool isDie = false;

    private void Start()
    {
        defaultPos = transform.position;
        atkPos = GameObject.Find("shotPos").transform;
        anim = GetComponentInChildren<Animator>();
    }

    public void Fire()
    {
        GameObject prefab = Instantiate(bullet, atkPos.position, Quaternion.identity);
        Rigidbody rigid = prefab.GetComponent<Rigidbody>();

        Vector3 dir = transform.forward.normalized;

        rigid.AddForce(dir * 20f, ForceMode.Impulse);
    }

    public void Damaged()
    {
        if (isDie) return;

        hp--;

        EnemyFSM FSM = GetComponent<EnemyFSM>();

        if (hp <= 0)
        {
            isDie = true;

            FSM.EnemDie();

            Destroy(gameObject, 3f);
        }

        else
        {
            FSM.DamagedAnim();
        }
    }
}
