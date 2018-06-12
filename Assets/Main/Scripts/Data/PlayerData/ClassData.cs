using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class ClassData
{
    private ClassType type = ClassType.None;
    private int characterID = 0;
    private ClassCharacterTableSetting table;

    public ClassType Type { get { return type; } }
    public int CharacterID { get { return characterID; } }
    public ClassCharacterTableSetting Table { get { return table; } }

    public ClassData(int characterID)
    {
        this.characterID = characterID;
        ClassCharacterTableSetting setting = ClassCharacterTableSettings.Get(characterID);
        if (setting!=null)
        {
            table = setting;
            type = (ClassType)table.ClassType;
        }
    }
}
