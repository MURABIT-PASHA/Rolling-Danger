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
            //saldýrý yönüne karar vermek için player ýn olduðu pozisyondan düþman pozisyonunu
            //çýkararak vektörü hesaplama 
            Vector3 targetDirection = player.transform.position - transform.position;

            // Düþmanýn Rigidbody sine kuvvet eklemek, hýz ve DeltaTÝme ile çarparak hareketi yumuþatmak
            // force mode hýz deðiþikliði olacak demektir, bunun anlamý bu güç düþman kütlesine bakmaksýzýn
            // bizim hýzýmýzý deðiþtirecektir.
            rb.AddForce(targetDirection * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            
            //Hýzýmýzý temp (geçici) yedekleyip daha sonra deðiþtireceðiz
            Vector3 newVelocity = rb.velocity;

            //Y düzleminde olan herhangi bir hýzý kaldýrýr
            newVelocity.y = 0;

            //Yeni hýzýmýzý Rigidbody'e atýyoruz
            rb.velocity = newVelocity;
        }
    }
}
