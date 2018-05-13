using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class KaKu {
    public List<NormalCard> cards { get; private set; }

    public KaKu(){
        cards = new List<NormalCard>();

    }


    private void AddNormalCard(NormalCard card)
    {
        cards.Add(card);
    }

    public void Add(NormalCard card)
    {
        AddNormalCard(card);
    }
    public void Add(int cardId)
    {

        NormalCard card = new NormalCard(cardId);
        AddNormalCard(card);
    }


    public List<NormalCard> getClassTypeCards(ClassType classType, bool includeNone)
    {
        return getClassTypeCards((int) classType,includeNone);
    }
    //检索职业卡
    public List<NormalCard> getClassTypeCards(int classType,bool includeNone)

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
}
