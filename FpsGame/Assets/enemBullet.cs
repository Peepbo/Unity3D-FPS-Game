using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemBullet : MonoBehaviour
{
    public GameObject fxFactory; // 이펙트 프리팹 
    public float LifeTime = 5f;

    private void Start()
    {
        StartCoroutine(Coru_lifeTime(LifeTime));
    }

    IEnumerator Coru_lifeTime(float sec)
    {
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);
    }
}
