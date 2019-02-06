using UnityEngine;

public class TriggerTarget : TriggerProjectile
{
    public string tagToTarget;
    
    private void Awake()
    {
        target = GameObject.FindWithTag(tagToTarget).transform;
    }
}