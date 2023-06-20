using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] ParticleSystem _particlePrefab;
    [SerializeField] ParticleSystem _particlePrefabRotate;

    ParticleSystem _firstParticle;
    ParticleSystem _secondParticle;
    ParticleSystem _thirdParticle;
    [SerializeField] GameObject _player;

    List<ParticleSystem> _list = new List<ParticleSystem>();


    int _currentParticle = 0;

    private void Start()
    {
        _list.Add(_firstParticle);
        _list.Add(_secondParticle);
        _list.Add(_thirdParticle);
        for (int i = 0; i < _list.Count; i++)
        {
            if (i != 2)
                _list[i] = Instantiate(_particlePrefab);
            else
                _list[i] = Instantiate(_particlePrefabRotate);
            
            _list[i].Stop();
        }
        _list[0].transform.position = _player.transform.position;
        _list[0].Play();
    }

    public void OnPlayerMoveTutorial(Vector2 dir)
    {
        switch (_currentParticle)
        {
            case 0:
                if (dir == Vector2.left)
                {
                    _list[_currentParticle].Stop();
                    _currentParticle++;
                    StartCoroutine(ParticlePlayCoroutine());
                }
                break;
            case 1:
                if (dir == Vector2.down)
                {
                    _list[_currentParticle].Stop();
                    _currentParticle++;
                    StartCoroutine(ParticlePlayCoroutine());
                }
                break;
            default:
                break;
        }
    }

    public void OnPlayerRotateTutorial()
    {
        if (_currentParticle == 2)
        {
            foreach (var item in _list)
            {
                Destroy(item);
            }
            _list.Clear();
            Destroy(this);
        }
    }

    private IEnumerator ParticlePlayCoroutine()
    {
        yield return new WaitForSeconds(1);
        _list[_currentParticle].transform.position = _player.transform.position;
        if (_currentParticle != 2)
        {
            _list[_currentParticle].transform.Rotate(0, -90, 0);
        }
        _list[_currentParticle]?.Play();
    }
}
