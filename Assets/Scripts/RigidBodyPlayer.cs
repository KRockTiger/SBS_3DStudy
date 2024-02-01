using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayer : MonoBehaviour
{
    //
    private float gravity = 9.81f; //9.81로 지구의 중력값을 표현
    private float verticalVelocity = 0f;
    [SerializeField] private bool isGround = false; //바닥 터치 여부
    private bool isJump = false; //점프 여부
    private Vector3 moveDir; //이동 방향
    private Rigidbody rigid; //플레이어에 붙여있는 RigidBody 사용
    private CapsuleCollider cap;

    [Header("플레이어 스텟")]
    [SerializeField] private float moveSpeed = 2f; //이동속도
    [SerializeField] private float jumpForce = 5f; //점프력
    [SerializeField, Tooltip("마우스의 감도 값")] private float mouseSensivity = 5f; //마우스 감도
    private Vector2 rotateValue; //마우스 회전 각도값

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        CheckGround();
        Moving();
        Jumping();
        CheckGravity();
    }

    /// <summary>
    /// 플레이어가 땅에 닿아 있는지 체크
    /// </summary>
    private void CheckGround()
    {
        if (rigid.velocity.y < 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down,
                       cap.height * 0.55f, LayerMask.GetMask("Ground"));
        }
    }

    /// <summary>
    /// 플레이어의 움직임
    /// </summary>
    private void Moving()
    {
        moveDir.z = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");
        rigid.velocity = transform.rotation * moveDir * moveSpeed;
    }
    
    /// <summary>
    /// 플레이어 점프
    /// </summary>
    private void Jumping()
    {
        if (!isGround)
        { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    /// <summary>
    /// 중력 확인
    /// </summary>
    private void CheckGravity()
    {
        if (isGround)
        {
            verticalVelocity = 0f;
        }

        if (isJump)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        rigid.velocity = new Vector3(rigid.velocity.x, verticalVelocity, rigid.velocity.z);
    }
}
