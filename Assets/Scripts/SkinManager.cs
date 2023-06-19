using KorYmeLibrary.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] SKINPACK _skinPack;

    [Header("References")]
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerReflection _playerReflection;
    [SerializeField] SkinContainer _skinContainer;

    private void Start()
    {
        List<GameObject> allSkins;
        switch (_playerReflection.ReflectionColor)
        {
            case Utilities.GAMECOLORS.Red:
                allSkins = _skinContainer.DwarfSkins;
                break;
            case Utilities.GAMECOLORS.Blue:
                allSkins = _skinContainer.OndineSkins;
                break;
            case Utilities.GAMECOLORS.Yellow:
                allSkins = _skinContainer.ElfSkins;
                break;
            default:
                return;
        }
        if (!DataManager.Instance.SkinEquippedDictionnary.ContainsKey(_playerReflection.ReflectionColor)) return;
        if (DataManager.Instance.SkinEquippedDictionnary[_playerReflection.ReflectionColor] == _skinPack) return;
        if (allSkins.Count <= (int)DataManager.Instance.SkinEquippedDictionnary[_playerReflection.ReflectionColor]) return;
        GameObject newPlayer = Instantiate(allSkins[(int)DataManager.Instance.SkinEquippedDictionnary[_playerReflection.ReflectionColor]], 
            transform.position, Quaternion.identity, transform.parent);
        newPlayer.transform.localScale = transform.localScale;
        newPlayer.GetComponent<PlayerReflection>().ReflectionTypeValue = _playerReflection.ReflectionTypeValue;
        PlayerController pc = newPlayer.GetComponent<PlayerController>();
        pc.EightLaserDirections = _playerController.EightLaserDirections;
        pc.IsRotationClockWise = _playerController.IsRotationClockWise;
        pc.InitialDirection = _playerController.InitialDirection;
        Destroy(gameObject);
    }
}
