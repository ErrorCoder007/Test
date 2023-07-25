using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControl : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _interactionDistance = 1.0f;

    private Animator animator => GetComponent<Animator>();

    private void Start()
    {
        // StartCoroutine("CollisionToTree");
    }

    private IEnumerator FindATree()
    {
        while (true)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _interactionDistance))
            {
                if (hit.transform.gameObject.CompareTag("Tree"))
                {
                    GameObject gameObjectTree = hit.transform.gameObject;

                    IDamageTree iDamageTree = gameObjectTree.GetComponent<DamageTree>();
                    iDamageTree.Damage(1);

                    yield return new WaitForSeconds(0.8f);

                    if (iDamageTree.Hits == 0)
                    {
                        StartCoroutine(Events(gameObjectTree));
                    }
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Events(GameObject gameObjectTree)
    {
        IDamageTree iDamageTree = gameObjectTree.GetComponent<DamageTree>();

        gameObjectTree.SetActive(false);

        yield return new WaitForSeconds(15.0f);

        gameObjectTree.SetActive(true);
        iDamageTree.Hits = 2;
    }
}
