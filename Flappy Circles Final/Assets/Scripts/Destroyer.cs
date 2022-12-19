using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag=="bg"){
            Destroy(col.gameObject);
            GameManager.instance.SpawnBG();
           
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        Destroy(col.transform.root.gameObject);
    }
}
