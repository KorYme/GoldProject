using KorYmeLibrary.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinContainer", menuName = "ScriptableObjects/SkinContainer", order = 1)]
public class SkinContainer : ScriptableObject
{
    public List<GameObject> ElfSkins = new List<GameObject>();
    public List<GameObject> DwarfSkins = new List<GameObject>();
    public List<GameObject> OndineSkins = new List<GameObject>();
}
