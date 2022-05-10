using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerStatus
{
    [SerializeField]
    int _level = 1;
    public int PlayerLevel
    {
        get { return _level; }
        set { _level = value; }
    }
    [SerializeField]
    float _Exp = 0;
    public float Exp
    {
        
        get { return _Exp; }      
        set 
        {
            _Exp += value;
            // 요구 경험치는 플레이어 레벨^2 * 10 >> 레벨이 2면 경험치 요구량 40 (임시)
            while (_Exp >= PlayerLevel * PlayerLevel * 10)                         
            {
                _Exp -= PlayerLevel * PlayerLevel * 10;
                PlayerLevel++;
            }
        }
    }
    [SerializeField]
    float _HP = 100.0f;
    public float HP
    {
        get { return _HP; }
        set { _HP = value; }
    }
    [SerializeField]
    float _hidepoint = 100.0f;
    public float Hidepoint
    {
        get { return _hidepoint; }
        set { _hidepoint = value; }
    }
    [SerializeField]
    int _gold = 0;
    public int Gold
    {
        get { return _gold;}
        set { _gold = value; }
    }

    public enum LOCATION
    {
        TOWN, DUNGEON
    }
    public LOCATION myLocation = LOCATION.TOWN;

    float _MoveSpeed = 5.0f;
    public float MoveSpeed
    {
        get { return _MoveSpeed; }
        set { _MoveSpeed = value; }
    }
    float _OriginMoveSpeed = 10.0f;
    public float OriginMoveSpeed
    {
        get { return _OriginMoveSpeed; }
        set { OriginMoveSpeed = value; }
    }
    float _UnlockingSpeed = 10.0f;
    public float UnlockingSpeed
    {
        get { return _UnlockingSpeed; }
        set { _UnlockingSpeed = value; }
    }

}

public class SPlayer : MonoBehaviour
{
    static public SPlayer instance;

    float hAxis;
    float vAxis;
    bool Running;
    Coroutine SpeedSet;


    #region Hide&Seek
    Coroutine Cloaking;
    SkinnedMeshRenderer[] _mySkin;
    SkinnedMeshRenderer[] MySkin
    {
        get 
        {
            if (_mySkin == null)
                _mySkin = this.GetComponentsInChildren<SkinnedMeshRenderer>();
            return _mySkin; 
        }
        set
        {
            _mySkin = value;
        }
    }

    public bool OnHide = false;
    bool Down = false;
    #endregion

    public PlayerStatus MyStatus;

    public Transform myPlayer;
    public STATE myState = STATE.NONE;

    public Transform mySpringArm;
    public UIManager_L myUIManager;

    #region Interaction
    public LayerMask InterMask;
    public LayerMask DungeonMask;
    SStock_Shelves myStock;
    [SerializeField]
    Open myDoor;
    public Open Door
    {
        get { return myDoor; }
        set { myDoor = value; }
    }
    SOrb myOrb;
    public SOrb Orb
    {
        get { return myOrb; }
        set { myOrb = value; }
    }
    #endregion
    #region Animation
    Animator _Anim = null;
    Animator myAnim
    {
        get
        {
            if (_Anim == null) _Anim = GetComponentInChildren<Animator>();
            return _Anim;
        }
    }
    SAnimEvent _animEvent = null;
    SAnimEvent myAnimEvent
    {
        get
        {
            if (_animEvent == null)
            {
                _animEvent = this.GetComponentInChildren<SAnimEvent>();
            }
            return _animEvent;
        }
    }
    #endregion

    [SerializeField]
    public List<Map> myMapList; // 플레이어 지도 리스트
    int _usingMapNum;
    public int UsingMapNum
    {
        get { return _usingMapNum;}
        set { _usingMapNum = value; }
    }
    public Map GetmyMap()
    {
        if (myMapList.Count != 0)
            return myMapList[UsingMapNum];

        return null;
    }
    public SMapData MapDatabase;
    public GameObject[] MyItem;

   

    public enum STATE
    {
        NONE, CREATE, PLAY, DEATH
    }

