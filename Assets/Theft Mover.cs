using UnityEngine;

public class TheftMover : MonoBehaviour 
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private Transform _exitPoint;

    private float _variable = 0.1f;
    private bool _isMovingToHouse = true;

    private void Update()
    {
        Transform target = _isMovingToHouse ? _entryPoint : _exitPoint;

        transform.LookAt(target.position);

        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);

        if ((transform.position - target.position).sqrMagnitude < _variable * _variable)
        {
            _isMovingToHouse = !_isMovingToHouse;
        }
    }
}
