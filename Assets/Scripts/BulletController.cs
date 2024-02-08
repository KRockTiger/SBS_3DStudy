using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 direction; //�߻� ����
    private float force; //�߻� ��(�ӵ�)
    private Rigidbody rigid; //��ź�� ���� ���� ������Ʈ
    private bool isGrenade = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            Destroy(gameObject);
        }
    } // ==> �ݸ��� �ڵ�� ������ٵ� �ʿ���

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (isGrenade) { return; } //��ź�� ����� ��� ����

        transform.position = Vector3.MoveTowards(transform.position, direction, force * Time.deltaTime);
        CheckDestination();
    }

    private void CheckDestination()
    {
        
        if (Vector3.Distance(transform.position, direction) == 0f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �ܺο��� �Է��� ����� ���� �����ͼ� �����Ű��
    /// </summary>
    /// <param name="_direction"></param>
    /// <param name="_force"></param>
    public void P_SetDestination(Vector3 _direction, float _force)
    {
        rigid.useGravity = false;
        direction = _direction;
        force = _force;
    }

    public void P_AddForce(float _force)
    {
        isGrenade = true;
        rigid.useGravity = true;
        rigid.AddForce(transform.rotation * Vector3.forward * _force, ForceMode.Impulse);
    }
}
