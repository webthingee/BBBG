using System.Collections.Generic;
using UnityEngine;

public class TileColumnGenerator : MonoBehaviour
{
    public bool openColumn;
    public List<PathTypes> path = new List<PathTypes>();

    public int pathHeight = 8;
    public int chanceOfOpen = 75;
    public int chanceOfEnemy = 95;
    
    public GameObject[] prefabOpenPath;
    public GameObject[] prefabBlockedPath;
    public GameObject[] prefabBarrierPath;
    public GameObject prefabEnemy;
    public GameObject prefabCoin;
    public GameObject prefabHealing;
    public GameObject prefabStore;

    private void Start()
    {
        if (!openColumn)
        {
            BuildRandomColumnForLevel();
        }
        else
        {
            BuildOpenColumnForLevel();
        }
        
        Invoke("DisplayColumnFromList", 1f);
    }

    private PathTypes PathTypeRandom()
    {
        int rand = Random.Range(0, 101);
            
        if (rand <= chanceOfOpen) return PathTypes.Open;
        
        if (rand > chanceOfOpen && rand <= chanceOfEnemy) return PathTypes.Blocker;

        if (rand > chanceOfEnemy && rand <= 100) return PathTypes.Enemy;

        return PathTypes.Blocker;
    }

    private void BuildRandomColumnForLevel()
    {        
        path.Add(PathTypes.Barrier);
        
        for (int i = 0; i < pathHeight; i++)
        {
            path.Add(PathTypeRandom());
        }
        
        path.Add(PathTypes.Barrier);
    }
    
    private void BuildOpenColumnForLevel()
    {        
        path.Add(PathTypes.Barrier);
        
        for (int i = 0; i < pathHeight; i++)
        {
            path.Add(PathTypes.Open);
        }
        
        path.Add(PathTypes.Barrier);
    }

    private void DisplayColumnFromList()
    {
        GameObject prefabToPlace = prefabOpenPath[0];

        for (int index = 0; index < path.Count; index++)
        {
            PathTypes p = path[index];
            
            // we want a background tile every time.
            prefabToPlace = prefabOpenPath[Random.Range(0, prefabOpenPath.Length)];
            
            Vector3 blankTilePos = transform.position + Vector3.up * index;
            Instantiate(prefabToPlace, blankTilePos, Quaternion.identity, transform);

            // we want to add random tiles when/where needed
            if (p == PathTypes.Open)
            {                
                if (openColumn) continue;
                
                if (Random.Range(0, 101) < 2)
                {
                    prefabToPlace = prefabStore;
                }
                
                if (Random.Range(0, 101) < 5)
                {
                    prefabToPlace = prefabCoin;
                }
                
                if (Random.Range(0, 101) < 2)
                {
                    prefabToPlace = prefabHealing;
                }
                
            }

            if (p == PathTypes.Blocker)
            {
                prefabToPlace = prefabBlockedPath[0];
                
                if (Random.Range(0, 101) < 40)
                {
                    prefabToPlace = prefabBlockedPath[1];
                } 
            }

            if (p == PathTypes.Enemy)
            {
                prefabToPlace = prefabEnemy;
            }

            if (p == PathTypes.Barrier)
            {
                prefabToPlace = prefabBarrierPath[Random.Range(0, prefabBarrierPath.Length)]; 
            }
            
            Vector3 pos = transform.position + Vector3.up * index;
            Instantiate(prefabToPlace, pos, Quaternion.identity, transform);
        }
    }
}