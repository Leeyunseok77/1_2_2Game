using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 몬스터 이동 속도

    private void Start()
    {
        // 몬스터가 생성될 때마다 플레이어 방향으로 직선 이동하도록 설정
        MoveInPlayerDirection();
    }

    void MoveInPlayerDirection()
    {
        // 플레이어 오브젝트를 찾음
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // 플레이어의 위치에서 몬스터가 생성된 위치로 향하는 방향을 계산
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // 몬스터의 방향 설정
            transform.forward = directionToPlayer;

            // 몬스터에게 이동 명령 추가 (Rigidbody 사용)
            Rigidbody monsterRigidbody = GetComponent<Rigidbody>();
            if (monsterRigidbody != null)
            {
                monsterRigidbody.velocity = directionToPlayer * moveSpeed;
            }
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }
}
