using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager의 인스턴스를 저장할 정적 변수
    public static GameManager Instance { get; private set; }

    // 몬스터 출현 지점을 저장할 리스트
    public List<Transform> points = new List<Transform>();
    // 몬스터 프리팹
    public GameObject monster;
    // 몬스터 생성 주기
    public float createTime = 3.0f;
    // 게임 종료 여부
    private bool isGameOver;

    // 스코어 변수 및 캔버스 텍스트 컴포넌트에 대한 참조 추가
    public int score = 0;
    public TextMeshProUGUI scoreText; // 캔버스의 TextMeshProUGUI 컴포넌트에 대한 참조

    // 게임 종료 여부 속성 및 설정 시 몬스터 생성 중단
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                // 게임 종료 시 몬스터 생성 중단
                CancelInvoke("CreateMonster");
            }
        }
    }

    // GameManager의 인스턴스 설정
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 몬스터 출현 지점 초기화 및 일정 주기로 몬스터 생성 함수 호출
    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
        // Score Text를 코드로 찾아 연결
        GameObject canvas = GameObject.Find("Canvas"); // 캔버스 오브젝트의 이름에 따라 수정
        Transform scoreTextTransform = canvas.transform.Find("Score Text");

        // scoreTextTransform이 null이 아니라면 TextMeshProUGUI 컴포넌트 가져오기
        if (scoreTextTransform != null)
        {
            scoreText = scoreTextTransform.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Score Text (TextMeshProUGUI) not found under Canvas!");
        }
        // Set initial score text
        UpdateScoreText();

        // 일정 주기로 몬스터 생성 함수 호출
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    // 몬스터 생성 함수
    void CreateMonster()
    {
        // 랜덤한 지점에서 몬스터 생성
        int idx = Random.Range(0, points.Count);
        GameObject newMonster = Instantiate(monster, points[idx].position, points[idx].rotation);

        // 몬스터의 사라짐 이벤트에 대한 리스너 등록
        MonsterController monsterController = newMonster.GetComponent<MonsterController>();
        if (monsterController != null)
        {
            monsterController.OnMonsterDisappear.AddListener(IncreaseScore);
        }
    }

    // 플레이어 방향을 반환하는 함수
    Vector3 GetPlayerDirection()
    {
        // 플레이어 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // 플레이어 방향 계산
            return (points[Random.Range(0, points.Count)].position - player.transform.position).normalized;
        }
        else
        {
            // 플레이어가 없으면 기본 방향으로 설정
            return Vector3.forward;
        }
    }

    // 몬스터의 방향 설정 함수
    void SetMonsterDirection(Transform monsterTransform, Vector3 direction)
    {
        // 몬스터 방향 설정
        monsterTransform.forward = direction;
    }

    public void IncreaseScore()
    {
        // 스코어를 500 증가
        score += 500;

        // 캔버스의 스코어 텍스트 업데이트
        UpdateScoreText();

        // 스코어가 15000이 되면 End2 씬으로 전환
        if (score >= 15000)
        {
            SceneManager.LoadScene("End2");
        }
    }

    // 캔버스의 스코어 텍스트 업데이트 함수
    public void UpdateScoreText()
    {
        // 캔버스의 스코어 텍스트 업데이트
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogError("Score Text (TextMeshProUGUI) not found!");
        }
    }
}