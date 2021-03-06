﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanonSpawn : MonoBehaviour
{
    [SerializeField] public GameObject bullet = null;                //총알 프리팹
    [SerializeField] public GameObject gunBarrel = null;             //포신 프리팹
    [SerializeField] public Transform firePos = null;                //발사 위치

    private List<GameObject> collEnemys = new List<GameObject>();    //사거리 내에 들어온(충돌한) 객체를 담을 리스트
    [SerializeField] private float fireTimeMin = 0f;                 //발사 주기(최소)
    [SerializeField] private float fireTimeMax = 5.0f;               //발사 주기(최대)    //5초마다 쏘겠다

    public float theta = 45f;   //각도
    public float gravity;       //중력값
    public float v0;

    void Start()    //초기화
    {
        gravity = 9.8f;
        theta = 45f;
    }

    float Radian(float degree)
    {
        return degree * Mathf.PI / 180.0f;
    }

    void Update()
    {
        fireTimeMin += Time.deltaTime;  //발사주기 갱신

        if (collEnemys.Count > 0)   //충돌한 객체가 한놈이라도 있을 경우
        {
            GameObject target = collEnemys[0];          //첫번째로 충돌한 객체를 타겟으로 넣는다           
            if (target != null)
            {
                gunBarrel.transform.LookAt(target.transform.position);        //타겟을 향해 포신이 회전한다 (바라본다)

                if (fireTimeMin > fireTimeMax)
                {
                    fireTimeMin = 0.0f;

                    var aBolt = Instantiate(bullet, firePos.position, bullet.transform.rotation, transform);              //미사일 생성
                    var cannon = aBolt.GetComponent<Canon>();
                    var rigidbody = aBolt.GetComponent<Rigidbody>();
                    aBolt.transform.position = firePos.transform.position;
                    cannon.m_target = target;

                    Vector3 velocity = new Vector3(target.transform.position.x - cannon.transform.position.x, 0.0f, target.transform.position.z - cannon.transform.position.z);
                    velocity = Vector3.Normalize(velocity);
                    velocity.y = Mathf.Tan(Radian(theta));
                    velocity = Vector3.Normalize(velocity);

                    var dist = Vector3.Distance(cannon.transform.position, target.transform.position);
                    v0 = Mathf.Sqrt(gravity * dist / Mathf.Sin(Radian(2 * theta)));

                    rigidbody.velocity = velocity * v0;
                }
            }
            if (target == null)     //타겟이 없으면 리스트의 첫번째에 담은 녀석을 지운다
            {
                collEnemys.Remove(target);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)         //사거리내에 들어온 enemy태그가 붙은 객체를 리스트에 추가
    {
        if (collision.tag == "ENEMY")
            collEnemys.Add(collision.gameObject);
    }

    private void OnTriggerExit(Collider collision)          //사거리를 빠져나간 녀석은 리스트에서 지운다
    {
        foreach (GameObject enemy in collEnemys)
        {
            if (enemy == collision.gameObject)
            {
                collEnemys.Remove(enemy);
                break;
            }
        }
    }
}