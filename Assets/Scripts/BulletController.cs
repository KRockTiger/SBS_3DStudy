using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 direction; //발사 방향
    private float force; //발사 힘(속도)
    private Rigidbody rigid; //유탄이 받을 물리 컴포넌트
    private bool isGrenade = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            Destroy(gameObject);
        }
    } // ==> 콜리전 코드는 리지드바디가 필요함

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
        if (isGrenade) { return; } //유탄을 사용할 경우 막기

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
    /// 외부에서 입력한 방향과 힘을 가져와서 적용시키기
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
