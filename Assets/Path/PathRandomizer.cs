using System.Collections.Generic;
using UnityEngine;

public class PathRandomizer : MonoBehaviour
{
    public List<PathTypes> path = new List<PathTypes>();

    public int pathHeight = 8;
    public int chanceOfOpen = 75;
    public int chanceOfEnemy = 95;
    
    public GameObject prefabOpenPath;
    public GameObject prefabBlockedPath;
    public GameObject prefabEnemy;
    
    void Start()
    {
        BuildPath();
        Invoke("DisplayPath", 1f);
    }

    private void BuildPath()
    {        
        path.Add(PathTypes.X);
        
        for (int i = 0; i < pathHeight; i++)
        {            
            int rand = Random.Range(0, 101);
            
            if (rand <= chanceOfOpen) path.Add(PathTypes.O);
            
            if (rand > chanceOfOpen && rand <= chanceOfEnemy) path.Add(PathTypes.X);
            
            if (rand > chanceOfEnemy && rand <= 100) path.Add(PathTypes.E); // change to an insert to open within 4 or more?

            //System.Array A = System.Enum.GetValues(typeof(PathTypes));
            //PathTypes V = (PathTypes)A.GetValue(Random.Range(0,A.Length));            
        }
        
        path.Add(PathTypes.X);
    }

    private void DisplayPath()
    {
        GameObject prefabToPlace = prefabOpenPath;

        for (int index = 0; index < path.Count; index++)
        {
            PathTypes p = path[index];
            
            if (p == PathTypes.O) prefabToPlace = prefabOpenPath;
            if (p == PathTypes.X) prefabToPlace = prefabBlockedPath;
            if (p == PathTypes.E) prefabToPlace = prefabEnemy;

            var prefab = Instantiate(prefabToPlace, transform.position, Quaternion.identity, transform);
            Vector3 pos = transform.position + Vector3.up * index;
            prefab.transform.position = pos;
        }
    }
}