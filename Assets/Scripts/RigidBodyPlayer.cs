using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayer : MonoBehaviour
{
    //private float gravity = 9.81f; //9.81�� ������ �߷°��� ǥ��
    //[SerializeField] private float verticalVelocity = 0f; //�޴� �߷��� ��
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

    private Transform trsCam;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
        trsCam =transform.GetChild(0);
        //trsCam = transform.Find("Main Camera");
        //trsCam = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        CheckGround();
        Moving();
        Jumping();
        CheckGravity();

        Rotation();
    }

    /// <summary>
    /// �÷��̾ ���� ��� �ִ��� üũ
    /// </summary>
    private void CheckGround()
    {
        if (rigid.velocity.y <= 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down,
                       cap.height * 0.55f, LayerMask.GetMask("Ground"));
        }

        else if (rigid.velocity.y > 0)
        {
            isGround = false;
        }
    }

    /// <summary>
    /// �÷��̾��� ������
    /// </summary>
    private void Moving()
    {
        //moveDir.x = InputHorizontal();
        //moveDir.y = rigid.velocity.y;
        //moveDir.z = InputVertical();
        //rigid.velocity = transform.rotation * moveDir;

        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(new Vector3(0, 0, moveSpeed), ForceMode.Force);
        }

        else if (Input.GetKey(KeyCode.S))
        {            
            rigid.AddForce(new Vector3(0, 0, -moveSpeed), ForceMode.Force);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(new Vector3(-moveSpeed, 0, 0), ForceMode.Force);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(new Vector3(moveSpeed, 0, 0), ForceMode.Force);
        }
    }

    private float InputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private float InputVertical()
    {
        return Input.GetAxisRaw("Vertical") * moveSpeed;
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
            //verticalVelocity = jumpForce;
        }
    }

    /// <summary>
    /// �߷� Ȯ��
    /// </summary>
    private void CheckGravity()
    {
        #region ���� ���
        //if (isGround && !isJump) //�������� �ƴѻ��¿��� ���� �پ�������
        //{
        //    verticalVelocity = 0f;
        //}

        //else if (!isGround) //���� �� ������ ���� �پ����� ������
        //{
        //    verticalVelocity -= gravity * Time.deltaTime; //������ �߷����� ������ ��
        //}

        //if (isJump && !isGround)
        //{
        //    if (verticalVelocity < 0f)
        //    {
        //        isJump = false;
        //    }
        //}

        //else
        //{
        //    verticalVelocity -= gravity * Time.deltaTime;
        //}

        //rigid.velocity = new Vector3(rigid.velocity.x, verticalVelocity, rigid.velocity.z);
        #endregion

        if (isJump)
        {
            isJump = false;
            rigid.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse); //������ ������� ����
        }

    }

    /// <summary>
    /// ī�޶� ȸ��
    /// </summary>
    private void Rotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensivity * Time.deltaTime;

        rotateValue += new Vector2(-mouseY, mouseX);

        rotateValue.x = Mathf.Clamp(rotateValue.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(new Vector3(0, rotateValue.y, 0)); //�¿�� ������
        //���Ʒ��� ������ �� ĳ���Ͱ� �� �Ʒ��� ȸ��

        trsCam.rotation = Quaternion.Euler(rotateValue.x, rotateValue.y, 0); //�� �Ʒ��� ������
        //�¿�� ������ �� ĳ���Ͱ� �¿�� �������� ����
    }
}
