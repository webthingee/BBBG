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
    public GameObject prefabEnemy;
    public GameObject prefabCoin;
    public GameObject prefabHealing;

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
            
        if (rand <= chanceOfOpen) return PathTypes.O;
        
        if (rand > chanceOfOpen && rand <= chanceOfEnemy) return PathTypes.X;

        if (rand > chanceOfEnemy && rand <= 100) return PathTypes.E;

        return PathTypes.X;
    }

    private void BuildRandomColumnForLevel()
    {        
        path.Add(PathTypes.X);
        
        for (int i = 0; i < pathHeight; i++)
        {
            path.Add(PathTypeRandom());
        }
        
        path.Add(PathTypes.X);
    }
    
    private void BuildOpenColumnForLevel()
    {        
        path.Add(PathTypes.X);
        
        for (int i = 0; i < pathHeight; i++)
        {
            path.Add(PathTypes.O);
        }
        
        path.Add(PathTypes.X);
    }

    private void DisplayColumnFromList()
    {
        GameObject prefabToPlace = prefabOpenPath[0];

        for (int index = 0; index < path.Count; index++)
        {
            PathTypes p = path[index];

            if (p == PathTypes.O)
            {
                prefabToPlace = prefabOpenPath[Random.Range(0, prefabOpenPath.Length)];
                
                if (Random.Range(0, 101) < 1)
                {
                    prefabToPlace = prefabCoin;
                }
                
                if (Random.Range(0, 101) < 1)
                {
                    prefabToPlace = prefabHealing;
                }
                
            }

            if (p == PathTypes.X)
            {
                prefabToPlace = prefabBlockedPath[0];
                
                if (Random.Range(0, 101) < 22)
                {
                    prefabToPlace = prefabBlockedPath[1];
                } 
            }
            
            if (p == PathTypes.E) prefabToPlace = prefabEnemy;

            Vector3 pos = transform.position + Vector3.up * index;
            Instantiate(prefabToPlace, pos, Quaternion.identity, transform);
        }
    }
}