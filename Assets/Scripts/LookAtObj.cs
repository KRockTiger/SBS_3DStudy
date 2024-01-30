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

        transform.LookAt(trsLookAt); //특정한 Transform값을 바라보게 하는 코드
    }
}
