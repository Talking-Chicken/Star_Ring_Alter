using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyBullet : MonoBehaviour
{
    [ShowNonSerializedField] private Vector2 targetDirection;
    [ShowNonSerializedField] private float speed;
    private bool isAlive = true;
    private Animator myAnim;
    void Start()
    {
        speed = 2.0f;
        targetDirection = (FindObjectOfType<PlayerControlArcade>().transform.position - transform.position).normalized;
        myAnim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (isAlive) {
            transform.Translate(targetDirection * Time.deltaTime * speed);
        } else {
            myAnim.SetBool("isAlive", false);
            StartCoroutine(waitToDestroy());
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.GetComponent<Bullet>() != null)
            isAlive = false;
    }

    IEnumerator waitToDestroy() {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
