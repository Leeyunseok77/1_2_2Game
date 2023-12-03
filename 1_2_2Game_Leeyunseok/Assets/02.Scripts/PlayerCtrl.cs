using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    private Animator anim;
    private Rigidbody rb;
    public float moveSpeed = 10.0f;
    public float jumpForce = 5.0f;
    private bool isMoving = false;
    private bool isJumping = false;
    private int monsterCollisionCount = 0;

    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (moveDirection != Vector3.zero)
        {
            if (!isMoving)
            {
                anim.SetBool("Run", true);
                isMoving = true;
            }

            float targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
            tr.rotation = Quaternion.Euler(0, targetAngle, 0);

            float currentMoveSpeed = isJumping ? moveSpeed * 2.0f : moveSpeed;
            tr.Translate(moveDirection * currentMoveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            if (isMoving)
            {
                anim.SetBool("Run", false);
                isMoving = false;
            }
        }

        // Alt 키를 누르면 Jump 애니메이션 실행 및 캐릭터 점프
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            anim.SetTrigger("Jump");
            Jump();
        }

        // Ctrl 키를 누르면 Attack 애니메이션 실행
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            anim.SetTrigger("Attack");
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Monster Collision!");

            // 몬스터와 처음 충돌할 때만 충돌 횟수를 증가시킴
            if (monsterCollisionCount < 3)
            {
                monsterCollisionCount++;

                if (monsterCollisionCount >= 3)
                {
                    SceneManager.LoadScene("End");
                }
            }
        }
    }
}