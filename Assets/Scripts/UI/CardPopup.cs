using System.Collections;
using UnityEngine;

public class CardPopup : MonoBehaviour
{
    [SerializeField] private CardDisplay cardPrefab;
    [SerializeField] private Transform displayParent;
    [SerializeField] private float displayDuration = 3f;

    private void Start()
    {
        FindAnyObjectByType<DeckManager>().OnCardDrawn += ShowCard;
    }

    private void ShowCard(CardData card)
    {
        StopAllCoroutines();
        foreach (Transform child in displayParent)
            Destroy(child.gameObject);

        var display = Instantiate(cardPrefab, displayParent);
        display.Populate(card);
        StartCoroutine(HideAfter(displayDuration));
    }

    private IEnumerator HideAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        foreach (Transform child in displayParent)
            Destroy(child.gameObject);
    }
}
