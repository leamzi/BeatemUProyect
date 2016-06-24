using UnityEngine;
using System.Collections;
using InControl;

public abstract class PlayableController : MonoBehaviour {

    private PlayableActions _playable_actions;
    private iPlayableState _state;

    public PlayableEntity playable_entity;


    private void Start ()
    {
        _playable_actions = new PlayableActions();

        _playable_actions.Left.AddDefaultBinding(Key.A);
        _playable_actions.Right.AddDefaultBinding(Key.D);
        _playable_actions.Down.AddDefaultBinding(Key.S);
        _playable_actions.Up.AddDefaultBinding(Key.W);
        _playable_actions.Attack.AddDefaultBinding(Key.E);

        _state = GetDefaultState();
        _state.Enter(playable_entity);
    }

    private void Update()
    {
        iPlayableState new_state = _state.HandleInput(playable_entity, _playable_actions);
        if ( new_state != null)
        {
            _state = new_state;
            _state.Enter(playable_entity);
        }
    }



    protected abstract iPlayableState GetDefaultState();

}
