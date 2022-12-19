using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if(!GameManager.instance.isGameOver){
            if(Input.GetMouseButtonDown(0)){
                GameManager.instance.TapSound.Play();
                rb.velocity=Vector3.up * GameManager.instance.jumpSpeed;
                transform.Rotate(0,0,GameManager.instance.playerRotationSpeedUp);
            }

            else{
                transform.Rotate(0,0,-GameManager.instance.playerRotationSpeedDown);
            }
        }
        else{
            rb.velocity=Vector2.zero;
        }  
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag=="ring"){
            GameManager.instance.ExplosionSound.Play();
            Instantiate(GameManager.instance.explodePrefab,transform.position,transform.rotation);
            GameManager.instance.isGameOver=true;
            GameManager.instance.failedPanel.SetActive(true);
            Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag=="collect"){
                GameManager.instance.PointSound.Play();
                GameManager.instance.Score=GameManager.instance.Score+1;
        }
    }
}
