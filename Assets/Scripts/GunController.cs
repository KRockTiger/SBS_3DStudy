using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject objBullet; //������ �Ѿ�
    [SerializeField] private Transform trsMuzzle; //����Ȱ ��ġ
    [SerializeField] private Transform trsDynamic;
    private Camera camMain; //ǥ���� ī�޶� �ü� �������� ���߱� ������ ���
    private float distance = 250f; //��� �Ÿ�
    [SerializeField] private float gunForce = 100f; //�Ѿ��� �߻��� ��

    [Space]
    [SerializeField] private bool isGrenade;

    private void Start()
    {
        camMain = Camera.main;
    }

    private void Update()
    {
        GunPointer();
        CheckFire();
        CheckGrenade();
    }

    /// <summary>
    /// �ѱⰡ ī�޶� �Ѱ�� ���̴� ������Ʈ�� �븮���� ������� �Լ�
    /// </summary>
    private void GunPointer()
    {
        if (Physics.Raycast(camMain.transform.position, camMain.transform.forward,
            out RaycastHit hit, distance, LayerMask.GetMask("Ground"))) //(�߻� ��ǥ, �߻� ����, Ž���� ����� ...) 
        {
            transform.LookAt(hit.point);
        }

        else //�׶��� ������Ʈ�� �����ɽ�Ʈ�� ���� �ʾ��� ��
        {
            Vector3 lookPos = camMain.transform.position +
                              camMain.transform.forward * distance;
                              //����� �������� ������ �ִ�Ÿ���ŭ ��ġ�� ��ǥ����
            
            transform.LookAt(lookPos);
        }
    }

    /// <summary>
    /// �߻� Ʈ����
    /// </summary>
    private void CheckFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShotBullet();
        }
    }

    /// <summary>
    /// ��ź��� Ȯ��
    /// </summary>
    private void CheckGrenade()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isGrenade = false;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isGrenade = true;
        }
    }

    /// <summary>
    /// �Ѿ� ���� �� �߻�
    /// </summary>
    private void ShotBullet()
    {
        GameObject go = Instantiate(objBullet, trsMuzzle.position, trsMuzzle.rotation, trsDynamic);

        BulletController sc = go.GetComponent<BulletController>();

        if (isGrenade) //���� ���Ⱑ ��ź�̶��
        {
            sc.P_AddForce(gunForce * 0.5f);
        }

        //�ؿ��� ���Ⱑ ��ź�� �ƴ϶��
        //���� : �� ���ǹ��� �Ȱ��� �ڵ��̹Ƿ� �ѹ� ����غ���
        else if (Physics.Raycast(camMain.transform.position, camMain.transform.forward,
            out RaycastHit hit, distance, LayerMask.GetMask("Ground"))) //(�߻� ��ǥ, �߻� ����, Ž���� ����� ...) 
        {
            sc.P_SetDestination(hit.point, gunForce);
        }

        else //�׶��� ������Ʈ�� �����ɽ�Ʈ�� ���� �ʾ��� ��
        {
            Vector3 lookPos = camMain.transform.position +
                              camMain.transform.forward * 1000f;
            //����� �������� ������ �ִ�Ÿ���ŭ ��ġ�� ��ǥ����

            sc.P_SetDestination(lookPos, gunForce);
        }
    }
}
