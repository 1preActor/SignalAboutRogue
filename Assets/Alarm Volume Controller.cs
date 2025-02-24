using UnityEngine;
using System.Collections;

public class AlarmVolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _fadeSpeed = 1f;

    private float _targetVolume = 0f;
    private Coroutine _volumeChangeCoroutine;

    private void Start()
    {
        _alarmSound.volume = 0f;
    }

    public void OnIntruderEntered(TheftMover intruder)
    {
        _targetVolume = 1f;
                
        if (_volumeChangeCoroutine == null)
        {
            _volumeChangeCoroutine = StartCoroutine(ChangeVolume());
        }
    }

    public void OnIntruderExited(TheftMover intruder)
    {
        _targetVolume = 0f;
                
        if (_volumeChangeCoroutine == null)
        {
            _volumeChangeCoroutine = StartCoroutine(ChangeVolume());
        }
    }

    private IEnumerator ChangeVolume()
    {        
        while (Mathf.Abs(_alarmSound.volume - _targetVolume) > 0.01f)
        {            
            _alarmSound.volume = Mathf.Lerp(_alarmSound.volume, _targetVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        _alarmSound.volume = _targetVolume;

        _volumeChangeCoroutine = null;
    }
}