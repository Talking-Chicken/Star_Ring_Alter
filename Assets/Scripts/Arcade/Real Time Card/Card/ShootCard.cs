using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create a bullet at player postion, moving toward a specific direction
public class ShootCard : Card
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private bool isShootingDiagonal;
    public override void play()
    {
        PlayerControlArcade player = FindObjectOfType<PlayerControlArcade>();
        
        Bullet bullet = Instantiate(player.Bullet, 
                                    new Vector2(player.transform.position.x, player.transform.position.y + 0.1f), 
                                    player.transform.rotation).GetComponent<Bullet>();
        bullet.Direction = this.direction;

        //if shooting diagonally, create another bullet, and shoot to the opposite direction
        if (isShootingDiagonal) {
            Bullet other = Instantiate(player.Bullet, 
                                    new Vector2(player.transform.position.x, player.transform.position.y + 0.1f), 
                                    player.transform.rotation).GetComponent<Bullet>();
            other.Direction = new Vector2(this.direction.x * -1, this.direction.y);
        }
        
        base.play();
    }
}
