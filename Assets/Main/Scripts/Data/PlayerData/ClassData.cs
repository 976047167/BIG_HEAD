using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class ClassData
{
    private ClassType type = ClassType.None;
    private int characterID = 0;
    private ClassTableSetting classTable;
    private ClassCharacterTableSetting characterTable;

    public ClassType Type { get { return type; } }
    public int CharacterID { get { return characterID; } }
    public ClassTableSetting ClassTable { get { return classTable; } }
    public ClassCharacterTableSetting CharacterData { get { return characterTable; } }

    public ClassData(int characterID)
    {
        this.characterID = characterID;
        characterTable = ClassCharacterTableSettings.Get(characterID);
        if (characterTable == null)
        {
            Debug.LogError("character id = " + characterID + " is not exist!");
        }
        type = (ClassType)characterTable.ClassType;
        classTable = ClassTableSettings.Get((int)type);
        if (classTable == null)
        {
            Debug.LogError("class type = [" + type.ToString() + "] is not exist!");
        }

    }
}
