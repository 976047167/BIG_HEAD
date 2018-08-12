using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.protocol;

public class Deck
{
    private int index = 0;
    private int max_count = 0;
    string name = "";
    private List<NormalCard> m_Cards;
    public List<NormalCard> Cards { get { return m_Cards; } }

    public Deck(int index, int max_count, string name)
    {
        this.index = index;
        this.max_count = max_count;
        this.name = name;
        m_Cards = new List<NormalCard>();
    }
    public Deck(PBDeck pBDeck) : this(pBDeck.Index, pBDeck.MaxCount, pBDeck.Name)
    {
        for (int i = 0; i < pBDeck.Cards.Count; i++)
        {
            m_Cards.Add(new NormalCard(pBDeck.Cards[i], false));
        }
    }

    public List<NormalCard> GetCards(int cardId)
    {
        List<NormalCard> result = new List<NormalCard>();
        for (int i = 0; i < m_Cards.Count; i++)
        {
            if (m_Cards[i].CardId == cardId)
                result.Add(m_Cards[i]);
        }
        return result;
    }

    public void AddCard(NormalCard card)
    {
        m_Cards.Add(card);
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
        if (m_Cards.Remove(card) == false)
            return null;
        return card;
    }
    public int GetCardCounnt()
    {
        return m_Cards.Count;
    }
    public NormalCard this[int index]
    {
        get
        {
            return m_Cards[index];

        }
    }
    public int Count
    {
        get { return m_Cards.Count; }
    }
    public void Clear()
    {
        m_Cards.Clear();
    }
}
