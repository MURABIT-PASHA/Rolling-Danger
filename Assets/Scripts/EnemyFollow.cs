using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Rigidbody rb;
    private bool isPlayerInRange = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerInRange)
        {
            //sald�r� y�n�ne karar vermek i�in player �n oldu�u pozisyondan d��man pozisyonunu
            //��kararak vekt�r� hesaplama 
            Vector3 targetDirection = player.transform.position - transform.position;

            // D��man�n Rigidbody sine kuvvet eklemek, h�z ve DeltaT�me ile �arparak hareketi yumu�atmak
            // force mode h�z de�i�ikli�i olacak demektir, bunun anlam� bu g�� d��man k�tlesine bakmaks�z�n
            // bizim h�z�m�z� de�i�tirecektir.
            rb.AddForce(targetDirection * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            
            //H�z�m�z� temp (ge�ici) yedekleyip daha sonra de�i�tirece�iz
            Vector3 newVelocity = rb.velocity;

            //Y d�zleminde olan herhangi bir h�z� kald�r�r
            newVelocity.y = 0;

            //Yeni h�z�m�z� Rigidbody'e at�yoruz
            rb.velocity = newVelocity;
        }
    }
}
