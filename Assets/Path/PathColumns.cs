using UnityEngine;

public class PathColumns : MonoBehaviour
{
    public GameObject pathRandom;
    public int columns;

    private float moveInterval = 1f;
    private float nextMoveTime;

    private void Awake()
    {
        nextMoveTime = moveInterval;
    }

    private void Start()
    {
        for (int i = 0; i < columns; i++)
        {
            var p = Instantiate(pathRandom, transform.position, Quaternion.identity, transform);
            Vector3 pos = transform.position + Vector3.right * i;
            p.transform.position = pos;
        }
    }

    private void Update()
    {
        
        if (Time.time < nextMoveTime) return;
        
        transform.position = transform.position + Vector3.left * 0.5f;

        nextMoveTime = Time.time + moveInterval;
    }
}