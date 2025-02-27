using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IntruderDetector : MonoBehaviour
{
    public System.Action OnIntruderEntered;
    public System.Action OnIntruderExited;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent(out TheftMover intruder))
        {
            OnIntruderEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TheftMover intruder))
        {
            OnIntruderExited?.Invoke();
        }
    }
}