using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    [Header("Components")]
    [SerializeField] private FixedJoystick _fixedJoystick;
    private Rigidbody rigidbodyPlayer => GetComponent<Rigidbody>();
    private Animator animator => GetComponent<Animator>();

    private void Update()
    {
        Movment();
    }

    private void Movment()
    {
        Vector3 directionMovment = new Vector3(_fixedJoystick.Horizontal, 0.0f, _fixedJoystick.Vertical);
        rigidbodyPlayer.velocity = directionMovment * 10.0f;

        if (Mathf.Abs(rigidbodyPlayer.velocity.magnitude) > 0.01f)
        {
            Rotation(directionMovment);
        }

        SetSpeedAnimator(rigidbodyPlayer.velocity.magnitude);
    }

    private void Rotation(Vector3 direction)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _speedRotation);
    }

    private void SetSpeedAnimator(float value)
    {
        animator.SetFloat("Speed", value);
    }
}
