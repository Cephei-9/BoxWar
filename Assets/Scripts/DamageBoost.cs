using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoost : MonoBehaviour {

    [SerializeField] private float _boostValue = 1;
    [SerializeField] private float _boostTime = 1;

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody) {
            PlayerBox playerBox = other.attachedRigidbody.GetComponent<PlayerBox>();
            if (playerBox) {
                Collect(playerBox);
            }
            
        }
    }

    public void Collect(PlayerBox playerBox) {
        playerBox.ActivateTemporaryBoost(_boostValue, _boostTime);
        Destroy(gameObject);
    }

}
