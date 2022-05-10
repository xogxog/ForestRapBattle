using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;//포톤 기능 사용
using TMPro;//텍스트 메쉬 프로 기능 사용
using Photon.Realtime;
using System.Linq;


public class LobbyManager : MonoBehaviourPunCallbacks//다른 포톤 반응 받아들이기
{
    [Header("Create Room References")]
    [SerializeField]
    private Button CreateRoomBtn;
    [SerializeField]
    private Button CancelRoomBtn;
    [SerializeField]
    private Button CreateConfirmBtn;
    [Space(5f)]

    [Header("Create Room Info References")]
    [SerializeField]
    private TMP_InputField RoomName;
    [SerializeField]
    private Button SecretOnBtn;
    [SerializeField]
    private Button SecretOffBtn;
    [SerializeField]
    private TMP_InputField RoomPw;
    [SerializeField]
    private GameObject CreateRoomPanel;
    [Space(5f)]

    [Header("Room List References")]
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [Space(5f)]

    [Header("User Info References")]
    [SerializeField]
    private TMP_Text Username;
    [SerializeField] Transform userCharContent;
    [SerializeField] GameObject bearPrefab;
    [SerializeField] GameObject buffaloPrefab;
    [SerializeField] GameObject catPrefab;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] GameObject chikPrefab;
    [SerializeField] GameObject dogPrefab;
    [SerializeField] GameObject duckPrefab;
    [SerializeField] GameObject elephantPrefab;
    [SerializeField] GameObject frogPrefab;
    [SerializeField] GameObject monkeyPrefab;
    [SerializeField] GameObject pigPrefab;
    [SerializeField] GameObject rabbitPrefab;
    [SerializeField] GameObject rhinoPrefab;
    [Space(5f)]

    [Header("Game Room References")]
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Button exitBtn;
    [SerializeField] Button startBtn;
    [SerializeField] Button readyBtn;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject bearinroomPrefab;
    [SerializeField] GameObject buffaloinroomPrefab;
    [SerializeField] GameObject catinroomPrefab;
    [SerializeField] GameObject chickeninroomPrefab;
    [SerializeField] GameObject chikinroomPrefab;
    [SerializeField] GameObject doginroomPrefab;
    [SerializeField] GameObject duckinroomPrefab;
    [SerializeField] GameObject elephantinroomPrefab;
    [SerializeField] GameObject froginroomPrefab;
    [SerializeField] GameObject monkeyinroomPrefab;
    [SerializeField] GameObject piginroomPrefab;
    [SerializeField] GameObject rabbitinroomPrefab;
    [SerializeField] GameObject rhinoinroomPrefab;
    [SerializeField] GameObject KickPlayerPanel;
    [SerializeField] TMP_Text KickPlayerText;
    [SerializeField] Button proceedKickBtn;
    [SerializeField] Button cancelKickBtn;
    [Space(5f)]

    [Header("Secret Room References")]
    [SerializeField] GameObject EnterPwPanel;
    [SerializeField] TMP_InputField enterRoomPwInput;
    [SerializeField] TMP_Text errorText;
    [SerializeField] Button confirmBtn;
    [SerializeField] Button cancelPwBtn;
    [Space(5f)]

    public static int charInt = 0;

    bool isSecret = false;

    public Color activatedColor;
    public Color deactivatedColor;

    public static LobbyManager Instance;//Launcher스크립트를 메서드로 사용하기 위해 선언

    private string targetRoomPw = "";
    private string targetRoomName = "";


    void Awake()
    {
        Instance = this;//메서드로 사용

        CreateRoomBtn.onClick.RemoveAllListeners();
        CreateRoomBtn.onClick.AddListener(ActivateModal);
        CancelRoomBtn.onClick.RemoveAllListeners();
        CancelRoomBtn.onClick.AddListener(DeactivateModal);
        SecretOnBtn.onClick.RemoveAllListeners();
        SecretOnBtn.onClick.AddListener(SecretOn);
        SecretOffBtn.onClick.RemoveAllListeners();
        SecretOffBtn.onClick.AddListener(SecretOff);
        CreateConfirmBtn.onClick.RemoveAllListeners();
        CreateConfirmBtn.onClick.AddListener(CreateRoom);

        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(LeaveRoom);

        proceedKickBtn.onClick.RemoveAllListeners();
        proceedKickBtn.onClick.AddListener(Kick);

        cancelKickBtn.onClick.RemoveAllListeners();
        cancelKickBtn.onClick.AddListener(DeactivateKickModal);

        confirmBtn.onClick.RemoveAllListeners();
        confirmBtn.onClick.AddListener(CheckPw);

        cancelPwBtn.onClick.RemoveAllListeners();
        cancelPwBtn.onClick.AddListener(DeactivatePwModal);

        readyBtn.onClick.RemoveAllListeners();
        readyBtn.onClick.AddListener(Ready);

        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(StartGame);
    }

    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();//설정한 포톤 서버에 때라 마스터 서버에 연결

        CreateRoomPanel.SetActive(false);
        RoomPw.interactable = false;

        string[] playerNames = new string[] { "물이차노", "교주와 신도", "곧싸탈예정인최명재", "꼬물이엄마", "합체왕도킹", "나는야조은누리" };
        string[] tiers = new string[] { "Iron", "Bronze", "Silver", "Gold", "Platinum", "Diamond" };
        //string[] characters = new string[] { "bear", "buffalo", "cat", "chicken", "chik", "dog", "duck", "elephant", "frog", "monkey", "pig", "rabbit", "rhino" };

        int randomPlayer = UnityEngine.Random.Range(0, 6);
        int randomTier = UnityEngine.Random.Range(0, 6);
        int randomChar = UnityEngine.Random.Range(0, 13);

        PhotonNetwork.NickName = playerNames[randomPlayer] + "(" + tiers[randomTier] + ")";
        Debug.Log(PhotonNetwork.NickName); //들어온사람 이름 랜덤으로 숫자붙여서 정해주기

        charInt = randomChar;

        if (charInt == 0)
        {
            Instantiate(bearPrefab, userCharContent);
        }
        else if (charInt == 1)
        {
            Instantiate(buffaloPrefab, userCharContent);
        }
        else if (charInt == 2)
        {
            Instantiate(catPrefab, userCharContent);
        }
        else if (charInt == 3)
        {
            Instantiate(chickenPrefab, userCharContent);
        }
        else if (charInt == 4)
        {
            Instantiate(chikPrefab, userCharContent);
        }
        else if (charInt == 5)
        {
            Instantiate(dogPrefab, userCharContent);
        }
        else if (charInt == 6)
        {
            Instantiate(duckPrefab, userCharContent);
        }
        else if (charInt == 7)
        {
            Instantiate(elephantPrefab, userCharContent);
        }
        else if (charInt == 8)
        {
            Instantiate(frogPrefab, userCharContent);
        }
        else if (charInt == 9)
        {
            Instantiate(monkeyPrefab, userCharContent);
        }
        else if (charInt == 10)
        {
            Instantiate(pigPrefab, userCharContent);
        }
        else if (charInt == 11)
        {
            Instantiate(rabbitPrefab, userCharContent);
        }
        else if (charInt == 12)
        {
            Instantiate(rhinoPrefab, userCharContent);
        }

        //플레이어 캐릭터 정보를 포톤 네트워크에 저장
        ExitGames.Client.Photon.Hashtable playerChar = new ExitGames.Client.Photon.Hashtable();
        playerChar.Add("charInt", (int)LobbyManager.charInt);
        PhotonNetwork.SetPlayerCustomProperties(playerChar);
    }

    public override void OnConnectedToMaster()//마스터서버에 연결시 작동됨
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();//마스터 서버 연결시 로비로 연결
        PhotonNetwork.AutomaticallySyncScene = true;//자동으로 모든 사람들의 scene을 통일 시켜준다. 
    }

    public override void OnJoinedLobby()//로비에 연결시 작동
    {
        Debug.Log("Joined Lobby");
        

        Username.text = PhotonNetwork.NickName;
    }

    private void ActivateModal()
    {
        CreateRoomPanel.SetActive(true);
    }

    private void DeactivateModal()
    {
        CreateRoomPanel.SetActive(false);
        RoomPw.interactable = false;
        RoomPw.text = "";
        RoomName.text = "";
        SecretOff();
    }

    public void ActivateKickModal(string targetName)
    {
        KickPlayerText.text = targetName + "님을 정말로 내보내시겠습니까?";
        KickPlayerPanel.SetActive(true);
    }

    private void DeactivateKickModal()
    {
        KickPlayerText.text = "";
        KickPlayerPanel.SetActive(false);
    }

    public void ActivatePwModal()
    {
        EnterPwPanel.SetActive(true);
    }

    private void DeactivatePwModal()
    {
        enterRoomPwInput.text = "";
        EnterPwPanel.SetActive(false);
        errorText.text = "";
    }

    private void SecretOn()
    {
        isSecret = true;

        ColorBlock OnBlock = SecretOnBtn.colors;
        ColorBlock OffBlock = SecretOffBtn.colors;

        OnBlock.normalColor = activatedColor;
        OnBlock.highlightedColor = activatedColor;
        OnBlock.pressedColor = activatedColor;
        SecretOnBtn.colors = OnBlock;

        OffBlock.normalColor = deactivatedColor;
        OffBlock.highlightedColor = deactivatedColor;
        OffBlock.pressedColor = deactivatedColor;
        SecretOffBtn.colors = OffBlock;

        RoomPw.interactable = true;
        RoomPw.Select();
    }

    private void SecretOff()
    {
        isSecret = false;
        ColorBlock OnBlock = SecretOnBtn.colors;
        ColorBlock OffBlock = SecretOffBtn.colors;

        OnBlock.normalColor = deactivatedColor;
        OnBlock.highlightedColor = deactivatedColor;
        OnBlock.pressedColor = deactivatedColor;
        SecretOnBtn.colors = OnBlock;

        OffBlock.normalColor = activatedColor;
        OffBlock.highlightedColor = activatedColor;
        OffBlock.pressedColor = activatedColor;
        SecretOffBtn.colors = OffBlock;

        RoomPw.text = "";
        RoomPw.interactable = false;
    }

    public void CreateRoom()//방만들기
    {
        if (string.IsNullOrEmpty(RoomName.text))
        {
            return;//방 이름이 빈값이면 방 안만들어짐
        }

        string[] propertiesListedInLobby = new string[3];
        propertiesListedInLobby[0] = "Owner";
        propertiesListedInLobby[1] = "isSecret";
        propertiesListedInLobby[2] = "Password";

        ExitGames.Client.Photon.Hashtable openWith = new ExitGames.Client.Photon.Hashtable();
        openWith.Add("Owner", PhotonNetwork.NickName);
        openWith.Add("isSecret", isSecret.ToString());

        if (isSecret)
        {
            openWith.Add("Password", RoomPw.text);
        }

        PhotonNetwork.CreateRoom(RoomName.text, new RoomOptions
        {
            MaxPlayers = 2,
            IsVisible = true,
            IsOpen = true,
            CustomRoomProperties = openWith,
            CustomRoomPropertiesForLobby = propertiesListedInLobby
        });
        Debug.Log(openWith["Owner"]);
        Debug.Log(openWith["isSecret"]);
        Debug.Log(openWith["Password"]);
        DeactivateModal();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)//포톤의 룸 리스트 기능
    {
        foreach (Transform trans in roomListContent)//존재하는 모든 roomListContent
        {
            Destroy(trans.gameObject);//룸리스트 업데이트가 될때마다 싹지우기
        }
        for (int i = 0; i < roomList.Count; i++)//방갯수만큼 반복
        {
            if (roomList[i].RemovedFromList)//사라진 방은 취급 안한다. 
                continue;
            Debug.Log("Initiating.......");
            Debug.Log(roomList[i].CustomProperties["isSecret"]);
            Debug.Log(roomList[i].CustomProperties["Owner"]);
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            //instantiate로 prefab을 roomListContent위치에 만들어주고 그 프리펩은 i번째 룸리스트가 된다. 
        }
    }

    public override void OnJoinedRoom()//최초 방 생성 후 방에 들어갔을때 작동
    {
        Debug.Log("Joined Room");
        MenuManager.Instance.OpenMenu("Room");//룸 메뉴 열기

        roomNameText.text = "방 제목 : " + PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < players.Count(); i++)
        {
            if (((int)players[i].CustomProperties["charInt"] == 0))
            {
                Instantiate(bearinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
            }
            else if (((int)players[i].CustomProperties["charInt"] == 1))
            {
                Instantiate(buffaloinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 2))
            {
                Instantiate(catinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 3))
            {
                Instantiate(chickeninroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 4))
            {
                Instantiate(chikinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 5))
            {
                Instantiate(doginroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 6))
            {
                Instantiate(duckinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 7))
            {
                Instantiate(elephantinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 8))
            {
                Instantiate(froginroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 9))
            {
                Instantiate(monkeyinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 10))
            {
                Instantiate(piginroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 11))
            {
                Instantiate(rabbitinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
            else if (((int)players[i].CustomProperties["charInt"] == 12))
            {
                Instantiate(rhinoinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); ;
            }
        }
        Debug.Log(PhotonNetwork.IsMasterClient);
        startBtn.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        readyBtn.gameObject.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)//다른 플레이어가 나가서 내가 방장이 되었을 때
    {
        Debug.Log("OnMasterClientSwitched");
        startBtn.gameObject.SetActive(newMasterClient.IsMasterClient);
        readyBtn.gameObject.SetActive(!newMasterClient.IsMasterClient);
        PhotonNetwork.CurrentRoom.CustomProperties["Owner"] = newMasterClient.NickName;
    }


    public void LeaveRoom() // 퇴장
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Lobby");//다시 로비로
        
    }

    public void JoinRoom(RoomInfo info) //입장
    {
        if (info.CustomProperties["Password"] != null) // 비밀방인 경우
        {
            ActivatePwModal();
            targetRoomName = info.Name;
            targetRoomPw = info.CustomProperties["Password"].ToString();
            Debug.Log(targetRoomName);
            Debug.Log(targetRoomPw);
        }
        else
        {
            PhotonNetwork.JoinRoom(info.Name);
            MenuManager.Instance.OpenMenu("Room");
        }
       
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)//다른 플레이어 입장했을 때
    {
        if ((int)newPlayer.CustomProperties["charInt"] == 0)
        {
            Instantiate(bearinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 1)
        {
            Instantiate(buffaloinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 2)
        {
            Instantiate(catinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 3)
        {
            Instantiate(chickeninroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 4)
        {
            Instantiate(chikinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 5)
        {
            Instantiate(doginroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 6)
        {
            Instantiate(duckinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 7)
        {
            Instantiate(elephantinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 8)
        {
            Instantiate(froginroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 9)
        {
            Instantiate(monkeyinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 10)
        {
            Instantiate(piginroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 11)
        {
            Instantiate(rabbitinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        else if ((int)newPlayer.CustomProperties["charInt"] == 12)
        {
            Instantiate(rhinoinroomPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
    }

    //public override void OnPlayerLeftRoom(Player otherPlayer)//다른 플레이어 퇴장했을 때
    //{
    //    Debug.Log("Other Player Left");
    //}

    private void Kick()
    {
        KickPlayerText.text = "";
        KickPlayerPanel.SetActive(false);
        PlayerListItem.Instance.KickProceed();
    }

    private void CheckPw()
    {
        if (enterRoomPwInput.text == targetRoomPw)
        {
            DeactivatePwModal();
            PhotonNetwork.JoinRoom(targetRoomName);
            MenuManager.Instance.OpenMenu("Room");
            //방 입장 후에 초기화해야됨.....
            targetRoomPw = "";
            targetRoomName = "";
        }
        else
        {
            errorText.text = "비밀번호가 틀립니다.";
        }
    }

    private void Ready()
    {
        PlayerListItem.isReady = !PlayerListItem.isReady;
        Debug.Log("ready?" + PlayerListItem.isReady);
        PlayerListItem.Instance.ReadyBtn();
    }

    public void StartGame()
    {
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Player player in players)
        {
            if (!player.IsMasterClient) //방장이 아닌 플레이어에 대해
            {
                if (player.CustomProperties["isReady"] != null)
                {
                    if ((bool)player.CustomProperties["isReady"]) //레디 상태가 true이면
                    {
                        Debug.Log("the other player is ready for game!");
                        PhotonNetwork.CurrentRoom.IsOpen = false; //게임시작 및 초기화
                        Debug.Log(PhotonNetwork.CurrentRoom.IsOpen);
                        PlayerListItem.isReady = false;
                        Debug.Log(PlayerListItem.isReady);
                        //PhotonNetwork.LoadLevel("Game_temp");
                        PhotonNetwork.LoadLevel("PhotonTest");
                    }

                    else //레디 프로퍼티가 있지만 false값
                    {
                        Debug.Log("the other player is not ready for game");
                    }
                }

                else //레디 프로퍼티가 없음
                {
                    Debug.Log("the other player is not ready for game");
                }
            }
        }
    }

}