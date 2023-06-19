using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] ParticleSystem _particlePrefab;

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
            _list[i] = Instantiate(_particlePrefab);
            _list[i].Stop();
        }
        _firstParticle.transform.position = new Vector2(1, 1);
        _firstParticle.Play();
    }

    public void OnPlayerAction()
    {
        if (_list[_currentParticle].isPlaying)
        {
            _list[_currentParticle].Stop();
            _currentParticle++;
            if (_list[_currentParticle] != null)
                StartCoroutine(ParticlePlayCoroutine());
        }
    }

    private IEnumerator ParticlePlayCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _list[_currentParticle].transform.position = _player.transform.position;
        _list[_currentParticle].Play();
    }
}
