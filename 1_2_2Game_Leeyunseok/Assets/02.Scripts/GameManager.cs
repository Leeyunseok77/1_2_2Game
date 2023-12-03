using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Transform> points = new List<Transform>();
    public GameObject monster;
    public float createTime = 3.0f;
    private bool isGameOver;

    // 이 부분을 추가
    public Text scoreText;

    private int score = 0;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

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

        // scoreText가 null이면 캔버스의 Text(TmP)를 찾아 할당
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
    }

    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }

        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);
        GameObject newMonster = Instantiate(monster, points[idx].position, points[idx].rotation);
        SetMonsterDirection(newMonster.transform, GetPlayerDirection());
    }

    Vector3 GetPlayerDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            return (points[Random.Range(0, points.Count)].position - player.transform.position).normalized;
        }
        else
        {
            return Vector3.forward;
        }
    }

    void SetMonsterDirection(Transform monsterTransform, Vector3 direction)
    {
        monsterTransform.forward = direction;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}