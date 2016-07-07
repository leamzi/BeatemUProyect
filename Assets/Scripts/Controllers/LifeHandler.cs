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

        }
    }
}
