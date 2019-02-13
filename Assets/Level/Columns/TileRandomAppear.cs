using UnityEngine;

public class TileRandomAppear : MonoBehaviour
{
    public MinMax moveInterval;
    private float moveTime = 5f;
    private float nextMoveTime = 1f;
    
    public MinMax keepAliveRange;
    public Transform[] definedSpots;

    public LayerMask dontPlaceOn;

    public GameObject prefabTileToAppear;

    private void Update()
    {
        if (!(Time.time > nextMoveTime)) return;

        PlaceRandomTile();
        moveTime = Random.Range(moveInterval.minValue, moveInterval.maxValue);
        nextMoveTime = Time.time + moveTime;
    }

    private void PlaceRandomTile()
    {        
        Vector3 pos = Vector3.zero;

        if (definedSpots.Length > 0)
        {
            pos = definedSpots[Random.Range(0, definedSpots.Length)].position;
        }
        else
        {
            var randX = Random.Range(-12, 0);
            var randY = Random.Range(-4, 4);
        
            pos = new Vector3(randX, randY, 0);
        }
                        
        var t = Instantiate(prefabTileToAppear, pos, Quaternion.identity, transform);
        if (IsOccupiedPosition(t.transform.position))
        {
            Destroy(t);
            nextMoveTime = Time.time;
            return;
        }
        
        TriggerProjectile tProj = t.GetComponent<TriggerProjectile>();
        tProj.moveSpeed = 5f;

        Destroy(t, Random.Range(keepAliveRange.minValue, keepAliveRange.maxValue));
    }

    private bool IsOccupiedPosition(Vector2 pos)
    {        
        //Debug.DrawRay(pos, Vector3.right, Color.red, 0.25f);
        //var hit = Physics2D.Raycast(pos, Vector2.right, 0.25f, dontPlaceOn);
        //if (hit) Debug.Log(hit.collider.name);
        return Physics2D.Raycast(pos, Vector2.right, 0.25f, dontPlaceOn);
    }
}