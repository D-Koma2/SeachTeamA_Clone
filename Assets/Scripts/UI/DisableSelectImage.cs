using UnityEngine;

public class DisableSelectImage : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.Android)
        {
            this.gameObject.SetActive(false);
        }
    }
}
