using KorYmeLibrary.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KorYmeLibrary;

public class SkinManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] SKINPACK _skinPack;

    [Header("References")]
    [SerializeField] PlayerReflection _playerReflection;
    [SerializeField] List<GameObject> _allSkins;

    private void Start()
    {
        Debug.Log("ChangeSkin");
        if (DataManager.Instance.SkinEquippedDictionnary[(int)_playerReflection.ReflectionColor] == (int)_skinPack) return;
        if (_allSkins.Count <= DataManager.Instance.SkinEquippedDictionnary[(int)_playerReflection.ReflectionColor]) return;
        Instantiate(_allSkins[DataManager.Instance.SkinEquippedDictionnary[(int)_playerReflection.ReflectionColor]], transform.position, Quaternion.identity, transform.parent);
        // I'm disabling it before before destroy because it is way less expensive in term of calculus
        enabled = false;
        // But still destroy it because we never know
        Destroy(gameObject);
    }
}
