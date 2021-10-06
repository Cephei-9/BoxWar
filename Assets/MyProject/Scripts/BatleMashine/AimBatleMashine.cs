using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBatleMashine : MonoBehaviour
{
    [Header("Aim")]
    public BatleMashine batleMashine;
    public Vector2 angleVision;
    [SerializeField] private float _timeUpdateActualEnemy = 0.3f;

    protected List<Transform> enemies = new List<Transform>();
    protected Transform _actualEenemy;

    private Coroutine _serchCorutine;

    public bool hasEnemy 
    {
        get
        {
            foreach (var item in enemies)
            {
                if (item != null) return true;
            }
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAI>())
        {
            enemies.Add(other.transform);
        }
    }

    private IEnumerator SerchEnemyTransform()
    {
        while (true)
        {
            if (_actualEenemy)
            {
                Vector3 toEnemy = _actualEenemy.position - batleMashine.transform.position;
                if (CheckAngle(toEnemy) == false) _actualEenemy = null;              
            }
            else
            {
                GetTransformEnemy(out Transform transform);
                _actualEenemy = transform;
            }
            yield return new WaitForSeconds(_timeUpdateActualEnemy);
        }
    }

    public virtual bool GetTransformEnemy(out Transform transform) 
    {
        transform = null;
        foreach (var item in enemies)       
        {
            if (item != null)
            {
                Vector3 toEnemy = item.position - batleMashine.transform.position;

                if (CheckAngle(toEnemy)) 
                {
                    transform = item;
                    return true;
                }                
            }
        }
        return false;
    }

    private bool CheckAngle(Vector3 directionToEnemy)
    {
        Vector3 startTransformRight = Vector3Rotate.RotateVectorOnAngleInAxis(batleMashine.startForward, 90, Vector3.up);
        Vector3 toEnemyOnRightPlane = Vector3.ProjectOnPlane(directionToEnemy, startTransformRight);
        float angleX = Vector3.Angle(toEnemyOnRightPlane, batleMashine.startForward);

        Vector3 toEnemyOnUpPlane = Vector3.ProjectOnPlane(directionToEnemy, Vector3.up);
        float angleY = Vector3.Angle(toEnemyOnUpPlane, batleMashine.startForward);

        if (angleX < angleVision.x && angleY < angleVision.y) return true;
        return false;
    }

    public virtual void RotateBatleMashine(Vector3 enemyPosition)
    {
        batleMashine._targetRotation = Quaternion.LookRotation(enemyPosition - batleMashine._rotateTransform.position);
    }

    public virtual void ActivateAim()
    {
        this.enabled = true;
        _serchCorutine = StartCoroutine(SerchEnemyTransform());
    }

    public virtual void DeActivateAim()
    {
        this.enabled = false;
        if(_serchCorutine != null) StopCoroutine(_serchCorutine);
    }
}
