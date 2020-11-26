using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 20;
    public Vector3 defaultPos;
    public float followDistance;

    public float speed = 5f;

    private void Start()
    {
        defaultPos = transform.position;
    }

    public void Damaged()
    {
        hp--;

        if (hp <= 0) Destroy(gameObject);
    }
}
