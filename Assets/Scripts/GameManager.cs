using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using Unity.VisualScripting;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviourPun
{
    public float locationX;
    public float locationY;
    public FixedJoystick joystick;
    private GameObject SelectedCharacter;
    public Vector3 konum;
    public float donmeHizi = 10.0f;
    [SerializeField]
    private UnityEngine.UI.Button btn1, btn2, btn3,btn4,btn5,btn6;
    public float countdownDuration = 1f;
    PhotonView view;
    public CharacterManager CManager;
    public bool isCharacterAnimationPlaying=false;
    [SerializeField] private Image healthBarSprite;
    public ICharacter icharacter;
    public ISkill iskill;
    private bool isClickable1 = true;
    private bool isClickable2 = true;
    private bool isClickable3 = true;
    public Image skillImage1,skillImage2,skillImage3;
    public UnityEngine.UI.Text skillCountText1, skillCountText2, skillCountText3;
    public Button btn;
    public Transform[] players;
    public GameObject character;
    public GameObject rivalCharacter;
    public float health;

    void Start()
    {
        skillImage1.sprite= Resources.Load<Sprite>(CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 3].skillSprite);
        skillImage2.sprite = Resources.Load<Sprite>(CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 2].skillSprite);
        skillImage3.sprite = Resources.Load<Sprite>(CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 1].skillSprite);
        players = FindObjectsOfType<Transform>();
    }





