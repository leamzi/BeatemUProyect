using UnityEngine;
using System.Collections;
using InControl;

public class PlayableActions: PlayerActionSet
{
    public PlayerAction Attack;
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Down;
    public PlayerAction Up;
    public PlayerTwoAxisAction Move;

    public PlayableActions()
    {
        Attack = CreatePlayerAction("Attack");
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Up = CreatePlayerAction("Move Up");
        Down = CreatePlayerAction("Move Down");

        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
    }
}

