using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObj : MonoBehaviour
{
    [SerializeField] Transform trsLookAt;

    private void Update()
    {
        if (trsLookAt == null)
        { return; }

        transform.LookAt(trsLookAt); //Ư���� Transform���� �ٶ󺸰� �ϴ� �ڵ�
    }
}
