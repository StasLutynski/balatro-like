using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public GameObject cardPrefab;
    private List<GameObject> deck = new List<GameObject>();

    private string[] suits = { "Hearts", "Spades", "Clubs", "Diamonds" };
    private int[] ranks = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

    void Start()
    {
        CreateDeck();
        Debug.Log("да");
    }

    void CreateDeck()
    {

        float xPos = 0;
        float yPos = 0;

        foreach (string suit in suits)
        {
            foreach (int rank in ranks)
            {
                Vector3 cardPos = new Vector2(xPos, yPos);
                GameObject cardObject = Instantiate(cardPrefab);
                TextMeshPro CardText = cardObject.GetComponentInChildren<TextMeshPro>();
                MeshRenderer textRenderer = CardText.GetComponent<MeshRenderer>();
                Renderer spriteRenderer = cardObject.GetComponent<SpriteRenderer>();

                textRenderer.sortingLayerName = "text";
                textRenderer.sortingOrder = deck.Count;
                spriteRenderer.sortingLayerName = "Cards";
                spriteRenderer.sortingOrder = deck.Count;


                // Настраиваем текст
                TextMeshPro textComponent = cardObject.GetComponentInChildren<TextMeshPro>();
                if (textComponent != null)
                {
                    string displayText = ConvertToDisplayText(suit, rank);
                    textComponent.text = displayText;

                    if (suit == "Hearts" || suit == "Diamonds")
                        textComponent.color = Color.red;
                    else
                        textComponent.color = Color.black;
                }

                deck.Add(cardObject);
                xPos += 120f;
            }
            xPos = 0;
            yPos -= 150f;
        }
    }

    string ConvertToDisplayText(string suit, int rank)
    {
        string rankSymbol = rank switch
        {
            11 => "J",
            12 => "Q",
            13 => "K",
            14 => "A",
            _ => rank.ToString()
        };

        string suitSymbol = suit switch
        {
            "Hearts" => "♥",
            "Diamonds" => "♦",
            "Clubs" => "♣",
            "Spades" => "♠",
            _ => "?"
        };

        return $"{rankSymbol}{suitSymbol}";
    }
}