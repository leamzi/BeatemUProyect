using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityController))]
public class LifeHandler : MonoBehaviour
{
    private EntityController _controller;

    private void Awake()
    {
        _controller = GetComponent<EntityController>();
        if (_controller != null)
        {
            _controller.OnHit += OnHit;
        }
    }

    private void OnHit(Transform dealer_transform, int hit_damage)
    {
        Debug.Log("Damage Recieved: " + hit_damage);
    }
}
