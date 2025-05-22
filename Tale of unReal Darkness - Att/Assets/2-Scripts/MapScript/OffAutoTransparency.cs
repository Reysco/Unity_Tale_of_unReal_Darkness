using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffAutoTransparency : MonoBehaviour
{

    //Esse script de transparencia é somente para entrar 1x e permanece

    [Range(0, 1)]
    public float transparency;
    public Renderer targetRenderer;
    public LayerMask layerMask;
    public float fadeDuration;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            SetTransparency(transparency);
        }
    }

    private void SetTransparency(float alpha)
    {
        StartCoroutine("FadeCoroutine", alpha);
    }

    private IEnumerator FadeCoroutine(float fadeTo)
    {
        float timer = 0;
        Color currentColor = targetRenderer.material.color;
        float startAlpha = targetRenderer.material.color.a;

        while (timer < 1)
        {
            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime / fadeDuration;

            currentColor.a = Mathf.Lerp(startAlpha, fadeTo, timer);
            targetRenderer.material.color = currentColor;
        }

    }

}