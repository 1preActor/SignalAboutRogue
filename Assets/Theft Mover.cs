using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheftMover : MonoBehaviour 
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private Transform _exitPoint;

    private bool _isMovingToHouse = true;

    private void Update()
    {
        Transform target = _isMovingToHouse ? _entryPoint : _exitPoint;

        transform.LookAt(target.position);

        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            _isMovingToHouse = !_isMovingToHouse;
        }
    }
}
