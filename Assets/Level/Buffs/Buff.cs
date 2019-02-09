using UnityEngine;

public class Buff : MonoBehaviour
{
    public string title;
    public GameObject buffBadgePrefab;
    [HideInInspector] public GameObject buffBadge;

    protected Transform displayAt;

    protected void Awake()
    {
        displayAt = FindObjectOfType<BuffBadgePanel>().transform;
    }
}