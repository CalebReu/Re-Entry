using System.Collections;
using UnityEngine;

public class Flash_Sprite : SingletonMonoBehavior<Flash_Sprite>
{
    private SpriteRenderer sprite;
    private bool flashing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void startFlash() {
        flashing = true;
        StartCoroutine(Flash());
    }
    public void stopFlash()
    {
        flashing = false;
        StopCoroutine(Flash());
        sprite.enabled = true;
    }

    public void flashForDuration(float d) { 
        flashing = true;
        StartCoroutine(Flash(d));
    }

    private IEnumerator Flash(float duration) {
        float gap = 0.05f;
        float totalTime = 0;
        while (flashing) {
            sprite.enabled = !sprite.enabled;
            gap *= 1.25f;
            totalTime += gap;
            if (totalTime >= duration) {
                flashing = false;
                sprite.enabled = true;
                StopCoroutine(Flash(duration));
            }
            yield return new WaitForSeconds(gap);
        }
    }
    private IEnumerator Flash()
    {
        while (flashing)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.03f);
        }

    }
}
