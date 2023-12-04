using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    private Animator anim;
    public float moveSpeed = 10.0f;
    private bool isMoving = false;
    private int monsterCollisionCount = 0;

    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleAbilitiesInput();
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            if (!isMoving)
            {
                anim.SetBool("Run", true);
                isMoving = true;
            }

            float targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
            tr.rotation = Quaternion.Euler(0, targetAngle, 0);

            float currentMoveSpeed = moveSpeed;
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
    }

    void HandleAbilitiesInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            anim.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Monster Collision!");

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