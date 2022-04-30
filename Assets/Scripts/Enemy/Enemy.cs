using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected float elapsedTime;
    protected Animator myAnim;
    private AudioSource audioSource;

    public virtual void Start()
    {
        elapsedTime = 0;
        myAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        move();
    }

    public virtual void move() {}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<Bullet>() != null) {
            StartCoroutine(waitToDestory());
        }
    }

    protected IEnumerator waitToDestory() {
        myAnim.SetBool("isAlive", false);
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
