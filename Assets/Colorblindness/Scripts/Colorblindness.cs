using NaughtyAttributes;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public enum ColorblindTypes
{
    Normal = 0,
    Protanopia,
    Protanomaly,
    Deuteranopia,
    Deuteranomaly,
    Tritanopia,
    Tritanomaly,
    Achromatopsia,
    Achromatomaly,
}
#if UNITY_EDITOR
public class Colorblindness : MonoBehaviour
{
    [SerializeField, OnValueChanged(nameof(InitChange))] ColorblindTypes type;

    // TODO: Clear saved settings

    Volume[] volumes;
    VolumeComponent lastFilter;

    int maxType;
    int _currentType = 0;
    int currentType
    {
        get => _currentType;

        set
        {
            _currentType = value;
        }
    }

    void SearchVolumes() => volumes = GameObject.FindObjectsOfType<Volume>();

    #region Enable/Disable
    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    #endregion

    public static Colorblindness Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SearchVolumes();

        if (volumes == null || volumes.Length <= 0) return;

        Change(-1);
    }

    void Start()
    {
        SearchVolumes();
        StartCoroutine(ApplyFilter());
    }

    public void Change(int filterIndex = -1)
    {
        currentType = Mathf.Clamp(filterIndex, 0, maxType);
        StartCoroutine(ApplyFilter());
    }

    void InitChange()
    {
        if (volumes == null) return;
#if UNITY_EDITOR
        // TODO: Use a public event system to announce the change of the activated filter
        Debug.Log($"Color changed to <b>{(ColorblindTypes)currentType} {currentType}</b>/{maxType}");
#endif

        StartCoroutine(ApplyFilter());
    }

    IEnumerator ApplyFilter()
    {
        ResourceRequest loadRequest = Resources.LoadAsync<VolumeProfile>($"Colorblind/{type}");

        do yield return null; while (!loadRequest.isDone);

        var filter = loadRequest.asset as VolumeProfile;

        if (filter == null)
        {
            Debug.LogError("An error has occured! Please, report");
            yield break;
        }

        if (lastFilter != null)
        {
            foreach (var volume in volumes)
            {
                volume.profile.components.Remove(lastFilter);

                foreach (var component in filter.components)
                    volume.profile.components.Add(component);
            }
        }

        lastFilter = filter.components[0];
    }
}
#endif