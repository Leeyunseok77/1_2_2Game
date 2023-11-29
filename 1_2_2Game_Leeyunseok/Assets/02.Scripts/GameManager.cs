using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    // ���� ���� ������ ������ ����Ʈ
    public List<Transform> points = new List<Transform>();
    // ���� ������
    public GameObject monster;
    // ���� ���� �ֱ�
    public float createTime = 3.0f;
    // ���� ���� ����
    private bool isGameOver;

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

    void Start()
    {
        // ���� ���� ���� �ʱ�ȭ
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }

        // ���� �ֱ�� ���� ���� �Լ� ȣ��
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    void CreateMonster()
    {
        // ������ �������� ���� ����
        int idx = Random.Range(0, points.Count);
        GameObject newMonster = Instantiate(monster, points[idx].position, points[idx].rotation);

        // �÷��̾� ������ ���ؼ� ���Ϳ� ����
        SetMonsterDirection(newMonster.transform, GetPlayerDirection());
    }

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

    void SetMonsterDirection(Transform monsterTransform, Vector3 direction)
    {
        // ���� ���� ����
        monsterTransform.forward = direction;        
    }
}