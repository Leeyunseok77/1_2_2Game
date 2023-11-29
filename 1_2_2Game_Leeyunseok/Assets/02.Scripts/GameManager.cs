using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    // 몬스터 출현 지점을 저장할 리스트
    public List<Transform> points = new List<Transform>();
    // 몬스터 프리팹
    public GameObject monster;
    // 몬스터 생성 주기
    public float createTime = 3.0f;
    // 게임 종료 여부
    private bool isGameOver;

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

    void Start()
    {
        // 몬스터 출현 지점 초기화
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }

        // 일정 주기로 몬스터 생성 함수 호출
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    void CreateMonster()
    {
        // 랜덤한 지점에서 몬스터 생성
        int idx = Random.Range(0, points.Count);
        GameObject newMonster = Instantiate(monster, points[idx].position, points[idx].rotation);

        // 플레이어 방향을 구해서 몬스터에 전달
        SetMonsterDirection(newMonster.transform, GetPlayerDirection());
    }

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

    void SetMonsterDirection(Transform monsterTransform, Vector3 direction)
    {
        // 몬스터 방향 설정
        monsterTransform.forward = direction;        
    }
}