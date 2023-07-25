using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _interactionDistance = 1.0f;

    [Header("Components")]
    [SerializeField] private FixedJoystick _fixedJoystick;
    private Rigidbody rigidbodyPlayer => GetComponent<Rigidbody>();
    private Animator animator => GetComponent<Animator>();

    private void Start()
    {
        StartCoroutine(Event());
    }

    private IEnumerator Event()
    {
        while (true)
        {
            yield return new WaitUntil(() => FindATree());
            animator.SetTrigger("Shock");

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void ShockToTree()
    {
        GameObject treeGameObject;

        if (FindATree(out treeGameObject))
        {
            treeGameObject.GetComponent<DamageTree>().Damage(1);
        }
    }

    private bool FindATree(out GameObject gameObject)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            if (hit.transform.gameObject.CompareTag("Tree"))
            {
                gameObject = hit.transform.gameObject;
                return true;
            }
        }

        gameObject = null;
        return false;
    }

    private bool FindATree()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            if (hit.transform.gameObject.CompareTag("Tree"))
            {
                return true;
            }
        }

        return false;
    }


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
