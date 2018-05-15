using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class KaKu {
    public List<NormalCard> cards { get; private set; }

    public KaKu(){
        cards = new List<NormalCard>();

    }




 
    public void Add(int cardId)
    {

        NormalCard card = new NormalCard(cardId);
        Add(card);
    }
    public void Add(NormalCard card)
    {
        cards.Add(card);
    }

    public List<NormalCard> GetClassTypeCards(ClassType classType, bool includeNone = false)
    {
        return GetClassTypeCards((int) classType,includeNone);
    }
    //检索职业卡
    public List<NormalCard> GetClassTypeCards(int classType,bool includeNone = false)

    {
        if (classType == 0)
            return cards;


        List<NormalCard> result = new List<NormalCard>();
        foreach (NormalCard card in cards)
        {
            if (card.CardData.ClassLimit == classType || includeNone && card.CardData.ClassLimit == 0)
                result.Add(card);
        }


        return result;

    }
    //从卡库中剔除卡组的卡片
    public List<NormalCard> GetCardsWithDeck(Deck deck) { 


        List<NormalCard> result =GetClassTypeCards(deck.ClassType,true);
        foreach (var deckCard in deck.cards)
        {
            for (int i = 0; i < result.Count;i++)
            {
                if (deckCard.CardId == result[i].CardId)
                {
                    result.RemoveAt(i);
                    break;
                }
            }
        }


        return result;

    }



}
