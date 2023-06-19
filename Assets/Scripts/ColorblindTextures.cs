using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorblindTextures", menuName = "ScriptableObjects/ColorblindTextures", order = 2)]
public class ColorblindTextures : ScriptableObject
{
    public List<Texture2D> Textures;
}
