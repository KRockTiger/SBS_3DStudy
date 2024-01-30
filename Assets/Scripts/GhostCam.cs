using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f; //���콺 ����
    [SerializeField] private float mouseMoveSpeed = 5f; //���콺 �̵��ӵ�
    private Vector3 rotateValue; //������Ʈ ���� ���� ����

    private void Start()
    {
        rotateValue = transform.rotation.eulerAngles; //���ʹϾ��� Vector3�� ����ȯ

        Cursor.lockState = CursorLockMode.Locked; //���� �����ϸ� ���콺 �ᱸ��
    }

    /// <summary>
    /// ���콺 Ŀ�� ǥ��
    /// </summary>
    private void CheckMouse()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked; //���콺�� ������ �ʵ��� �׻� ���콺�� ȭ���� ��� ����
            }

            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void Update()
    {
        Moving(); //�̵����
        Rotating(); //ȸ�����
        CheckMouse();
        //CheckFrame();
    }

    /// <summary>
    /// �̵������ ���� �Լ�
    /// </summary>
    private void Moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position +=
                transform.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.forward) * mouseMoveSpeed * Time.deltaTime;
            //  Vector3.forward * mouseMoveSpeed * Time.deltaTime; //�۷ι�
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position +=
                -transform.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.back * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.back) * mouseMoveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.position +=
                -transform.right * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.left * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.left) * mouseMoveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.position +=
                transform.right * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.right * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.right) * mouseMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //transform.position +=
            //   Vector3.up * mouseMoveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //transform.position +=
            //   Vector3.down * mouseMoveSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// ȸ������� ���� �Լ�
    /// </summary>
    private void Rotating()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateValue += new Vector3(-mouseY, mouseX);

        //if (rotateValue.x > 90f)
        //{
        //    rotateValue.x = 90f;
        //}

        //else if (rotateValue.x < -90f)
        //{
        //    rotateValue.x = -90f;
        //} ==> ȸ������ ����

        //�� �ּ����� ���� �ڵ带 �Ʒ��ڵ�� �����ϰ� ǥ�� ����
        //Mathf.Clamp() == ��� ���� ������ �ִ񰪰� �ּڰ����̿��� �����ϵ��� ����
        rotateValue.x = Mathf.Clamp(rotateValue.x, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(rotateValue); //Vector3���� Quaternion������ ��ȯ

        //SetActive�� ���� ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ�� �� �� ����
        //Enabled�� ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ�� �� �� ����
    }

    /// <summary>
    /// �������� Ȯ���ϴ� bool�� �Լ�
    /// </summary>
    /// <returns></returns>
    private bool CheckFrame(int _limitFrame)
    {
        //���� 1�����Ӵ� 0.016�ʰ� �ɸ���.
        int curFrame = (int)(1 / Time.deltaTime); //���� �������� ��Ÿ��
                                                  //==> int������ ����� �Ҽ����� ������ �����.

        //if (_limitFrame < curFrame)
        //{
        //    return true;
        //}

        //else
        //{
        //    return false;
        //}=>

        return _limitFrame < curFrame;
    }
}
