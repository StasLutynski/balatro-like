using UnityEngine;

//public class Card
//{
//    public string suit;
//    public int rank;
//    public string displayName;

//    public Card(string suit, int rank)
//    {
//        this.suit = suit;
//        this.rank = rank;
//        displayName = ConvertToDisplayName(suit, rank);
//    }

//    public Card()
//    {
//        suit = "spades";
//        rank = 14;
//    }// по умолчанию создастся пиковый туз


//    public string ConvertToDisplayName(string suit, int rank)
//    {
//        string rankSymbol;
//        string suitSymbol;

//        switch (rank)
//        {
//            case 11: rankSymbol = "J"; break;
//            case 12: rankSymbol = "Q"; break;
//            case 13: rankSymbol = "K"; break;
//            case 14: rankSymbol = "A"; break;
//            default: rankSymbol = rank.ToString(); break;
//        }
//        switch (suit)
//        {
//            case "hearts": suitSymbol = "1"; break;
//            case "spades": suitSymbol = "♠"; break;
//            case "diamonds": suitSymbol = "♦"; break;
//            case "clubs": suitSymbol = "♣"; break;
//            default: suitSymbol = ""; break;
//        }

//        return rankSymbol + " " + suitSymbol;
//    } // берет вводные параметры, преобразует их
//    в строку, которая будет выведена на карте

//    public int GetValue()
//    {
//        if (rank <= 10) return rank;
//        else if (rank == 14) return 11;
//        else return 10; // J, Q, K = 10 очков
//    }// стоимость карт в очках




//}
//public class CardScript : MonoBehaviour
//{

//    bool isDragged;



//    private void Update()
//    {
//        while (isDragged)
//        {
//            Vector3 mousePosition = GetMouseWorldPosition();
//            transform.position = mousePosition;
//        }

//    }

//    void OnMouseDown()
//    {
//        isDragged = true;
//    }
//    void OnMouseUp()
//    {
//        isDragged = false;
//    }
//    Vector3 GetMouseWorldPosition()
//    {
//        Vector3 mouseScreenPosition = Input.mousePosition;
//        mouseScreenPosition.z = 10; // Расстояние от камеры
//        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
//    }
//}





using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CardInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;

    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder;
    }

    // Вызывается когда начинаем перетаскивание
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        // Вычисляем смещение
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f));
        offset = transform.position - worldPosition;

        // Поднимаем карту поверх других
        spriteRenderer.sortingOrder = 100;
    }

    // Вызывается при перетаскивании
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f));
            transform.position = worldPosition + offset;
        }
    }

    // Вызывается когда отпускаем
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        spriteRenderer.sortingOrder = originalSortingOrder;
    }
}