    void Start()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            ChangeState(STATE.CREATE);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();

    }

    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.CREATE:
                myAnimEvent.StandUp += () =>
                {
                    OnHide = false;
                };
                // 숨은 상태 해제되도록 하는 delegate 전달
                ChangeState(STATE.PLAY); // 생성후 Play STATE로 변경
                break;
            case STATE.PLAY:
                break;
            case STATE.DEATH:
                break;
        }
    }

    public void StateProcess()
    {

        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.PLAY:

                if (!OnHide)  // 엎드린 상태가 아닐때만 이동 가능
                {
                    hAxis = Input.GetAxis("Horizontal");
                    vAxis = Input.GetAxis("Vertical");
                    Running = Input.GetButton("Run");
                  
                    Vector3 pos = new Vector3(hAxis, 0, vAxis).normalized;
                    Vector3 CompVec = Quaternion.AngleAxis(mySpringArm.rotation.eulerAngles.y, Vector3.up) * pos;

                    Moving(CompVec);
                }

                if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsWalk") && MyStatus.Hidepoint > 5.0f)
                    Hiding();

                HideSystem();

                if (myDoor != null)
                    Unlocking();
                if (myOrb != null)
                    OrbSetting();

                CreateItem();
                break;
            case STATE.DEATH:
                break;
                
        }
    }
    public void Moving(Vector3 pos)
    {
        
        myAnim.SetBool("IsWalk", pos != Vector3.zero);
        switch(MyStatus.myLocation)
        {
            case PlayerStatus.LOCATION.TOWN:
            myAnim.SetBool("IsRun_T", Running);
                break;
            case PlayerStatus.LOCATION.DUNGEON:
            myAnim.SetBool("IsRun", Running);
                break;
        }
        
        myPlayer.LookAt(myPlayer.transform.position + pos);

        if (SpeedSet == null) // 함정에 걸리지 않은 상태에서만 작동
        {
            MyStatus.MoveSpeed = myAnim.GetBool("IsRun") || myAnim.GetBool("IsRun_T") ? MyStatus.OriginMoveSpeed : MyStatus.OriginMoveSpeed / 2;  //Run 상태면 5.0f, 아니면 절반
        }

        this.transform.Translate(pos * MyStatus.MoveSpeed * Time.deltaTime); // 이동  
    }

    public void Hiding()
    {
        if (Down == false)  // 숨지 않은 경우 숨는다
        {
            myAnim.SetTrigger("Hiding");  // Hiding 애니메이션 실행
            Down = true;
            OnHide = true;

            if (Cloaking != null) StopCoroutine(Cloaking);
            Cloaking = StartCoroutine(Clock());
        }
        else
        {
            myAnim.SetTrigger("StandUp");
            Down = false;
            if (Cloaking != null) StopCoroutine(Cloaking);
            Cloaking = StartCoroutine(Reveal());

        }
    }

    void HideSystem()
    {
        if (Down)
        {
            MyStatus.Hidepoint -= Time.deltaTime * 5.0f;
        }

        if (MyStatus.Hidepoint <= 0 && Down)  // Hidepoint가 0이고, 숨은 상태일 때
        {
            Down = false;
            OnHide = false;
            myAnim.SetTrigger("StandUp"); // 게이지 없으니 일어나게 만듬

            if (Cloaking != null) StopCoroutine(Cloaking);
            Cloaking = StartCoroutine(Reveal());
        }

        if (!Down && MyStatus.Hidepoint < 100.0f)
        {
            MyStatus.Hidepoint += Time.deltaTime; // 숨은 상태가 아니라면 hidepoint 최대치까지 회복

        }
        MyStatus.Hidepoint = Mathf.Clamp(MyStatus.Hidepoint, 0.0f, 100.0f);
    }

    IEnumerator SpeedDown(float speed, float time)
    {
        if (MyStatus.MoveSpeed > speed)  // 가장 강력한 디버프를 받을 수 있도록 하기 위한 조건
        {
            MyStatus.MoveSpeed = speed;
        }
        yield return new WaitForSeconds(time);
        MyStatus.MoveSpeed = MyStatus.OriginMoveSpeed;
        SpeedSet = null;
    }

    public void SetSpeed(float speed, float time)
    {
        SpeedSet = StartCoroutine(SpeedDown(speed, time));
    }

    public void Ondamage(float Damage)
    {
        MyStatus.HP -= Damage;
        if (OnHide == true)
        {
            if (Cloaking != null) StopCoroutine(Cloaking);
            Cloaking = StartCoroutine(Reveal()); // 맞았을 때 해제되도록
        }

        if (MyStatus.HP <= 0.0f)
        {
            MyStatus.HP = 0.0f;
            myAnim.SetTrigger("Death");  // 쓰러지는 애니메이션 출력
            ChangeState(STATE.DEATH);
        }
        else
        {
            myAnim.SetTrigger("Hit");  // 맞았을 때 애니메이션 출력
            Down = false;
            OnHide = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((InterMask & 1 << other.gameObject.layer) != 0)
        {
            myStock = other.GetComponent<SStock_Shelves>();
        }
        if ((DungeonMask & 1<< other.gameObject.layer) != 0)
        {
            int Col = other.gameObject.GetComponent<Dungeon>().Col;
            int Row = other.gameObject.GetComponent<Dungeon>().Row;
            myUIManager.SetMyButton(Row, Col);
        }
        /*
        if(other.gameObject.layer == LayerMask.NameToLayer("Door") && !other.gameObject.GetComponent<Open>().DoorOpen)
        {
            myDoor = other.gameObject.GetComponent<Open>();
        }
        */
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(myStock != null)
        {
            if(Input.GetKeyDown(KeyCode.E) && !other.gameObject.GetComponent<SStock_Shelves>().DisplayItem)
            {
                //DisplayingMyMap(other.transform);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((InterMask & 1 << other.gameObject.layer) != 0)
        {
            if (myStock != null)
                myStock = null;
        }
        /*
        if (other.gameObject.layer == LayerMask.NameToLayer("Door") && !other.gameObject.GetComponent<Open>().DoorOpen)
        {
            if (myDoor != null)
                myDoor = null;
        }
        */
    }

    public void SetMapData(int button_num, int data)
    {
        myMapList[UsingMapNum].SetRoomsDoor(button_num, data);
    }

    #region Interaction
    public void Unlocking()
    {
        if (Input.GetKeyDown(KeyCode.E) && !myAnim.GetBool("IsWalk"))
        {
            PlayerstatManagement_L.instance.UnlockSet(true);
            myUIManager.GetComponent<PlayerstatManagement_L>().UnlockGauge.GetComponent<SGauge>().myText.text = "문 여는중...";
            myAnim.SetBool("Unlocking", true);

        }
        if (Input.GetKey(KeyCode.E) && !myAnim.GetBool("IsWalk"))
        {
            myDoor.DoorUnlock(MyStatus.UnlockingSpeed);
            myUIManager.GetComponent<PlayerstatManagement_L>().Unlocking(myDoor.GetLockgauge());

            if (myDoor.DoorOpen)
            {
                myAnim.SetBool("Unlocking", false);
                myDoor = null;
            }

        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            myUIManager.GetComponent<PlayerstatManagement_L>().UnlockSet(false);
            myAnim.SetBool("Unlocking", false);
        }
    }
    public Open getMydoor()
    {
        return myDoor;
    }
    void OrbSetting()
    {
        if (Input.GetKeyDown(KeyCode.E))
            myOrb.SetAlpha();
    }
    #endregion

    public void DrawMap()
    {
        if(myMapList.Count == 0 )
        {
            SGameManager.instance.ShowMessage("던전에 들어가지 않아 지도를 생성할 수 없습니다");
            return;
        }

        int InkSlot = myUIManager.FindItem(MyItem[1].GetComponent<SItem>());
        int PaperSlot = myUIManager.FindItem(MyItem[2].GetComponent<SItem>());

        if(InkSlot != -1 && PaperSlot != -1)
        {
            myUIManager.myItemSlot[InkSlot].RemoveItem();
            myUIManager.myItemSlot[PaperSlot].RemoveItem();
            myUIManager.AddItem(MyItem[3].GetComponent<SItem>(), 1, MapDatabase.CompareMap(myMapList[UsingMapNum].Mapnum, myMapList[UsingMapNum]));
        }
        else
        {
            SGameManager.instance.ShowMessage("재료가 부족합니다");
        }
    }
    public void HealingHP(float value)
    {
        MyStatus.HP += value;
        if (MyStatus.HP > 100.0f) MyStatus.HP = 100.0f;
    }
    public void HealingHidePoint(float value)
    {
        MyStatus.Hidepoint += value;
        if (MyStatus.Hidepoint > 100.0f) MyStatus.Hidepoint = 100.0f;
    }

    IEnumerator Clock()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i<= 60; i++ )
        {
            for (int j = 0; j < MySkin.Length; j++)
            {
                MySkin[j].material.SetFloat("_DissolveAmount", (float)i / 60.0f);
            }
            yield return null;
        }
        Cloaking = null;
    }
    IEnumerator Reveal()
    {
        for (int i = 60; i >= 0; i--)
        {
            for(int j = 0; j < MySkin.Length; j++)
            {
                MySkin[j].material.SetFloat("_DissolveAmount", (float)i / 60.0f);
            }
            yield return null;
        }
        Cloaking = null;
    }

    IEnumerator DotDamage(float damage, GameObject effect = null)
    {
        GameObject eff = Instantiate(effect, this.gameObject.transform);
        ParticleSystem doteff = eff.GetComponent<ParticleSystem>();
        doteff.Play();
        var main = doteff.main;
        main.loop = true;

        
        while(damage > 0)
        {
            float deltaDamage = Time.deltaTime * 1.5f;
            if (damage < deltaDamage)
                deltaDamage = damage;

            damage -= deltaDamage;
            MyStatus.HP -= deltaDamage;
            yield return null;
        }
        Destroy(eff);
    }

    public void OnDotDamage(float damage, GameObject effect = null)
    {
        StartCoroutine(DotDamage(damage, effect));
    }

    public void CreateItem()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myUIManager.AddItem(SGameManager.instance.Itemlist[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myUIManager.AddItem(SGameManager.instance.Itemlist[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            myUIManager.AddItem(SGameManager.instance.Itemlist[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DrawMap();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SGameManager.instance.Save(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach(SItemSlot slot in myUIManager.myItemSlot)
            {
                if (slot.myItem != null)
                {
                    slot.RemoveItem(1);
                    break;
                }
            }
        }
    }



}
