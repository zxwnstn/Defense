﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

    public Rigidbody canonRigidBody;
    public float gravity;
    public Vector3 velocity;

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    private void Start()
    {
        canonRigidBody = GetComponent<Rigidbody>();
        gravity = 50.0f;
    }

    void Update()
    {
        velocity = new Vector3(velocity.x, velocity.y - gravity * Time.deltaTime, velocity.z);
        canonRigidBody.transform.position += velocity * Time.deltaTime;
        transform.up = velocity;                          //Y축(머리)을 해당 방향으로 설정
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GROUND")
        {
            this.gameObject.SetActive(false);

            var hitEffect = EffectManager.instance.GetBullet();  //이펙트 생성
            hitEffect.transform.position = this.transform.position;
            hitEffect.SetActive(true);


            Collider[] hitsCol = Physics.OverlapSphere(transform.position, 10.0f);
            foreach (Collider hit in hitsCol)
            {
                if (hit.gameObject.tag == "ENEMY")
                {
                    Destroy(hit.gameObject);
                }
            }

        }
    }
}
