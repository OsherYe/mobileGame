using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour{
private Vector3 _originalPos;
public static Camera _instance;

void Awake(){
    _originalPos = transform.localPosition;
    _instance = this;
}
}