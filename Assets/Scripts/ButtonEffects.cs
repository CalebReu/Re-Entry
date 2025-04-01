using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonEffects : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickClip;

    void Start()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);

        foreach (var b in buttons)
        {
            UnityAction l = delegate { OnClick(); };
            b.onClick.AddListener(l);
        }
    }

    public void OnClick()
    {
        if (buttonClickClip != null)
            AudioManager.instance.PlaySound(buttonClickClip);
        else
            Debug.LogWarning("Button click sound not set in ButtonEffects script.");
    }
}
