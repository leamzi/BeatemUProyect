using UnityEngine;
using System.Collections;

public class HitFx : MonoBehaviour
{
    private tk2dSpriteAnimator _animator;

    private void Awake ()
    {
        _animator = GetComponent<tk2dSpriteAnimator>();

        if (_animator == null)
        {
            Debug.LogError("Unable to find the animator.");
        }
        _animator.Play();
    }

    private void Update()
    {
        if (_animator.IsPlaying(_animator.CurrentClip.name) == false)
        {
            Destroy(gameObject);
        }
    }
}
