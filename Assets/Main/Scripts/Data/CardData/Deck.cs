using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    public List<NormalCard> Cards { get; private set; }

    public Deck()
    {
        Cards = new List<NormalCard>();
    }
    /// <summary>
    /// 返回cards的dictonar形式，
    /// </summary>
    /// <param name="normalCards"></param>
    /// <returns></returns>
    public Dictionary<int, List<NormalCard>> GetDicCards()
    {
        return GetDicCards(Cards);
    }

    static public Dictionary<int, List<NormalCard>> GetDicCards(List<NormalCard> normalCards)
    {
        Dictionary<int, List<NormalCard>> cardsDic = new Dictionary<int, List<NormalCard>>();
        for (int i = 0; i < normalCards.Count; i++)
        {
            if (cardsDic.ContainsKey(normalCards[i].CardId) == false)
            {
                List<NormalCard> tempCards = GetOneCardList(normalCards[i].CardId, normalCards);
                cardsDic.Add(normalCards[i].CardId, tempCards);
            }
        }
        return cardsDic;
    }

    /// <summary>
    /// 返回某一cardid下的所有卡
    /// </summary>
    /// <param name="cardId"></param>
    /// <returns></returns>
     public List<NormalCard> GetOneCardList(int cardId){
        return GetOneCardList(cardId, Cards);
        }



    static public List<NormalCard> GetOneCardList(int cardId,List<NormalCard>cardlist)
    {
        List<NormalCard> result = new List<NormalCard>();
        for (int i = 0;i< cardlist.Count; i++)
        {
            if(cardlist[i].CardId == cardId)
            {
                result.Add(cardlist[i]);
            } 
        }


        return result;

    }
    public void AddCard(NormalCard card)
    {
        AddNormalCard(card);
    }
    public void AddCard(int cardId)
    {
           NormalCard card = new NormalCard(cardId);
        AddNormalCard(card);
    }
    public void AddCards (List<int> cardIds)
    {
        for (int i = 0; i < cardIds.Count; i++)
        {
            AddCard(cardIds[i]);
        }
    }
    public void AddCards(List<NormalCard> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            AddCard(cards[i]);
        }
    }
    private void AddNormalCard(NormalCard card)
    {
        Cards.Add(card);
    }

    public NormalCard RemoveCard(int cardId)
    {
        NormalCard card = Cards.Find(item => item.CardId == cardId);
        return  RemoveCard(card);
    }
    public NormalCard RemoveCard(NormalCard card)
    {
        if (card == null)
            return null;
        if(Cards.Remove(card) == false)
             return null;
        return card;
    }

    public Deck CloneSelf()
    {
        Deck clone = new Deck();
        clone.AddCards(Cards);
        return clone;
    }
}
