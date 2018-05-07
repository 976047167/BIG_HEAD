using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCharacterData : CharacterData {
    public List<CardBase> Kaku;
    public List<Profession> professions;//职业
    public object LevelProgeress;//关卡进度，目前没想好用什么类型

}

public class Profession
{
    public int ProfessionIndex;
    public string ProfessionName { get; private set; }
    public string ProfessionIconPath { get; private set; }
    public int Exp;
    public int Level;
    public int HpMax;
    public int MpMax;
    public int ApMax;
 
    public List<Deck> Decks;


}
public class Deck
{
    public string DeckName;
    public List<CardBase> cards;
}