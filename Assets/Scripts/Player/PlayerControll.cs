using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControll : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _interactionDistance = 1.0f;

    [Header("Components")]
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private TMP_Text _textMeshPro;
    private Rigidbody rigidbodyPlayer => GetComponent<Rigidbody>();
    private Animator animator => GetComponent<Animator>();

    private void Start()
    {
        StartCoroutine(Event());
        StartCoroutine(PickingDeck());
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

    private IEnumerator PickingDeck()
    {
        int deckCount = 0;
        while (true)
        {
            GameObject gameObjectDeck = null;
            yield return new WaitUntil(() => CollidesWithTheDeck(out gameObjectDeck));

            animator.SetBool("IsDeck", true);

            if (gameObjectDeck != null)
            {
                Adjustment(gameObjectDeck);
            }

            yield return new WaitUntil(() => IsTouchingTrigger());
            Destroy(gameObjectDeck);

            animator.SetBool("IsDeck", false);

            deckCount += 1;
            _textMeshPro.text = deckCount.ToString();
        }
    }

    private bool CollidesWithTheDeck(out GameObject gameObject)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Deck"))
            {
                gameObject = collider.gameObject;
                return true;
            }
        }

        gameObject = null;
        return false;
    }

    private bool IsTouchingTrigger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Trigger"))
            {
                return true;
            }
        }

        return false;
    }

    private void Adjustment(GameObject gameObjectDeck)
    {
        gameObjectDeck.GetComponent<Rigidbody>().isKinematic = true;
        gameObjectDeck.transform.SetParent(transform, true);

        gameObjectDeck.transform.localPosition = new Vector3(-0.043f, 2.735f, 0.286f);
        gameObjectDeck.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
    }
}
