using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControl : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _prefabsDeck;

    private GameObject player => GameObject.FindWithTag("Player");

    public void DestructionOfTheTree(GameObject gameObjectTree)
    {
        StartCoroutine(Destruction(gameObjectTree));
    }

    private IEnumerator Destruction(GameObject gameObjectTree)
    {
        gameObjectTree.SetActive(false);

        yield return new WaitForSeconds(15.0f);

        gameObjectTree.SetActive(true);
        gameObjectTree.GetComponent<DamageTree>().Hits = 2;
    }

    public void DeckControl(GameObject gameObjectTree)
    {
        Vector3 treePosition = gameObjectTree.transform.position;
        treePosition.y += 1.5f;

        GameObject deck = Instantiate(_prefabsDeck, treePosition, Quaternion.identity);
        // StartCoroutine(PickingDeck(deck));

        Vector3 deckPosition = deck.transform.position;
        deckPosition.y += 1f;

        deck.GetComponent<Rigidbody>().AddForceAtPosition(Random.onUnitSphere * 5.0f, deckPosition, ForceMode.Impulse);
    }

    // private IEnumerator PickingDeck(GameObject deck)
    // {
    //     float startTime = Time.time;
    //     float timeOfLastUpdate = Time.time;

    //     while ((timeOfLastUpdate - startTime) < 30.0f)
    //     {
    //         Collider[] collider = Physics.OverlapSphere(deck.transform.position, 0.1f);

    //         foreach (Collider collider1 in collider)
    //         {
    //             if (collider1.gameObject.CompareTag("Player"))
    //             {
    //                 Destroy(deck);
    //                 yield break;
    //             }
    //         }

    //         timeOfLastUpdate = Time.time;
    //         yield return null;
    //     }

    //     Destroy(deck);
    // }
}
