using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AlarmVolume : MonoBehaviour
{
    [SerializeField] private float _fadeSpeed = 1f;
    [SerializeField] private float _volumeTolerance = 0.01f;

    private AudioSource _alarmSound;
    private IntruderDetector _detector;
    private Coroutine _volumeChangeCoroutine;

    private void Awake()
    {    
        _alarmSound = GetComponent<AudioSource>();
        _detector = GetComponent<IntruderDetector>();

        _alarmSound.volume = 0f;
    }

    private void OnEnable()
    {
        if (_detector != null)
        {
            _detector.OnIntruderEntered += OnIntruderEntered;
            _detector.OnIntruderExited += OnIntruderExited;
        }
    }

    private void OnDisable()
    {
        if (_detector != null)
        {
            _detector.OnIntruderEntered -= OnIntruderEntered;
            _detector.OnIntruderExited -= OnIntruderExited;
        }

        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
            _volumeChangeCoroutine = null;
        }
    }

    private void OnIntruderEntered()
    {
        StartVolumeChange(1f);
    }

    private void OnIntruderExited()
    {
        StartVolumeChange(0f);
    }

    private void StartVolumeChange(float targetVolume)
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }

        _volumeChangeCoroutine = StartCoroutine(ChangeVolume(targetVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        float startVolume = _alarmSound.volume;

        while (Mathf.Abs(_alarmSound.volume - targetVolume) > _volumeTolerance)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        _alarmSound.volume = targetVolume;

        _volumeChangeCoroutine = null;
    }
}