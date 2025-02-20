using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound; 
    [SerializeField] private float _fadeSpeed = 1f;

    private float _targetVolume = 0f;
    private bool _isIntruderInside = false;

    private void Start()
    {
        _alarmSound.volume = 0f;
    }

    private void Update()
    {
        _alarmSound.volume = Mathf.Lerp(_alarmSound.volume, _targetVolume, _fadeSpeed * Time.deltaTime);
        Debug.Log("Текущая громкость: " + _alarmSound.volume + ", Целевая громкость: " + _targetVolume);
                
        if (!_isIntruderInside && _alarmSound.volume < 0.01f)
        {
            _alarmSound.volume = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Intruder"))
        {
            _isIntruderInside = true;
            _targetVolume = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.CompareTag("Intruder"))
        {
            _isIntruderInside = false;
            _targetVolume = 0f;
        }
    }
}