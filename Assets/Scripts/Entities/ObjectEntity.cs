using UnityEngine;
using System.Collections;

public abstract class ObjectEntity : Entity
{
    protected abstract void OnStart();
    protected abstract void OnLateUpdate();
    protected abstract void OnAwake();
    protected abstract void OnUpdate();
}
