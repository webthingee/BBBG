﻿using UnityEngine;

public class PathColumnsGenerator : MonoBehaviour
{
    public int columns;
    public GameObject randomColumn;

    public int initialOpen;
    public int endOpen;
    public GameObject openColumn;

    private void Start()
    {
        PlaceColumns();
    }

    private void PlaceColumns()
    {
        GameObject columnToPlace = randomColumn;
        
        for (int i = 0; i < columns; i++)
        {
            if (i <= initialOpen)
            {
                columnToPlace = openColumn;
            }
            else if (endOpen > 0 && i >= columns - endOpen)
            {
                columnToPlace = openColumn;
            }
            else
            {
                columnToPlace = randomColumn;
            }
            
            Vector3 pos = transform.position + Vector3.right * i;
            Instantiate(columnToPlace, pos, Quaternion.identity, transform);
        }
        
        Invoke("MinimizeStoreTriggers", 2f);
    }

    private void MinimizeStoreTriggers()
    {
        TriggerStore[] stores = FindObjectsOfType<TriggerStore>();
        if (stores.Length <= 2) return;
            
        for (int i = stores.Length - 1; i > 0; i--)
        {
            if (i != (int) stores.Length / 2)
            {
                Destroy(stores[i].gameObject);
            }
        }
    }
}