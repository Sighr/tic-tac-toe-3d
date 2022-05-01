using System;
using UnityEngine;

public class ArrowData : MonoBehaviour
{
    public enum Mode
    {
        In = 0,
        Out = 2,
        Reset = 1
    }
    
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        Forward,
        Back,
        All
    }
    
    public Mode mode;
    public Direction direction;
    
    public void ModifyHiddenIndices(HideArrowsController.HiddenIndices indices)
    {
        if (mode == Mode.Reset)
        {
            indices.Reset();
            return;
        }
        switch (direction)
        {
            case Direction.Up:
                indices.up += (int) mode - 1;
                break;
            case Direction.Down:
                indices.down -= (int) mode - 1;
                break;
            case Direction.Right:
                indices.right += (int) mode - 1;
                break;
            case Direction.Left:
                indices.left -= (int) mode - 1;
                break;
            case Direction.Forward:
                indices.forward += (int) mode - 1;
                break;
            case Direction.Back:
                indices.back -= (int) mode - 1;
                break;
            case Direction.All:
                indices.up += (int) mode - 1;
                indices.down -= (int) mode - 1;
                indices.right += (int) mode - 1;
                indices.left -= (int) mode - 1;
                indices.forward += (int) mode - 1;
                indices.back -= (int) mode - 1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
