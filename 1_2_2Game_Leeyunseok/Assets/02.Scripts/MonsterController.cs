using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // ���� �̵� �ӵ�

    private void Start()
    {
        // ���Ͱ� ������ ������ �÷��̾� �������� ���� �̵��ϵ��� ����
        MoveInPlayerDirection();
    }

    void MoveInPlayerDirection()
    {
        // �÷��̾� ������Ʈ�� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // �÷��̾��� ��ġ���� ���Ͱ� ������ ��ġ�� ���ϴ� ������ ���
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // ������ ���� ����
            transform.forward = directionToPlayer;

            // ���Ϳ��� �̵� ��� �߰� (Rigidbody ���)
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
