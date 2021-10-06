using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPart : MonoBehaviour {

    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private float _scaleTime = 1f;

    private void Start() {
        StartCoroutine(ScaleAnimation());
    }

    IEnumerator ScaleAnimation() {
        Vector3 startScale = transform.localScale;
        yield return new WaitForSeconds(10f);
        for (float t = 0; t < 1f; t+=Time.deltaTime / _scaleTime) {
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = startScale * scale;
            yield return null;
        }
        Destroy(gameObject);
    }

}
