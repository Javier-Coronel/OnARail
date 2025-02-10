using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyLogic : MonoBehaviour
{
    int searchLayer;
    public float angle = 10;
    public GameObject physicalEnemy;
    public SpriteShapeController detectionZone;
    public float maxDistance = 5;
    private Vector3 leftMod;
    private Vector3 rightMod;
    // Start is called before the first frame update
    void Start()
    {
        searchLayer = GameData.Instance.playerCart.gameObject.layer;
        leftMod = physicalEnemy.transform.right*Mathf.Sin(angle);
        rightMod = physicalEnemy.transform.right*Mathf.Sin(-angle);
        detectionZone.spline.SetPosition(1, (physicalEnemy.transform.up + rightMod) * maxDistance);
        detectionZone.spline.SetPosition(2, physicalEnemy.transform.up * (maxDistance+1.25f));
        detectionZone.spline.SetPosition(3, (physicalEnemy.transform.up + leftMod) * maxDistance);
        detectionZone.spline.SetRightTangent(1, -physicalEnemy.transform.right);
        detectionZone.spline.SetLeftTangent(1,  physicalEnemy.transform.right);
        detectionZone.spline.SetRightTangent(2, -physicalEnemy.transform.right);
        detectionZone.spline.SetLeftTangent(2,  physicalEnemy.transform.right);
        detectionZone.spline.SetRightTangent(3, -physicalEnemy.transform.right);
        detectionZone.spline.SetLeftTangent(3,  physicalEnemy.transform.right);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        leftMod = physicalEnemy.transform.right*Mathf.Sin(angle);
        rightMod = physicalEnemy.transform.right*Mathf.Sin(-angle);
        bool left = Physics.Raycast(physicalEnemy.transform.position, (physicalEnemy.transform.forward + leftMod), out RaycastHit leftHit, maxDistance, searchLayer);
        bool right = Physics.Raycast(physicalEnemy.transform.position, (physicalEnemy.transform.forward + rightMod), out RaycastHit rightHit, maxDistance, searchLayer);
        bool center = Physics.Raycast(physicalEnemy.transform.position, physicalEnemy.transform.forward, out RaycastHit centerHit, maxDistance, searchLayer);
        Debug.Log("PlayerDetected " + left + " " + right + " " + center);
        if(left||right||center){
            Debug.Log("PlayerDetected " + leftHit + " " + rightHit + " " + centerHit);
        }
        Debug.DrawRay(physicalEnemy.transform.position,  physicalEnemy.transform.forward * maxDistance, Color.white);
        Debug.DrawRay(physicalEnemy.transform.position, (physicalEnemy.transform.forward + leftMod) * maxDistance, Color.blue);
        Debug.DrawRay(physicalEnemy.transform.position, (physicalEnemy.transform.forward + rightMod) * maxDistance, Color.red);

        //Debug.Log( physicalEnemy.transform.forward);
        //Debug.Log((physicalEnemy.transform.forward + leftMod));
        //Debug.Log((physicalEnemy.transform.forward + rightMod));

    }
}
