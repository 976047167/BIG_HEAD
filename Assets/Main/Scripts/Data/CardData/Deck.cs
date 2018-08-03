using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private int index = 0;
    private int max_count = 0;
    string name = "";
    private List<NormalCard> Cards;

    public Deck(int index, int max_count, string name)
    {
        this.index = index;
        this.max_count = max_count;
        this.name = name;
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
