using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{

    private List<NormalCard> Cards ;

    public Deck()
    {
        Cards = new List<NormalCard>();
    }


    public List<NormalCard> GetCards(int cardId)
    {
        List<NormalCard> result = new List<NormalCard>();
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i].CardId == cardId)
                result.Add(Cards[i]);
        }
        return result;
    }
    
    public void AddCard(NormalCard card)
    {
        Cards.Add(card);
    }


    public void AddCards(List<NormalCard> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            AddCard(cards[i]);
        }
    }

    public NormalCard RemoveCard(NormalCard card)
    {
        if (card == null)
            return null;
        if (Cards.Remove(card) == false)
            return null;
        return card;
    }
    public int GetCardCounnt()
    {
        return Cards.Count;
    }
    public NormalCard this[int index]
    {
        get
        {
            return Cards[index];

        }
    }
    public int Count
    {
        get { return Cards.Count; }
    }
    public void Clear()
    {
        Cards.Clear();
    }
}
