using UnityEngine;

public class Turret : MonoBehaviour
{
    private void OnDestroy()
    {
        ProjectileSpawner ps = GetComponentInParent<ProjectileSpawner>();
        if (ps != null) ps.turretsList.Remove(transform);
    }
}