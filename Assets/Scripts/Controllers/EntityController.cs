using UnityEngine;
using System.Collections;

public abstract class EntityController : MonoBehaviour
{
    public delegate void EventOnHit(Transform dealer_transform, int hit_damage);
    public EventOnHit OnHit;

    public delegate void EventOnDeath();
    public EventOnDeath OnDeath;

    public abstract void SetHit(Transform dealer_transform, int hit_damage);
    public abstract void SetDeath();
}
