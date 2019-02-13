using UnityEngine;

public class Buff : MonoBehaviour
{
    public string title;
    public float lengthToStay = 10f;
    public GameObject buffBadgePrefab;
    [HideInInspector] public GameObject buffBadge;

    protected Transform displayAt;

    protected void Awake()
    {
        displayAt = FindObjectOfType<BuffBadgePanel>().transform;
    }
}git