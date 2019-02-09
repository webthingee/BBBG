using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetsDisplay : MonoBehaviour
{
    public GameObject beetPrefab;
    
    private List<GameObject> beetsList = new List<GameObject>();

    public void UpdateBeetsDisplay(int beets)
    {
        foreach (GameObject beet in beetsList)
        {
            Destroy(beet);
        }

        beetsList.Clear();
        
        for (int i = 0; i < beets; i++)
        {
            GameObject h = Instantiate(beetPrefab, transform.position, Quaternion.identity, transform);
            beetsList.Add(h);
        }
    }
}