void Update()
    {

        view = GetComponent<PhotonView>();
        SelectedCharacter = GameObject.Find(CharacterManager.icharacter.instantieName.ToString());
        GameObject.Find(CharacterManager.icharacter.instantieName).GetComponent<ICharacter>();
        locationX = joystick.Horizontal;
        locationY = joystick.Vertical;
        konum = SelectedCharacter.transform.position;
        SelectedCharacter.transform.position = new Vector3(SelectedCharacter.transform.position.x + locationX * ((float)2) * Time.deltaTime, SelectedCharacter.transform.position.y * Time.deltaTime, SelectedCharacter.transform.position.z + locationY * ((float)2) * Time.deltaTime);
        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            WalkAnimation();
            Vector3 yeniYon = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            SelectedCharacter.transform.rotation = Quaternion.LookRotation(GetNewVelocity());
        }
        if (joystick.Horizontal == 0 && joystick.Vertical == 0 && isCharacterAnimationPlaying == false )
        {
            IdleAnimation();
        }
        PhotonView[] photonViews = GameObject.FindObjectsOfType<PhotonView>();
        List<PhotonView> photonViewListesi = new List<PhotonView>(photonViews);
        List<PhotonView> bulunanPhotonViews = photonViewListesi.FindAll(pv => pv.name.Contains("Clone"));

        if (bulunanPhotonViews.Count > 1)
        {
            //Debug.Log("Aranan kelime i�eren PhotonView'ler bulundu:");

            foreach (PhotonView bulunanPhotonView in bulunanPhotonViews)
            {
                //Debug.Log("ID: " + bulunanPhotonView.ViewID + ", �sim: " + bulunanPhotonView.name);
                character = GameObject.Find(bulunanPhotonView.name);
                
            }
            for (int i = 0; i < bulunanPhotonViews.Count; i++)
            {

                float distance = Vector3.Distance(GameObject.Find(bulunanPhotonViews[i].name).transform.position, GameObject.Find(bulunanPhotonViews[i + 1].name).transform.position);
                if (distance < 5f)
                {
                    if (photonView.IsMine!)
                    {
                        rivalCharacter = GameObject.Find(bulunanPhotonViews[i].name);
                        Image buttonImage = btn.GetComponent<Image>();
                        buttonImage.sprite = Resources.Load <Sprite>(rivalCharacter.GetComponent<ICharacter>().characterSprite);
                        //Debug.Log(bulunanPhotonViews[i].name);
                        rivalCharacter.GetComponent<ICharacter>().Health= healthBarSprite.fillAmount;
                        Debug.Log(rivalCharacter.GetComponent<ICharacter>().Health);
                    }
                    
                }
            }
        }
        else
        {
            //Debug.Log("Aranan kelime i�eren PhotonView bulunamad�.");
        }
        Debug.Log(rivalCharacter.GetComponent<ICharacter>().Health);
    }
    void ListelePhotonNesneleri()
    {
       
    }

    private Vector3 GetNewVelocity()
    {
        return new Vector3(joystick.Horizontal, 0, joystick.Vertical) * donmeHizi * Time.fixedDeltaTime;
    }

    public void WalkAnimation()
    {
        if (SelectedCharacter != null)
        {
            Animator CharacterAnimation = SelectedCharacter.GetComponent<Animator>();
            if (CharacterAnimation != null)
            {
                CharacterAnimation.Play("Walk");
                CharacterAnimation.Play("WalkForward");
            }
        }
    }
    public void IdleAnimation()
    {
        if (SelectedCharacter != null)
        {
            Animator CharacterAnimation = SelectedCharacter.GetComponent<Animator>();
            if (CharacterAnimation != null)
            {
                CharacterAnimation.Play("Idle");
            }
        }
    }
    private IEnumerator StartCountdown()
    {
        float currentTime = countdownDuration;

        while (currentTime > 0)
        {
            isCharacterAnimationPlaying = true;
            currentTime -= 1f;
            yield return new WaitForSeconds(1f);
            isCharacterAnimationPlaying = false;
        }
    }
    IEnumerator GeriSayim()
    {
        int value = CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 3].coolDown;
        while (value> 0f)
        {
            isClickable1 = false;
            Color imageColor = skillImage1.color;
            imageColor.a = 0.5f;
            skillImage1.color = imageColor;
            value--;
            yield return new WaitForSeconds(1f);
            skillCountText1.text = value.ToString();
            isClickable1 = true;
            Color imageColor2 = skillImage1.color;
            imageColor2.a = 1f;
            skillImage1.color = imageColor2;
            if (value==0)
            {
                skillCountText1.text = " ";
            }
        }
    }
    IEnumerator GeriSayim2()
    {
        int value = CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 2].coolDown;
        while (value > 0f)
        {
            isClickable2 = false;
            Color imageColor = skillImage2.color;
            imageColor.a = 0.5f;
            skillImage2.color = imageColor;
            value--;
            yield return new WaitForSeconds(1f);
            skillCountText2.text = value.ToString();
            isClickable2 = true;
            Color imageColor2 = skillImage2.color;
            imageColor2.a = 1f;
            skillImage2.color = imageColor2;
            if (value == 0)
            {
                skillCountText2.text = " ";
            }
        }
    }
    IEnumerator GeriSayim3()
    {
        int value = CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 1].coolDown;
        while ( value > 0f)
        {
            isClickable3 = false;
            Color imageColor = skillImage3.color;
            imageColor.a = 0.5f;
            skillImage3.color = imageColor;
            value--;
            yield return new WaitForSeconds(1f);
            skillCountText3.text = value.ToString();
            isClickable3 = true;
            Color imageColor2 = skillImage3.color;
            imageColor2.a = 1f;
            skillImage3.color = imageColor2;
            if (value == 0)
            {
                skillCountText3.text = " ";
            }
        }
    }
    public void attackOne()
    {
        if (isClickable1)
        {
            Animator CharacterAnimation = SelectedCharacter.GetComponent<Animator>();
            if (CharacterAnimation != null)
                CharacterAnimation.Play(CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 3].skillName);
            StartCoroutine(StartCountdown());
            healthBarSprite = SelectedCharacter.transform.Find("Canvas/Elite/Bars/Healthbar").GetComponent<Image>();
            health = healthBarSprite.fillAmount;
            health += 0.1f;
            StartCoroutine(GeriSayim());
        }

        ListelePhotonNesneleri();

    }
    public void attackTwo()
    {
        if (isClickable2)
        {
            Animator CharacterAnimation = SelectedCharacter.GetComponent<Animator>();
            if (CharacterAnimation != null)
                CharacterAnimation.Play(CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 2].skillName);
            StartCoroutine(StartCountdown());
            healthBarSprite = SelectedCharacter.transform.Find("Canvas/Elite/Bars/Healthbar").GetComponent<Image>();
            healthBarSprite.fillAmount += 0.1f;
            StartCoroutine(GeriSayim2());

        }
    }
    public void attackThree()
     {
        if (isClickable3)
        {
            Animator CharacterAnimation = SelectedCharacter.GetComponent<Animator>();
            if (CharacterAnimation != null)
                CharacterAnimation.Play(CharacterManager.selectedSkillList[CharacterManager.selectedSkillList.Count - 1].skillName);
            StartCoroutine(StartCountdown());
            healthBarSprite = SelectedCharacter.transform.Find("Canvas/Elite/Bars/Healthbar").GetComponent<Image>();
            healthBarSprite.fillAmount += 0.1f;
            StartCoroutine(GeriSayim3());
        }
    }

    }

