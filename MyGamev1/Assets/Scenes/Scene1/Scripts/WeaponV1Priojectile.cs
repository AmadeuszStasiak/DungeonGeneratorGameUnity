using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponV1Priojectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsEnemy;

    public int damage;

    public GameObject destoyEffect;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsEnemy);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
            DestroyProjectile();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        //Instantiate(destoyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
