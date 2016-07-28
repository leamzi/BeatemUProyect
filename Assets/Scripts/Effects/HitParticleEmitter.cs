using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityController))]
public class HitParticleEmitter : MonoBehaviour
{
    public Transform particle_prefab;

    private EntityController _controller;

    private void Start()
    {
        _controller = GetComponent<EntityController>();
        if (_controller != null)
        {
            _controller.OnHit += OnHit;
        }
    }

    private void OnHit(Transform damage_dealer, int damages, Vector3 hit_position)
    {   
        if (particle_prefab != null)
        {
            Instantiate(particle_prefab, transform.position, new Quaternion());
        }
    }
}
