using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayer : MonoBehaviour
{
    //
    private float gravity = 9.81f; //9.81�� ������ �߷°��� ǥ��
    private float verticalVelocity = 0f;
    [SerializeField] private bool isGround = false; //�ٴ� ��ġ ����
    private bool isJump = false; //���� ����
    private Vector3 moveDir; //�̵� ����
    private Rigidbody rigid; //�÷��̾ �ٿ��ִ� RigidBody ���
    private CapsuleCollider cap;

    [Header("�÷��̾� ����")]
    [SerializeField] private float moveSpeed = 2f; //�̵��ӵ�
    [SerializeField] private float jumpForce = 5f; //������
    [SerializeField, Tooltip("���콺�� ���� ��")] private float mouseSensivity = 5f; //���콺 ����
    private Vector2 rotateValue; //���콺 ȸ�� ������

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
    /// �÷��̾ ���� ��� �ִ��� üũ
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
    /// �÷��̾��� ������
    /// </summary>
    private void Moving()
    {
        moveDir.z = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");
        rigid.velocity = transform.rotation * moveDir * moveSpeed;
    }
    
    /// <summary>
    /// �÷��̾� ����
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
    /// �߷� Ȯ��
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
