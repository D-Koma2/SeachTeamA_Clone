using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReStartButton : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private MenuSelector _menuSelector;
    [SerializeField] private ImageAlphaLerper _fadeLerper;

    public void OnButtonclick()
    {
        restartButton.interactable = false;
        _menuSelector.enabled = false;
        StartCoroutine(EnableButtonAfterDelay(1f));
    }

    IEnumerator EnableButtonAfterDelay(float delay)
    {
        // 完全に表示（不透明）にフェードイン
        _fadeLerper.FadeTo(2f);
        yield return new WaitForSeconds(delay);
        SceneReloader.Instance.loadGameScene("TitleScene");
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveAllListeners();
    }
}
