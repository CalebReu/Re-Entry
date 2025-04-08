using UnityEngine;

public class RandomAnimationOffset : MonoBehaviour
{
    Animator anim;
    float offset;
    void Start()
    {
        anim = GetComponent<Animator>();
        offset = Random.Range(0f, 1f);
        anim.Play("Background_idle",0,offset);
    }

}
