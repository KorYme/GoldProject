using KorYmeLibrary.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] Utilities.GAMECOLORS _playerColor;
    [SerializeField] SKINPACK _skin;

    [Header("References")]
    [SerializeField] GameObject _selectedIcon;
    [SerializeField] Image _skinIcon;

    private void Start()
    {
        EnableOrNot();
        DataManager.Instance.OnSkinchange += EnableOrNot;
    }

    private void EnableOrNot()
    {
        if (!DataManager.Instance.SkinAcquiredList.Contains(_skin))
        {
            // Change Sprite for not unlocked
            _selectedIcon.SetActive(false);
            _skinIcon.color = Color.black;
        }
        else
        {
            _selectedIcon.SetActive(DataManager.Instance.SkinEquippedDictionnary[_playerColor] == _skin);
            _skinIcon.color = Color.white;
        }
    }

    public void EquipeNewSkin()
    {
        DataManager.Instance.EquipSkin(_playerColor, _skin);
    }
}
