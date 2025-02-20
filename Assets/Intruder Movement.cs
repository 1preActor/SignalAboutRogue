using UnityEngine;

public class IntruderMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Transform _houseEntryPoint; 
    [SerializeField] private Transform _houseExitPoint;

    private bool _isMovingToHouse = true;

    private void Update()
    {        
        Transform target = _isMovingToHouse ? _houseEntryPoint : _houseExitPoint;

        transform.LookAt(target.position);
             
        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
                
        if (Vector3.Distance(transform.position, target.position) < 0.1f) {_isMovingToHouse = !_isMovingToHouse;}
    }
}