using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBox : MonoBehaviour {

    private float _damageValue;
    public Text DamageValueText;
    public float Health;
    public Rigidbody Rigidbody;

    private bool _invulnerable = false;

    [SerializeField] private Material _invulnerableMaterial;
    [SerializeField] private Renderer _renderer;
    private Material _startMaterial;
    [SerializeField] private GameObject _damageFrame;

    [HideInInspector]
    public bool IsMoveing;

    private void Start() {
        _startMaterial = _renderer.material;
        DamageValue = 2f;
        
        Rigidbody.centerOfMass = new Vector3(0f, 0.1f, 0f);

    }

    public float DamageValue {
        get {
            return _damageValue;
        }
        set {
            _damageValue = value;
            //DamageValueText.text = _damageValue.ToString("0.00");
        }
    }

    public void ActivateTemporaryBoost(float boostPrecent, float time) {
        StartCoroutine(TemporaryBoost(boostPrecent, time));
    }

    IEnumerator TemporaryBoost(float boostValue, float time) {
        DamageValue += boostValue;
        yield return new WaitForSeconds(time);
        DamageValue -= boostValue;
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.rigidbody) {
            EnemyBase enemyBase = collision.rigidbody.GetComponent<EnemyBase>();
            if (enemyBase) {
                enemyBase.OnCollisionWithPlayer(this, collision);
            }
        }

    }

    public void TakeDamage(float value) {

        if (_invulnerable) return;

        Debug.Log("TakeDamage  " +  value);

        _invulnerable = true;
        Invoke("StpoInvulnerable", 1f);
        Invoke("StopFrame",  0.2f);
        _renderer.material = _invulnerableMaterial;
        _damageFrame.SetActive(true);
        Health -= value;
        if (Health <= 0) {
            Die();
        }
    }

    void StopFrame() {
        _damageFrame.SetActive(false);
    }

    void StpoInvulnerable() {
        _invulnerable = false;
        _renderer.material = _startMaterial;
    }

    void Die() {
        Debug.Log("Die");
        FindObjectOfType<GameManager>().ShowLoseObject();
    }

}
