using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    public CharacterDatabase characterDB;
    private string selectedCharacter = "Mage";
    public CharacterManager characterManager;
    public ICharacter CreateCharacter()
    {
        
        return new Mage();
        
    }

    public string[] getSelectableCharacters()
    {
        return characterDB.getAllcharacters();
    }
    public void setSelectedCharacter(string selectedcharacter="Mage")
    {
        this.selectedCharacter = selectedcharacter;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
