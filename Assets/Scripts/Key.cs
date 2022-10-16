using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private Door doorToUnlock;
    [SerializeField] private float keyRotationSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * keyRotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorToUnlock.UnlockDoor();
            gameObject.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        if(doorToUnlock != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, doorToUnlock.transform.position - transform.position);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f);
        }
    }
}
