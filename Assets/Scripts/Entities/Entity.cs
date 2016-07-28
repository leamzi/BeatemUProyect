using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour
{
    protected Vector2 ClampPosition(Vector2 vector_pos)
    {
        vector_pos.x = (int)vector_pos.x;
        vector_pos.y = (int)vector_pos.y;
        return vector_pos;
    }

    public void Awake()
    {
        OnAwake();
    }

    public void Start()
    {
        OnStart();
    }

    public void Update()
    {
        OnUpdate();
    }

    private void LateUpdate()
    {
        OnLateUpdate();

        Vector3 clamped_position = ClampPosition(transform.position);
        clamped_position.z = clamped_position.y;
        transform.position = clamped_position;
    }

    // --- ABSTACT METHODS --- //
    protected abstract void OnStart();
    protected abstract void OnLateUpdate();
    protected abstract void OnAwake();
    protected abstract void OnUpdate();
    
}
