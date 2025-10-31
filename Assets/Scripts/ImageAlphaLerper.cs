using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageAlphaLerper : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float duration = 1f; // •Ï‰»‚É‚©‚¯‚éŽžŠÔ

    private void Start()
    {
        targetImage.color = new Color(0f, 0f, 0f, 1f);
        FadeTo(0f);
    }

    public void FadeTo(float targetAlpha)
    {
        StartCoroutine(LerpAlpha(targetAlpha));
    }

    IEnumerator LerpAlpha(float targetAlpha)
    {
        Color startColor = targetImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            targetImage.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }

        targetImage.color = endColor; // ÅI’l‚ð•ÛØ
    }
}
