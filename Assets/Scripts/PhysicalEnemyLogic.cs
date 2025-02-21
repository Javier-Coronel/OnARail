using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PhysicalEnemyLogic : MonoBehaviour
{
    public EnemyLogic enemyLogic;
    public CinemachineDollyCart enemyCart;
    public GameObject physicalEnemy;
    private void Start() {
        physicalEnemy=transform.parent.gameObject;
        enemyCart=physicalEnemy.transform.parent.GetComponent<CinemachineDollyCart>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.gameObject.CompareTag("Thowable")&&other.transform.parent.CompareTag("Untagged")){
            Debug.Log(other.tag);
            enemyCart.m_Speed=0;
            physicalEnemy.transform.LookAt(other.transform.position);
        }
    }
}
