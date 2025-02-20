using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileMovement : MonoBehaviour
{
    public bool bullet = false;
    public float velocity = 1f;
    public Transform objective;
    // Update is called once per frame
    void Update()
    {
        transform.position =Vector3.MoveTowards(transform.position, objective?objective.position:(transform.position+transform.up), velocity*Time.deltaTime);
    }
}
