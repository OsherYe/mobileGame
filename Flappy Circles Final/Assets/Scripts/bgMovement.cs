using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
         if(!GameManager.instance.isGameOver){
            transform.Translate(-GameManager.instance.bgMoveSpeed*Time.deltaTime,0,0);
        }
    }
}
