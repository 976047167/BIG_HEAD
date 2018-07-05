using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class KaKu {
    private List<NormalCard> Cards;

    public KaKu() {
        Cards = new List<NormalCard>();

    }
    /*
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
    public List<NormalCard> GetCards()
    {
        List<NormalCard> result = new List<NormalCard>();
        for (int i = 0; i < Cards.Count; i++)
        {
            result.Add(Cards[i]);
        }
        return result;
    }
    */
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
        for (int i = 0; i < cards.Count; i++)
        {
            Cards.Add(cards[i]);
        }

    }
    /*
 public List<NormalCard> GetClassTypeCards(ClassType classType, bool includeNone = false)
 {
     return GetClassTypeCards((int)classType, includeNone);
 }
 //检索职业卡
 public List<NormalCard> GetClassTypeCards(int classType, bool includeNone = false)

 {
     if (classType == 0)
         return GetCards();
     //  return Cards.FindAll((card) => (card.Data.ClassLimit == classType || includeNone && card.Data.ClassLimit == 0));
     List<NormalCard> result = new List<NormalCard>();
     for (int i = 0; i < Cards.Count; i++)
     {
         if (Cards[i].Data.ClassLimit == classType || includeNone && Cards[i].Data.ClassLimit == 0)
             result.Add(Cards[i]);
     }
     return result;

 }
    */
    public int GetCardCounnt()
    {
        return Cards.Count;
    }
    public NormalCard this[int index]
    {
        get {
            return Cards[index];
        
        }
   }
    public int Count
    {
        get { return Cards.Count; }
    }

}
