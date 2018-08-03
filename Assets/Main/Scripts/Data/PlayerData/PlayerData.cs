using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.protocol;
using AppSettings;

public class PlayerData
{
    public string Name;
    public int Level;
    public int Exp = 0;
    public int MaxExp = 1;
    /// <summary>
    /// 血量
    /// </summary>
    public int HP;
    public int MaxHP;
    public int MP;
    public int MaxMP;
    public int Food;
    public int MaxFood;
    public int Gold;
    public int Diamond;
    public int HeadIcon;
    public int MapSkillID;
    public int BattleSkillID;
    
    public int UsingCharacter;
    public ClassData ClassData;


    

    public void Update(PBPlayerData playerData)
    {
        Level = playerData.Level;

        ClassData = new ClassData(playerData.CharacterId);
        LevelTableSetting levelData = LevelTableSettings.Get(Level);
        if (levelData == null)
        {
            return;
        }
        Name = playerData.Name;
        HP = playerData.Hp;
        MaxHP = playerData.MaxHp;
        MP = playerData.Mp;
        MaxMP = playerData.MaxMp;
        Food = playerData.Food;
        MaxFood = playerData.MaxFood;
        Gold = playerData.Gold;
        HeadIcon = playerData.HeadIcon;
        MapSkillID = playerData.MapSkillId;
        BattleSkillID = playerData.BattleSkillId;
    }
    

}
