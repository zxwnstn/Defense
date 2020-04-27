﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTower : MonoBehaviour
{
    ObjectSelector objectSelector;
    GameObject ButtonSellector;
    UiManager UiManager;
    GameObject GenTowers;
    GameManager gameManager;

    public GameObject[] towers;

    void Start()
    {
        var Camera = GameObject.Find("MainCamera");
        var Canvas = GameObject.Find("Canvas");
        var uiManager = GameObject.Find("UiManager");
        gameManager = GameManager.Get();

        UiManager = uiManager.GetComponent<UiManager>();
        objectSelector = Camera.GetComponent<ObjectSelector>();
        ButtonSellector = Canvas.transform.GetChild(0).gameObject;

        towers = new GameObject[Type.Tower.Max];
        towers[Type.Tower.Arrow] = Resources.Load("Tower/ArcherTower") as GameObject;
        towers[Type.Tower.Cannon] = Resources.Load("Tower/CanonTower") as GameObject;

        GenTowers = GameObject.Find("GenTowers");
    }

    public void GenArrowTower()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        Gen(0);
    }

    public void GenMissileTower()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        Gen(1);
    }

    public void GenMagicTower()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        Gen(2);
    }

    public void GenCannonTower()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        Gen(3);
    }

    public void Gen(int towerIndex)
    {
        GameObject tower = Instantiate(towers[towerIndex]) as GameObject;
        tower.transform.parent = GenTowers.transform;
        switch (towerIndex)
        {
            case 0:
                tower.transform.GetChild(0).name = "ArcherTower";
                break;
            case 3:
                tower.transform.GetChild(0).name = "CannonTower";
                break;
        }

        Vector3 towerPos = objectSelector.selectedBuildPointPos;
        objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().SetBuilding(true);
        objectSelector.selectedBuildingPoint = null;

        tower.transform.position = towerPos;
        ButtonSellector.SetActive(false);
        tower.SetActive(true);
    }

}