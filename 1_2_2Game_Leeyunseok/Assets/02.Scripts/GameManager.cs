using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager�� �ν��Ͻ��� ������ ���� ����
    public static GameManager Instance { get; private set; }

    // ���� ���� ������ ������ ����Ʈ
    public List<Transform> points = new List<Transform>();
    // ���� ������
    public GameObject monster;
    // ���� ���� �ֱ�
    public float createTime = 3.0f;
    // ���� ���� ����
    private bool isGameOver;

    // ���ھ� ���� �� ĵ���� �ؽ�Ʈ ������Ʈ�� ���� ���� �߰�
    public int score = 0;
    public TextMeshProUGUI scoreText; // ĵ������ TextMeshProUGUI ������Ʈ�� ���� ����

    // ���� ���� ���� �Ӽ� �� ���� �� ���� ���� �ߴ�
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                // ���� ���� �� ���� ���� �ߴ�
                CancelInvoke("CreateMonster");
            }
        }
    }

    // GameManager�� �ν��Ͻ� ����
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

    // ���� ���� ���� �ʱ�ȭ �� ���� �ֱ�� ���� ���� �Լ� ȣ��
    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
        // Score Text�� �ڵ�� ã�� ����
        GameObject canvas = GameObject.Find("Canvas"); // ĵ���� ������Ʈ�� �̸��� ���� ����
        Transform scoreTextTransform = canvas.transform.Find("Score Text");

        // scoreTextTransform�� null�� �ƴ϶�� TextMeshProUGUI ������Ʈ ��������
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

        // ���� �ֱ�� ���� ���� �Լ� ȣ��
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    // ���� ���� �Լ�
    void CreateMonster()
    {
        // ������ �������� ���� ����
        int idx = Random.Range(0, points.Count);
        GameObject newMonster = Instantiate(monster, points[idx].position, points[idx].rotation);

        // ������ ����� �̺�Ʈ�� ���� ������ ���
        MonsterController monsterController = newMonster.GetComponent<MonsterController>();
        if (monsterController != null)
        {
            monsterController.OnMonsterDisappear.AddListener(IncreaseScore);
        }
    }

    // �÷��̾� ������ ��ȯ�ϴ� �Լ�
    Vector3 GetPlayerDirection()
    {
        // �÷��̾� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // �÷��̾� ���� ���
            return (points[Random.Range(0, points.Count)].position - player.transform.position).normalized;
        }
        else
        {
            // �÷��̾ ������ �⺻ �������� ����
            return Vector3.forward;
        }
    }

    // ������ ���� ���� �Լ�
    void SetMonsterDirection(Transform monsterTransform, Vector3 direction)
    {
        // ���� ���� ����
        monsterTransform.forward = direction;
    }

    public void IncreaseScore()
    {
        // ���ھ 500 ����
        score += 500;

        // ĵ������ ���ھ� �ؽ�Ʈ ������Ʈ
        UpdateScoreText();

        // ���ھ 15000�� �Ǹ� End2 ������ ��ȯ
        if (score >= 15000)
        {
            SceneManager.LoadScene("End2");
        }
    }

    // ĵ������ ���ھ� �ؽ�Ʈ ������Ʈ �Լ�
    public void UpdateScoreText()
    {
        // ĵ������ ���ھ� �ؽ�Ʈ ������Ʈ
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