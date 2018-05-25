using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    public List<NormalCard> Cards;
    public ClassType ClassType;
    public string DeckName;
    public uint Uid;

    public Deck(uint uid)
    {
        Uid = uid;
        DeckName = "未命名";
        ClassType = ClassType.Warriop;
        Cards = new List<NormalCard>();
    }
    //设置卡组名
    public void SetDeckName(string newName)
    {
        DeckName = newName;
    }
    //设置卡组职业
    public void SetClassType(ClassType newClassType)
    {
        Cards.RemoveAll((card) => (
            card.CardData.ClassLimit != (int)ClassType.None &&
            card.CardData.ClassLimit != (int)newClassType)
            );

    }
    public Dictionary<int, List<NormalCard>> GetDicCards()
    {
        return GetDicCards(Cards);
    }

    static public Dictionary<int, List<NormalCard>> GetDicCards(List<NormalCard> normalCards)
    {
        Dictionary<int, List<NormalCard>> cardsDic = new Dictionary<int, List<NormalCard>>();
        foreach (var card in normalCards)
        {
            if (cardsDic[card.CardId] == null)
            {
                List<NormalCard> tempCards = normalCards.FindAll((tempCard) => (tempCard.CardId == card.CardId));
                cardsDic.Add(card.CardId, tempCards);
            }
        }
        return cardsDic;
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
        foreach (int i in cardIds)
        {
            AddCard(i);
        }
    }
    public void AddCards(List<NormalCard> cards)
    {
        foreach (NormalCard i in cards)
        {
            AddCard(i);
        }
    }
    private void AddNormalCard(NormalCard card)
    {
        if (card.CardData.ClassLimit != (int)ClassType && card.CardData.ClassLimit !=  (int) ClassType.None)
        {
            Debug.LogError("加入卡组的卡职业不正确");
            return;
        }
        Cards.Add(card);
    }
    public Deck CloneSelf()
    {
        Deck result = new Deck(Uid);
        result.SetDeckName(DeckName);
        result.SetClassType(ClassType);
        result.AddCards(Cards);
        return result;
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

}
