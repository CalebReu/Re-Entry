using UnityEngine;

public class Player_Sprite_swapper : MonoBehaviour
{
    private SpriteRenderer sp;
    private enum shotType { SIMPLE, TRIPLE, SHOTGUN, GATTLING };
    [SerializeField] Sprite[] sprites;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        if (sp.sprite) { Debug.Log("Sprite renderer going strong!"); }

    }
    public void SwapSprites() {
        sp = GetComponent<SpriteRenderer>();
        switch ((shotType)SceneHandler.Instance.equipped) 
        {
            case shotType.SHOTGUN:
                sp.sprite = sprites[1];
                break;
            case shotType.SIMPLE:
                sp.sprite = sprites[0];
                break;
            case shotType.TRIPLE:
                sp.sprite = sprites[2];
                break;
            case shotType.GATTLING:
                sp.sprite = sprites[3];
                break;
            default: sp.sprite = sprites[0]; break;

        }
    }
}
