using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndControl : MonoBehaviour
{
    public GameObject mainTrack;
    public Animator cartAnimator;
    public float maxMovementOnX=5;
    public float maxMovementOnZ=5;
    public float velocity = 1;
    private Vector3 onDashDirection;
    public float dashTime=1;
    public float dashSpeed=1;
    public float dashTimer=0;
    private GameObject leftHandObject = null;
    private GameObject rightHandObject = null;
    public Collider ownCollider;
    private float xMovement;
    private float zMovement;
    private bool dash;
    public bool onDash = false;
    public AudioClip dashSound;
    private int layer = 0;

    // Start is called before the first frame update
    void Start()
    {
        layer = gameObject.layer;
    }
    void Update()
    {
        if(!GameData.Instance.pauseMenuActivated){
            xMovement = Input.GetAxis("Horizontal");
            zMovement = Input.GetAxis("Vertical");
            dash = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return); 
            Vector3 movement = new Vector3(xMovement, 0, zMovement).normalized * velocity;
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
            if (onDashDirection.x != 0f || onDashDirection.z != 0f)
            {
                if (dashTimer >= dashTime)
                {
                    dashTimer = 0;
                    GetComponent<MeshRenderer>().materials[0].color = Color.white;
                    onDashDirection.x = 0;
                    onDashDirection.z = 0;
                    gameObject.layer = layer;
                    GameData.Instance.playerCart.m_Speed = 1;
                }
                else
                {
                    transform.Translate(onDashDirection*Time.deltaTime);
                }
            }
            else if (dash&&!onDash)
            {
                Dash(movement);
            }
            else
            {
                transform.Translate(movement * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Throw(true);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Throw(false);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Equipable"))
        {
            if (leftHandObject == null)
            {
                Equip(other.gameObject, true);
            }
            else if (rightHandObject == null)
            {
                Equip(other.gameObject, false);
            }
        }
        if (other.CompareTag("Route"))
        {
            other.GetComponent<AltRouteController>().setAltRoute();
            other.enabled=false;
        }
        if (other.CompareTag("Enemy")&&!onDash)
        {
            GameData.Instance.respawn();
            Debug.Log("Muerto :(");
            GameData.Instance.deleteTemporalObjects();
        }
        if (other.GetComponent<SpawnPosition>())
        {
            GameData.Instance.actualSpawnPosition = other.GetComponent<SpawnPosition>();
        }
    }
    void Equip(GameObject objectToEquip, bool toLeftHand)
    {
        if (toLeftHand) {
            leftHandObject = objectToEquip;
            objectToEquip.tag = "Untagged";
            objectToEquip.SetActive(false);
        }
        else
        {
            rightHandObject = objectToEquip;
            objectToEquip.tag = "Untagged";
            objectToEquip.SetActive(false);

        }
    }
    void Throw(bool fromLeftHand)
    {
        GameObject objectToThrow = fromLeftHand ? leftHandObject : rightHandObject;
        if (objectToThrow != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            ray.direction = new Vector3(ray.direction.x, 0, ray.direction.z);
            ray.origin = new Vector3(ray.origin.x+transform.localPosition.x, 0, ray.origin.z + transform.localPosition.z);
            Physics.Raycast(ray, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers);
            Debug.DrawRay(ray.origin,ray.direction,Color.white,Mathf.Infinity);
            //objectToThrow.SetActive(true);
            GameObject throwedObject = Instantiate(objectToThrow, hit.point, transform.rotation);
            throwedObject.SetActive(true);
            throwedObject.tag = "Untagged";
            Destroy(objectToThrow);
            if (fromLeftHand) {
                leftHandObject = null;
            }else rightHandObject = null;
        }
    }
    void Dash(Vector3 direction)
    {
        if (direction.x == 0f && direction.z == 0f)
        {
            direction=Vector3.forward*velocity;
        }
        cartAnimator.SetTrigger("Dash");
        GetComponent<MeshRenderer>().materials[0].color = Color.black;
        onDashDirection = direction*dashSpeed;
        GameData.Instance.playerCart.m_Speed = 2;
        gameObject.layer = 0;
        GameData.Instance.PlaySound(dashSound,0.6f,0.7f);
    }
}
