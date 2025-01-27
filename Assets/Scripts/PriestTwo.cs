using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestTwo : ICharacter,IPunObservable
{
    public int SkillOneCoolDown, SkillTwoCoolDown, SkillThreeCoolDown, SkillFourCoolDown, SkillFiveCoolDown = 0;
    public Sprite sprite;
    public GameObject Priest2;

    public UnityEngine.UI.Text nickName;

    public void Update()
    {
        
    }
    public void Start()
    {
        CharacterAnimation = Priest2.GetComponent<Animator>();
        nickName.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    public PriestTwo()
    {
        characterType = "Priest2";
        characterSprite = "CharacterSprites/Priest_Two";
        Health = 100;
        Range = 7;
        Speed = 10;
        Damage = 6;
        AttackPower = 10;
        Defence = 10;
        Description = "";
        instantieName = "Priest2(Clone)";
    }
}
