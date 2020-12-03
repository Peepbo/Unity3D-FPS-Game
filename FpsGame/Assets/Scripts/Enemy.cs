using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public GameObject bullet;

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
        //GameObject prefab = Instantiate(bullet, atkPos.position, Quaternion.identity);
        //Rigidbody rigid = prefab.GetComponent<Rigidbody>();

        //Vector3 dir = transform.forward.normalized;

        //rigid.AddForce(dir * 20f, ForceMode.Impulse);

        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Enemy Bullet");

        if(bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.SetActive(true);

            Rigidbody rigid = bullet.GetComponent<Rigidbody>();

            Vector3 dir = transform.forward.normalized;

            rigid.AddForce(dir * 20f, ForceMode.Impulse);
        }
    }

    public void Damaged()
    {
        EnemyFSM FSM = GetComponent<EnemyFSM>();

        if (isDie || FSM.isDamaged()) return;

        hp--;


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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(defaultPos, followDistance);
    }
}
