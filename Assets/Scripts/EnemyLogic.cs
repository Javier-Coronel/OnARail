using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.U2D;
[ExecuteAlways]
public class EnemyLogic : MonoBehaviour
{
    int searchLayer;
    public float angle = 10;
    public GameObject physicalEnemy;
    public SpriteShapeController detectionZone;
    public float maxDistance = 5;
    public GameObject bulletPrefab;
    private Vector3 leftMod;
    private Vector3 rightMod;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
        {
            searchLayer = GameData.Instance.playerCart.gameObject.layer;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rightAngle = Mathf.Sin(angle * Mathf.Deg2Rad);
        float leftAngle = Mathf.Sin(-angle * Mathf.Deg2Rad);
        if (Application.isPlaying)
        {
            leftMod = physicalEnemy.transform.right * leftAngle;
            rightMod = physicalEnemy.transform.right * rightAngle;
            bool left = Physics.Raycast(physicalEnemy.transform.position, (physicalEnemy.transform.forward + leftMod), out RaycastHit leftHit, maxDistance, searchLayer);
            bool right = Physics.Raycast(physicalEnemy.transform.position, (physicalEnemy.transform.forward + rightMod), out RaycastHit rightHit, maxDistance, searchLayer);
            bool center = Physics.Raycast(physicalEnemy.transform.position, physicalEnemy.transform.forward, out RaycastHit centerHit, ((physicalEnemy.transform.forward + rightMod) * maxDistance).magnitude, searchLayer);
            Debug.Log("PlayerDetected " + left + " " + right + " " + center);
            if (left || right || center)
            {
                if (left && leftHit.transform.CompareTag("Player"))
                {
                    AtackPlayer(leftHit.transform);
                }
                else if (right && rightHit.transform.CompareTag("Player"))
                {
                    AtackPlayer(leftHit.transform);
                }
                else if (center && centerHit.transform.CompareTag("Player"))
                {
                    AtackPlayer(leftHit.transform);
                }
                Debug.Log("PlayerDetected " + leftHit + " " + rightHit + " " + centerHit);
            }
            Debug.DrawRay(physicalEnemy.transform.position, physicalEnemy.transform.forward * ((physicalEnemy.transform.forward + rightMod) * maxDistance).magnitude, Color.white);
            Debug.DrawRay(physicalEnemy.transform.position, (physicalEnemy.transform.forward + leftMod) * maxDistance, Color.blue);
            Debug.DrawRay(physicalEnemy.transform.position, (physicalEnemy.transform.forward + rightMod) * maxDistance, Color.red);
            detectionZone.spline.SetPosition(1, (physicalEnemy.transform.up + (Vector3.right * leftAngle)) * maxDistance);
            detectionZone.spline.SetPosition(2, physicalEnemy.transform.up * ((physicalEnemy.transform.forward + rightMod) * maxDistance).magnitude);
            detectionZone.spline.SetPosition(3, (physicalEnemy.transform.up + (Vector3.right * rightAngle)) * maxDistance);
            detectionZone.spline.SetLeftTangent(2, Vector3.left * maxDistance * .5f);
            detectionZone.spline.SetRightTangent(2, Vector3.right * maxDistance * .5f);
        }


    }
    private void Update()
    {
        if (!Application.isPlaying)
        {
            float rightAngle = Mathf.Sin(angle * Mathf.Deg2Rad);
            float leftAngle = Mathf.Sin(-angle * Mathf.Deg2Rad);
            Debug.DrawRay(physicalEnemy.transform.position, physicalEnemy.transform.forward * ((physicalEnemy.transform.forward + rightMod) * maxDistance).magnitude, Color.white);
            Vector3 center = physicalEnemy.transform.up * ((physicalEnemy.transform.forward + rightMod) * maxDistance).magnitude;
            Vector3 right = (physicalEnemy.transform.up + (Vector3.right * rightAngle)) * maxDistance;
            detectionZone.spline.SetPosition(1, (physicalEnemy.transform.up + (Vector3.right * leftAngle)) * maxDistance);
            detectionZone.spline.SetPosition(2, center);
            detectionZone.spline.SetPosition(3, right);
            detectionZone.spline.SetLeftTangent(2, Vector3.left * (center-right).magnitude);
            detectionZone.spline.SetRightTangent(2, Vector3.right * (center - right).magnitude);
        }
    }
    void AtackPlayer(Transform player)
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        Debug.Log("Atacado");
    }
}
