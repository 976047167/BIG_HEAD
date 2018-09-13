using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using BigHead.protocol;
/// <summary>
/// 当前客户端玩家数据存放
/// </summary>
public class PlayerDetailData : PlayerData
{
    public int UsingDeck;
    protected List<NormalCard> m_EquipList = new List<NormalCard>();
    /// <summary>
    /// 
    /// </summary>
    protected List<NormalCard> m_BuffList = new List<NormalCard>();
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    protected List<NormalCard> m_CardList = new List<NormalCard>();
    /// <summary>
    /// 消耗品
    /// </summary>
    protected List<ItemData> m_ItemList = new List<ItemData>();
    protected Dictionary<int, Deck> decks = new Dictionary<int, Deck>();
    protected KaKu kaku = new KaKu();
    public List<NormalCard> EquipList { get { return m_EquipList; } }
    /// <summary>
    /// 身上自带的永久性buff
    /// </summary>
    public List<NormalCard> BuffList { get { return m_BuffList; } }
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    public List<NormalCard> CardList { get { return m_CardList; } }
    /// <summary>
    /// 消耗品
    /// </summary>
    public List<ItemData> ItemList { get { return m_ItemList; } }

    public Dictionary<int, Deck> Decks { get { return decks; } }

    public KaKu KaKu { get { return kaku; } }

    public void Update(PBPlayerDetailData playerDetailData)
    {
        //PlayerDetailData.Deck = new Deck();
        //for (int i = 0; i < characterData.DefaultCardList.Count; i++)
        //{
        //    NormalCard normalCard = new NormalCard(characterData.DefaultCardList[i], uidIndex++);
        //    CardList.Add(normalCard);
        //    PlayerDetailData.Kaku.Add(normalCard);
        //    PlayerDetailData.Deck.AddCard(normalCard);
        //}
        for (int i = 0; i < playerDetailData.Equips.Count; i++)
        {
            m_EquipList.Add(new NormalCard(playerDetailData.Equips[i], false));
        }
        for (int i = 0; i < playerDetailData.Buffs.Count; i++)
        {
            m_BuffList.Add(new NormalCard(playerDetailData.Buffs[i], false));
        }
        for (int i = 0; i < playerDetailData.Cards.Count; i++)
        {
            m_CardList.Add(new NormalCard(playerDetailData.Cards[i], false));
        }
        for (int i = 0; i < playerDetailData.Items.Count; i++)
        {
            m_ItemList.Add(new ItemData(playerDetailData.Items[i]));
        }
        UsingDeck = playerDetailData.UsingDeckIndex;
        for (int i = 0; i < playerDetailData.Decks.Count; i++)
        {
            PBDeck pbDeck = playerDetailData.Decks[i];
            Deck deck = new Deck(pbDeck.Index, pbDeck.MaxCount, pbDeck.Name);
            for (int j = 0; j < pbDeck.Cards.Count; j++)
            {
                deck.AddCard(new NormalCard(pbDeck.Cards[j], false));
            }
            decks[pbDeck.Index]= deck;
        }
        for (int i = 0; i < m_CardList.Count; i++)
        {
            kaku.Add(m_CardList[i]);
        }
    }

}

