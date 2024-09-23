using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardCollectionSO cardCollection;
    private List<CardController> cardControllers;
    private CardGridGenerator cardGridGenerator;

    private void Awake()
    {
        cardControllers = new List<CardController>();
        cardGridGenerator = new CardGridGenerator(cardCollection);//, gameDatas);
        SetCardConfig();
        GenerateCards();
        SetGameManagerCardCount();
    }

    private void SetCardConfig()
    {
        CardGridLayout cardGridLayout = GetComponent<CardGridLayout>();
        cardGridLayout.rows = MenuController.instance.GridHeight;
        cardGridLayout.columns = MenuController.instance.GridWidth;
        
        cardGridLayout.Invoke("CalculateLayoutInputHorizontal", 0.2f);
    }

   
    private void GenerateCards()
    {
        int cardCount = MenuController.instance.GridHeight * MenuController.instance.GridWidth;
        for (int i = 0; i < cardCount; i++)
        {
            GameObject card = Instantiate(cardPrefab, transform);
            card.name = "Card (" + i.ToString() + ")";
            cardControllers.Add(card.GetComponent<CardController>());
        }

        int halfCardCount = cardCount / 2;
        for (int i = 0; i < halfCardCount; i++)
        {
            CardScriptableObject randomCard = cardGridGenerator.GetRandomAvailableCardSO();
            SetRandomCardToGrid(randomCard);
            CardScriptableObject randomCardPair = cardGridGenerator.GetCardPairSO(randomCard.cardName);
            SetRandomCardToGrid(randomCardPair);
        }
    }

    private void SetRandomCardToGrid(CardScriptableObject randomCard)
    {
        int index = cardGridGenerator.GetRandomCardPositionIndex();
        CardController cardObject = cardControllers[index];
        cardObject.SetCardDatas( randomCard);
    }

    private void SetGameManagerCardCount()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.CardCount = MenuController.instance.GridHeight * MenuController.instance.GridWidth;
        }
    }
}
