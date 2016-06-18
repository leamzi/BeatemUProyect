using UnityEngine;
using System.Collections;
using InControl;

public class PlayerActions: PlayerActionSet
{
    public PlayerAction Attack;
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Down;
    public PlayerAction Up;
    public PlayerTwoAxisAction Move;

    public PlayerActions()
    {
        Attack = CreatePlayerAction("Attack");
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Up = CreatePlayerAction("Move Up");
        Down = CreatePlayerAction("Move Down");

        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
    }
}

public class PlayerController : MonoBehaviour {

    private PlayerActions _playable_actions;

    private void Start ()
    {
        _playable_actions = new PlayerActions();

        _playable_actions.Left.AddDefaultBinding(Key.A);
        _playable_actions.Right.AddDefaultBinding(Key.D);
        _playable_actions.Down.AddDefaultBinding(Key.S);
        _playable_actions.Up.AddDefaultBinding(Key.W);

    }

    private void Update ()
    {
        //Hack Test
        if (_playable_actions.Move.IsPressed)
        {
            PlayableEntity player = GetComponent<PlayableEntity>();
            player.Move(_playable_actions.Move.Value);
        }
    }
}
