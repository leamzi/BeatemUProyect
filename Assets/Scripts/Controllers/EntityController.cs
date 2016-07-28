using UnityEngine;
using System.Collections;

public abstract class EntityController : MonoBehaviour
{
    public delegate void EventOnHit(Transform dealer_transform, int hit_damage, Vector3 hit_position);
    public EventOnHit OnHit;

    public delegate void EventOnDeath();
    public EventOnDeath OnDeath;

    public abstract void SetHit(Transform dealer_transform, int hit_damage, Vector3 hit_position);
    public abstract void SetDeath();
}
