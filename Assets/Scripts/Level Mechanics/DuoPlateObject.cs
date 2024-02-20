//Last Editor: Caleb Husselman
//Last Edited: Feb 14

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoPlateObject : MonoBehaviour {
    private DuoPlate parentDuo => GetComponentInParent<DuoPlate>();
    [SerializeField] private LayerMask playerLayer;
    private int playersOnPlate = 0;

    private void OnTriggerEnter(Collider other) {

        if((playerLayer.value & 1 << other.gameObject.layer) != 0) {
            playersOnPlate ++;
            if(playersOnPlate == 1) {
                parentDuo.Activate();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if ((playerLayer.value & 1 << other.gameObject.layer) != 0) {
            playersOnPlate--;
            if (playersOnPlate == 0) {
                parentDuo.Deactivate();
            }
        }
    }
}
