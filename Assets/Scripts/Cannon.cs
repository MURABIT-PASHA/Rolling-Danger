using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform cannonHead;
    [SerializeField] private Transform cannonTip;
    [SerializeField] private Transform cannonBody;
    [SerializeField] private float shootingCoolDown = 3f;
    [SerializeField] private float laserPower = 20f;

    private bool isPlayerInRange = false;
    private GameObject player;
    private float timeLeftToShoot = 0;
    private LineRenderer cannonLaser;

    void Start()
    {
        cannonLaser = GetComponent<LineRenderer>();
        cannonLaser.sharedMaterial.color = Color.green;
        cannonLaser.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        timeLeftToShoot = shootingCoolDown;
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            cannonBody.transform.LookAt(player.transform);
            cannonHead.transform.LookAt(player.transform);
            cannonLaser.SetPosition(0,cannonTip.transform.position);
            cannonLaser.SetPosition(1, player.transform.position);
            timeLeftToShoot -= Time.deltaTime;
        }

        if(timeLeftToShoot <= shootingCoolDown * 0.5)
        {
            cannonLaser.sharedMaterial.color = Color.red;
        }
        if (timeLeftToShoot <= 0)
        {
            Debug.Log("Fired!");
            Vector3 directionToPushBack = player.transform.position - cannonTip.transform.position;
            player.GetComponent<Rigidbody>().AddForce(directionToPushBack * laserPower, ForceMode.Impulse);
            timeLeftToShoot = shootingCoolDown;
            cannonLaser.sharedMaterial.color= Color.green;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            cannonLaser.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        isPlayerInRange = false;
        cannonLaser.enabled = false;

        timeLeftToShoot = shootingCoolDown;

        cannonLaser.sharedMaterial.color = Color.green;
        }
        
    }
}
