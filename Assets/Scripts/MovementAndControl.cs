using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndControl : MonoBehaviour
{
    public float maxMovementOnX=5;
    public float maxMovementOnZ=5;
    public float velocity = 1;
    private GameObject leftHandObject = null;
    private GameObject rightHandObject = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (transform.localPosition.x > maxMovementOnX)
        {
            transform.localPosition = new Vector3(maxMovementOnX, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x < -maxMovementOnX)
        {
            transform.localPosition = new Vector3(-maxMovementOnX, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.z > maxMovementOnZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxMovementOnZ);
        }
        else if (transform.localPosition.z < -maxMovementOnZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -maxMovementOnZ);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        bool dash = Input.GetKeyDown(KeyCode.Space);
        Vector3 movement = new Vector3(xMovement, 0, zMovement).normalized * velocity;
        transform.Translate(movement);
        if (dash)
        {

        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Equipable"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                leftHandObject = other.gameObject;
                Equip(other.gameObject, true);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                rightHandObject = other.gameObject;
                Equip(other.gameObject, false);
            }
        }
    }
    void Equip(GameObject objectToEquip, bool toLeftHand)
    {

    }
    void Throw(bool fromLeftHand)
    {
        GameObject objectToThrow = fromLeftHand ? leftHandObject : rightHandObject;
        if (objectToThrow != null) { 
            Instantiate(objectToThrow,transform.position, transform.rotation);
            Destroy(objectToThrow);
        }
    }
    void Dash(Vector3 direction)
    {

    }
}
