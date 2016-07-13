using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityController))]
public class LifeHandler : MonoBehaviour
{
    private int _start_life;
    private int _current_life;

    public EntityController _controller;

    public int start_life;
    public int current_life { get { return _current_life; } }

    private void Awake()
    {
        _controller = GetComponent<EntityController>();
        if (_controller != null)
        {
            _controller.OnHit += OnHit;
        }

        _start_life = start_life;
        _current_life = _start_life;
    }

    private void OnHit(Transform dealer_transform, int hit_damage)
    {
        _current_life = Mathf.Max(0, _current_life - hit_damage);

        if (_current_life == 0)
        {
            _controller.SetDeath();
        }
    }
}
