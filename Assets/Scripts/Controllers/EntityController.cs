using UnityEngine;
using System.Collections;

public class EntityController : MonoBehaviour
{
    public delegate void EventOnHit(Transform dealer_transform, int hit_damage);
    public EventOnHit OnHit;

}
