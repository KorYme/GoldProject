using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LayersAndColor", menuName = "ScriptableObjects/LayersAndColor", order = 1)]
public class LayersAndColors : ScriptableObject
{
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
        int value = (int)color1 != 0 ? (int)color1 : 7;
        for (int i = 0; i < 3; i++)
        {
            value -= (((int)color2 >> i) % 2) * (int)Mathf.Pow(2, i);
        }
        return (GAMECOLORS)(value%7);
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
                return new Color(0.8f, 0.0f, 0.8f);
            case GAMECOLORS.Orange:
                return new Color(1.0f, 0.64f, 0.0f);
            case GAMECOLORS.Green:
                return Color.green;
            default:
                return Color.white;
        }
    }

    public static Dictionary<GAMECOLORS, LayerMask> MovementLayers = new Dictionary<GAMECOLORS, LayerMask>()
    {
        {GAMECOLORS.Red, LayerMask.GetMask("BasicWall", "Player", "BlueWall", "YellowWall", "GreenWall")},
        {GAMECOLORS.Blue, LayerMask.GetMask("BasicWall", "Player")},
        {GAMECOLORS.Yellow, LayerMask.GetMask("BasicWall", "Player")},
    };

    public static Dictionary<GAMECOLORS, LayerMask> LightLayers = new Dictionary<GAMECOLORS, LayerMask>()
    {
        {GAMECOLORS.White, LayerMask.GetMask("BasicWall", "Player")},
        {GAMECOLORS.Red, LayerMask.GetMask("BasicWall", "Player")},
        {GAMECOLORS.Blue, LayerMask.GetMask("BasicWall", "Player")},
        {GAMECOLORS.Yellow, LayerMask.GetMask("BasicWall", "Player")},
        {GAMECOLORS.Purple, LayerMask.GetMask("BasicWall", "Player")},
        {GAMECOLORS.Orange, LayerMask.GetMask("BasicWall", "Player")},
        {GAMECOLORS.Green, LayerMask.GetMask("BasicWall", "Player")},
    };
}
