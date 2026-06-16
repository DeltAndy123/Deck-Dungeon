using TMPro;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI effectText;

    public void Populate(CardData card)
    {
        nameText.text = card.cardName;
        effectText.text = card.description;
    }
}