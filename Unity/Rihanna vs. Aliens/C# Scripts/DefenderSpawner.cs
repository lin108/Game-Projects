﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;
    GameObject defenderParent;
    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        CreatDefenderParent();
    }

    private void CreatDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        StarDisplay starDispaly = FindObjectOfType<StarDisplay>();

        if (defender)
        {
            int defenderCost = defender.GetStarCost();
            if (starDispaly.HaveEnoughStars(defenderCost))
            {
                SpawnDefender(gridPos);
                starDispaly.SpendStars(defenderCost);
            }

        }

        
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        Vector2 wolrdPos = new Vector2(newX, newY);
        return wolrdPos;

    }

    private void SpawnDefender(Vector2 worldPos)
    {
        if (defender)
        {
            Defender newDefender = Instantiate(defender, worldPos, transform.rotation) as Defender;
            newDefender.transform.parent = defenderParent.transform;
        }  
    }
}
