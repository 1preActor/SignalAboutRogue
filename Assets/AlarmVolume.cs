using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AlarmVolume : MonoBehaviour
{
    [SerializeField] private float _fadeSpeed = 1f;
    [SerializeField] private float _permissibleError = 0.01f;

    private AudioSource _alarmSound;
    private float _targetVolume;
    private Coroutine _volumeChangeCoroutine;
    private float _startVolume;

    private void Awake()
    {
        _alarmSound = GetComponent<AudioSource>();
        _alarmSound.volume = 0f;
    }

    private void Start()
    {
        IntruderDetector detector = GetComponent<IntruderDetector>();

        if (detector != null)
        {
            detector.OnIntruderEntered += OnIntruderEntered;
            detector.OnIntruderExited += OnIntruderExited;
        }
    }

    private void OnIntruderEntered()
    {
        _targetVolume = 1f;

        if (_volumeChangeCoroutine == null)
        {
            _startVolume = _alarmSound.volume;
            _volumeChangeCoroutine = StartCoroutine(ChangeVolume());
        }
    }

    private void OnIntruderExited()
    {
        _targetVolume = 0f;

        if (_volumeChangeCoroutine == null)
        {
            _startVolume = _alarmSound.volume;
            _volumeChangeCoroutine = StartCoroutine(ChangeVolume());
        }
    }

    private IEnumerator ChangeVolume()
    {
        float volume = Mathf.Abs(_alarmSound.volume - _targetVolume);

        while (volume > _permissibleError)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _targetVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        _alarmSound.volume = _targetVolume;

        _volumeChangeCoroutine = null;
    }

    private void OnDestroy()
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }
    }
}