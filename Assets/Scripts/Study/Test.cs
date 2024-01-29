using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        //MouseFunction();

    }

    /// <summary>
    /// ���콺 Ŀ�� ǥ�� �ڵ�
    /// </summary>
    private void MouseFunction()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None) //���콺�� ���̰� ���������� ����
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            else if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }

            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
/*
01.29 ����
//��ũ��Ʈ�� ���� "������Ʈ ����" ȸ������ �������� ����
transform.up;
transform.forward;
transform.right;
==> ���⼭ down, back, left �ڵ�� �����Ƿ� -�� �ٿ��� �ڵ��Ѵ�. ex) -transform.up;

//�۷ι� ������ �������� ����
Vector3.up;
Vector3.forward;
Vector3.right;

//3D�� 2D ����ȣȯ?? => ����� �ƴϴ� : ���� ���� ������ ���� �� �ִ� ȯ���� �ֱ� ����

�ڵ�-----------------------------------------------------------------------------
//transform.position += transform.forward * Time.deltaTime; //�������� �̵�

//������ ����ϴ� �ڵ�
//transform.position += Rotation * transform.Position;
//transform.position += transform.rotation * Vector3.forward * Time.deltaTime;
//transform.position += transform.TransformDirection(Vector3.forward); 
*/
