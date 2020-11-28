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
        atkPos = transform.Find("AtkPos");
        anim = GetComponentInChildren<Animator>();
    }

    public void Fire()
    {
        GameObject prefab = Instantiate(bullet, atkPos.position, Quaternion.identity);
        Rigidbody rigid = prefab.GetComponent<Rigidbody>();

        Vector3 dir = transform.forward.normalized;

        rigid.AddForce(dir * 20f, ForceMode.Impulse);
        /*
         * 
         *    Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up * 2;
        //    dir.Normalize();
        //    rigid.AddForce(dir * 25f, ForceMode.Impulse);
         * 
         */
        //Vector3 dir = transform.forward + transform.up * 2;
        //dir.Normalize();
        //rigid.AddForce(dir * 20f, ForceMode.Impulse);
    }

    public void Damaged()
    {
        if (isDie) return;

        hp--;

        if (hp <= 0)
        {
            isDie = true;

            anim.SetTrigger("die");

            GetComponent<EnemyFSM>().EnemDie();
            Destroy(gameObject, 3f);
        }

        else
        {
            anim.SetTrigger("hit");
        }
    }
}
