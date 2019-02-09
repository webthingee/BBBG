using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsDisplay : MonoBehaviour
{
    public GameObject heartPrefab;
    
    private List<GameObject> heartsList = new List<GameObject>();

    private void Start()
    {
        UpdateHeartsDisplay(7, 3);
    }

    public void UpdateHeartsDisplay(int maxHealth, int currentHealth)
    {
        foreach (GameObject heart in heartsList)
        {
            Destroy(heart);
        }

        heartsList.Clear();
        
        for (int i = 0; i < maxHealth; i++)
        {
            var h = Instantiate(heartPrefab, transform.position, Quaternion.identity, transform);
            h.GetComponent<Image>().color = Color.black;
            heartsList.Add(h);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            heartsList[i].GetComponent<Image>().color = Color.white;
        }
    }
}
