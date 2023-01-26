using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isOccupied = false;
    private GameObject dragContainer;

    private void Update() {
        if(transform.childCount > 0){
            isOccupied = true;
        }
        else{
            isOccupied = false;
        }
    }
}
