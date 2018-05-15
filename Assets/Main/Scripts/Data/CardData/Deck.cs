using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    public List<NormalCard> cards;
    public ClassType ClassType;
    public string DeckName;
    public uint Uid;

    public Deck(uint uid)
    {
        Uid = uid;
        DeckName = "未命名";
        ClassType = ClassType.Warriop;
        cards = new List<NormalCard>();
    }
    //设置卡组名
    public void SetDeckName(string newName)
    {
        DeckName = newName;
    }
    //设置卡组职业
    public void SetClassType(ClassType newClassType)
    {
        cards.RemoveAll((card) => (
            card.CardData.ClassLimit != (int)ClassType.None &&
            card.CardData.ClassLimit != (int)newClassType)
            );

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
        cards.Add(card);
    }
    public Deck CloneSelf()
    {
        Deck result = new Deck(Uid);
        result.SetDeckName(DeckName);
        result.SetClassType(ClassType);
        result.AddCards(cards);
        return result;
    }

    public void RemoveCard(int cardId)
    {
        RemoveNormalCard( cardId);
    }

    private void RemoveNormalCard(int cardId)
    {
        foreach( var card in cards)
        {
            if (card.CardId == cardId)
            {
                cards.Remove(card);
                break;
            }
        }
    }
}
