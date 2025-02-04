using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    int searchLayer;
    public float angle = 10;
    public GameObject physicalEnemy;
    public float maxDistance = 5;
    private Vector3 leftMod;
    private Vector3 rightMod;
    // Start is called before the first frame update
    void Start()
    {
        searchLayer = GameData.Instance.playerCart.gameObject.layer;
        leftMod = new Vector3(0, 0, Mathf.Sin(angle));
        rightMod = new Vector3(0, 0, Mathf.Sin(-angle));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        leftMod = new Vector3(0, 0, Mathf.Sin(angle));
        rightMod = new Vector3(0, 0, Mathf.Sin(-angle));
        Physics.Raycast(physicalEnemy.transform.position, (physicalEnemy.transform.right+ leftMod), out RaycastHit leftHit, maxDistance, searchLayer);
        Physics.Raycast(physicalEnemy.transform.position, (physicalEnemy.transform.right+ rightMod), out RaycastHit rightHit, maxDistance, searchLayer);
        Physics.Raycast(physicalEnemy.transform.position, physicalEnemy.transform.right, out RaycastHit centerHit, maxDistance, searchLayer);
        Debug.DrawRay(physicalEnemy.transform.position, physicalEnemy.transform.right * maxDistance, Color.white);
        Debug.DrawRay(physicalEnemy.transform.position, (physicalEnemy.transform.right + leftMod) * maxDistance, Color.white);
        Debug.DrawRay(physicalEnemy.transform.position, (physicalEnemy.transform.right + rightMod) * maxDistance, Color.white);
        Debug.Log(physicalEnemy.transform.right);
        Debug.Log((physicalEnemy.transform.right + leftMod));
        Debug.Log((physicalEnemy.transform.right + rightMod));

    }
}
