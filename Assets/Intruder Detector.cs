using UnityEngine;
using UnityEngine.Events;

public class IntruderDetector : MonoBehaviour
{
    public UnityEvent<TheftMover> OnIntruderEntered;
    public UnityEvent<TheftMover> OnIntruderExited;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent(out TheftMover intruder))
        {
            OnIntruderEntered.Invoke(intruder);
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.TryGetComponent(out TheftMover intruder))
        {
            OnIntruderExited.Invoke(intruder);
        }
    }
}