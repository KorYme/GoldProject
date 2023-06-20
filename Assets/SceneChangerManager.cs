using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerManager : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public static SceneChangerManager Instance;

    int _sceneIndex;
    string _sceneName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        _sceneIndex = -1;
        _sceneName = "";
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _sceneIndex = -1;
        _sceneName = "";
        _animator.SetTrigger("OpenScene");
    }

    public void ChangeScene(int sceneIndex)
    {
        if (_sceneName != "" || _sceneIndex != -1) return;
        _sceneIndex = sceneIndex;
        _animator.SetTrigger("CloseScene");
    }

    public void ChangeScene(string sceneName)
    {
        if (_sceneName != "" || _sceneIndex != -1) return;
        _sceneName = sceneName;
        _animator.SetTrigger("CloseScene");
    }

    public void SceneClosed()
    {
        if (_sceneIndex == -1)
        {
            SceneManager.LoadScene(_sceneName);
        }
        else
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
