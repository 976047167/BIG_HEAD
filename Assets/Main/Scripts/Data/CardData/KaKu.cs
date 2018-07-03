using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class KaKu {
    public List<NormalCard> Cards { get; private set; }
 
    public KaKu(){
        Cards = new List<NormalCard>();

    }

    public Dictionary<int, List<NormalCard>> GetDicCards()
    {
        return GetDicCards(Cards);
    }

    static public Dictionary<int, List<NormalCard>> GetDicCards(List<NormalCard> normalCards)
    {
        Dictionary<int, List<NormalCard>> cardsDic = new Dictionary<int, List<NormalCard>>();

        for (int i = 0; i < normalCards.Count; i++)
        {
            NormalCard card = normalCards[i];
            if (cardsDic.ContainsKey(card.CardId) == false)
            {
                List<NormalCard> tempCards = new List<NormalCard>();
                for (int j = 0; j < normalCards.Count; j++)
                {
                    if (normalCards[j].CardId == card.CardId)
                    {
                        tempCards.Add(normalCards[j]);
                    }
                }
                cardsDic.Add(card.CardId, tempCards);
            }
        }

        return cardsDic;
    }


    public void Add(int cardId)
    {

        NormalCard card = new NormalCard(cardId);
        Add(card);
    }
    public void Add(NormalCard card)
    {
        Cards.Add(card);
    }
    public void Add(List<NormalCard> cards)
    {
        for (int i =0; i < cards.Count; i++)
        {
            Cards.Add(cards[i]);
        }
        
    }
    public List<NormalCard> GetClassTypeCards(ClassType classType, bool includeNone = false)
    {
        return GetClassTypeCards((int) classType,includeNone);
    }
    //检索职业卡
    public List<NormalCard> GetClassTypeCards(int classType,bool includeNone = false)

    {
        if (classType == 0)
            return Cards;
        return Cards.FindAll((card) => (card.Data.ClassLimit == classType || includeNone && card.Data.ClassLimit == 0));


    }
    //从卡库中剔除卡组的卡片
    public List<NormalCard> GetCardsWithDeck(Deck deck) {
        List<NormalCard> result = new List<NormalCard>();

        for (int i = 0;i< Cards.Count; i++)
        {
            for (int j = 0; j < deck.Cards.Count; j++)
            {
                if (Cards[i] == deck.Cards[j])
                    break;
                if (j == deck.Cards.Count - 1)
                    result.Add(Cards[i]);
            }
        }
        return result;

    }



}
