using UnityEngine;

public class TriggerTargetOnList : TriggerProjectile
{
    private Transform[] targets;
    
    private void Start()
    {
        targets = FindObjectOfType<ProjectileSpawner>().turretsList.ToArray();

        if (targets.Length > 0)
        {
            target = targets[Random.Range(0, targets.Length)];
        }
    }
}