using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Utilities
{
    public enum DIRECTIONS
    {
        Up,
        Down,
        Left,
        Right
    }

    public static Vector2 GetDirection(DIRECTIONS direction)
    {
        switch (direction)
        {
            case DIRECTIONS.Up:
                return Vector3.up;
            case DIRECTIONS.Down:
                return Vector3.down;
            case DIRECTIONS.Left:
                return Vector3.left;
            case DIRECTIONS.Right:
                return Vector3.right;
            default:
                return Vector2.zero;
        }
    }

    public enum GAMECOLORS
    {
        White = 0,
        Red = 1,
        Blue = 2,
        Yellow = 4,
        Purple = 3,
        Orange = 5,
        Green = 6,
    }

    /// <summary>
    /// Add a color to another one
    /// </summary>
    /// <param name="color1">Initial color</param>
    /// <param name="color2">Color to add</param>
    /// <returns>Final color</returns>
    public static GAMECOLORS GetMixedColor(GAMECOLORS color1, GAMECOLORS color2)
    {
        return (GAMECOLORS)((int)(color1 | color2)%7);
    }

    /// <summary>
    /// Subtract a color to another one
    /// </summary>
    /// <param name="color1">Initial color</param>
    /// <param name="color2">Color subtracted</param>
    /// <returns>Final color</returns>
    public static GAMECOLORS GetSubtractedColor(GAMECOLORS color1, GAMECOLORS color2)
    {
        return color1 - (int)(color1 & color2);
        //return (GAMECOLORS)((((int)color1 != 0 ? (int)color1 : 7) - (((int)color1 != 0 ? (int)color1 : 7) & (int)color2)) %7);
    }

    /// <summary>
    /// Turn the GAMECOLORS value into a Color struct
    /// </summary>
    /// <param name="color">Color enum value</param>
    /// <returns>Color struct</returns>
    public static Color GetColor(GAMECOLORS color)
    {
        switch (color)
        {
            case GAMECOLORS.White:
                return Color.white;
            case GAMECOLORS.Red:
                return Color.red;
            case GAMECOLORS.Blue:
                return Color.blue;
            case GAMECOLORS.Yellow:
                return Color.yellow;
            case GAMECOLORS.Purple:
                return new Color(0.8f, 0.0f, 0.8f, 1.0f);
            case GAMECOLORS.Orange:
                return new Color(1.0f, 0.64f, 0.0f, 1.0f);
            case GAMECOLORS.Green:
                return Color.green;
            default:
                return Color.white;
        }
    }

    public static Dictionary<GAMECOLORS, LayerMask> MovementLayers = new Dictionary<GAMECOLORS, LayerMask>()
    {
        {GAMECOLORS.White, LayerMask.GetMask("WhiteWall", "Player", "OnlyPlayers", "Border")},
        {GAMECOLORS.Red, LayerMask.GetMask("WhiteWall", "Player", "BlueWall", "YellowWall", "GreenWall", "OnlyPlayers", "Border")},
        {GAMECOLORS.Blue, LayerMask.GetMask("WhiteWall", "Player", "RedWall", "YellowWall", "OrangeWall", "OnlyPlayers", "Border")},
        {GAMECOLORS.Yellow, LayerMask.GetMask("WhiteWall", "Player", "RedWall", "BlueWall", "PurpleWall", "OnlyPlayers", "Border")},
    };

    public static LayerMask LightLayerMask = 
        LayerMask.GetMask("WhiteWall", "Player", "RedWall", "BlueWall", "YellowWall", "PurpleWall", "OrangeWall", "GreenWall", "OnlyLight", "Border");

    public static int GetClosestInteger(float value)
    {
        if (Mathf.Abs(Mathf.Ceil(value) - value) < Mathf.Abs(Mathf.Floor(value) - value))
        {
            return Mathf.CeilToInt(value);
        }
        else
        {
            return Mathf.FloorToInt(value);
        }
    }
}
