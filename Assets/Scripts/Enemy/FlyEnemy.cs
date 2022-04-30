using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    LocationManager location;
    private bool canMove = false, isDestinationSet = false;
    [SerializeField]Vector2 targetPosition;
    [SerializeField] private GameObject bullet;
    public override void Start() {
        location = FindObjectOfType<LocationManager>();
        targetPosition = transform.position;
        base.Start();
    }
    public override void move()
    {
        if (canMove) {
            do {
            targetPosition = new Vector2(location.Locations[(int)Random.Range(0, location.Locations.Count - 0.1f)].transform.position.x,
                                         transform.position.y);
            } while (Vector2.Distance(targetPosition, transform.position) <= 0.1f);
            canMove = false;
        }

        if (Vector2.Distance(transform.position, targetPosition) > 0.1f) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
            isDestinationSet = false;
            base.myAnim.SetBool("isMoving", true);
        }
        else {
            base.myAnim.SetBool("isMoving", false);
            if (!isDestinationSet)
                StartCoroutine(waitToMove());
        }
    }

    //after cool down, gameobject will move to a random location fomr location manager
    //then, shoot a bullet to player
    IEnumerator waitToMove() {
        isDestinationSet = true;
        yield return new WaitForSeconds(3.0f);
        canMove = true;
        shootBullet();
    }

    //shott toward target transform position a bullet
    private void shootBullet() {
        if (FindObjectOfType<PlayerControlArcade>() != null)
            Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
