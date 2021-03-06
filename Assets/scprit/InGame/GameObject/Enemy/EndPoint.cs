﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private const string enemyTag = "ENEMY";

    public GameObject[] enemyObj;
    public EnemySpawner[] enemySpawnerScript;

    private void Start()
    {
        var archer = GameObject.Find("ArcherSpawner");
        var mage = GameObject.Find("MageSpawner");
        var swordman = GameObject.Find("SwordmanSpawner");

        enemyObj = new GameObject[3] { archer, mage, swordman };

        enemySpawnerScript = new EnemySpawner[3]
        {
            enemyObj[0].GetComponent<EnemySpawner>(),
            enemyObj[1].GetComponent<EnemySpawner>(),
            enemyObj[2].GetComponent<EnemySpawner>()
        };
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == enemyTag && other.collider.name == "Archer")
        {
            enemySpawnerScript[0].currEnemy -= 1;
            other.gameObject.SetActive(false);
        }
        else if (other.collider.tag == enemyTag && other.collider.name == "Mage")
        {
            enemySpawnerScript[1].currEnemy -= 1;
            other.gameObject.SetActive(false);
        }
        else
        {
            enemySpawnerScript[2].currEnemy -= 1;
            other.gameObject.SetActive(false);
        }
    }
}
