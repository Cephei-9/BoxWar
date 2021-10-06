using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : EnemyBase {

    //public GameObject Parts;
    [SerializeField] EnemyParts _enemyParts;
    public int Level;
    private float _health;
    public Slider Slider;
    public Text LevelText;

    [SerializeField] private float _radius = 10f;
    protected bool _isActive;

    protected virtual void Update() {
        float distance = Vector3.Distance(_playerBox.transform.position, transform.position);
        _isActive = distance < _radius;
    }

    protected override void Start() {
        base.Start();
        _health = Level;
        Slider.maxValue = _health;
        LevelText.text = Level.ToString();
        TryKill(0, Vector3.zero);
    }

    public bool TryKill(float value, Vector3 velocity) {
        _health -= value;
        Slider.value = _health;
        if (_health <= 0f) {
            Die(velocity);
            return true;
        }
        return false;
    }

    public override void OnCollisionWithPlayer(PlayerBox playerBox, Collision collision) {
        base.OnCollisionWithPlayer(playerBox, collision);

        if (playerBox.IsMoveing) {
            bool enemyDead = TryKill(playerBox.DamageValue, collision.relativeVelocity);
            if (enemyDead) {

            } else {
                playerBox.TakeDamage(Level);
                playerBox.Rigidbody.AddForce(collision.relativeVelocity * 1.5f, ForceMode.VelocityChange);
            }
        } else {
            playerBox.TakeDamage(Level);
        }

    }

    protected override void Die(Vector3 velocity) {
        base.Die(velocity);
        _enemyParts.Push(velocity);
        //FindObjectOfType<BoxMove>()._charge = 1f;
        FindObjectOfType<PlayerBox>().ActivateTemporaryBoost(1f,1f);
    }

    private void OnDrawGizmos() {
#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
#endif
    }

}
