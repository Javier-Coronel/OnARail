using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileMovement : MonoBehaviour
{
    public bool bullet = false;
    public float velocity = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up*velocity*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(bullet && other.transform.CompareTag("Player"))
        {
            Debug.Log("Muerto :(");
        }
    }
}
