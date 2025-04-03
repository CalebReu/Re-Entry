
using UnityEngine;
using System.Collections;

public class PlayerController : SingletonMonoBehavior<PlayerController>
{
    // delete this comment
    [SerializeField] private float moveSpeed;
    private int shells = 5;
    // for bounding movement to within screen bounds only
    [SerializeField] private float leftBound, rightBound;
    private BoxCollider2D playerBoundingBox;
    private DisplayShells ShellsUI;

    // bullet stuff
    [SerializeField] private shotType equipped;
    public enum shotType { SIMPLE, TRIPLE, SHOTGUN };

    [SerializeField] private float fireRateMod = 1f;
    private float bulletSpeedMod = 1f;
    private float bulletSizeMod = 0.3f;
    private float damageMod = 1f;
    private bool reloading = false;
    private bool canFire = true;
    private Rigidbody2D rb;
    public GameObject simpleBullet;

    private void Start()
    {   
        bulletSpeedMod = SceneHandler.Instance.bulletSpeedMod;
        bulletSizeMod = SceneHandler.Instance.bulletSizeMod;
        damageMod = SceneHandler.Instance.damageMod;
        fireRateMod = SceneHandler.Instance.fireRateMod;
        equipped = (shotType)SceneHandler.Instance.equipped;
        rb = GetComponent<Rigidbody2D>();
        ShellsUI = GetComponentInChildren < DisplayShells >();
        
        playerBoundingBox = GetComponent<BoxCollider2D>();
        if (equipped == shotType.SHOTGUN)
        {ShellsUI.enable(); }
        else { ShellsUI.Disable(); }
        if (Camera.main != null)
        {
            rightBound = Camera.main.orthographicSize * Camera.main.aspect;
            leftBound = -rightBound;
        }
        if (equipped == shotType.SHOTGUN)
        {
            ShellsUI.enable();
            ShellsUI.SetShells(shells);
        }
        else
        {
            shells = 5;
            ShellsUI.Disable();
        }
    }

    IEnumerator reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime / fireRateMod);
        canFire = true;
    }
    IEnumerator Shellreload(float reloadTime) // for reloading shotgun shells 1 round at a time. Yay Recursion!
    {
        Debug.Log("There are " + shells + " remaing shells");
        if (shells == 0)
        {   
            Debug.Log("long reload because you are greedy!");
            yield return new WaitForSeconds(reloadTime * 2f / fireRateMod); //as it stands, this code won't run, and that makes me angry, but I don't think it can be helped. it's late and I want to sleep.
            
        }
        else
        {
            yield return new WaitForSeconds(reloadTime / fireRateMod);
        }


        if (shells < 5)
        {
            shells++;
            ShellsUI.SetShells(shells);
            canFire = true;
            if (shells == 5)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.PumpActionClip);
                reloading = false;
                StopAllCoroutines();
            }
            else
            {
                AudioManager.instance.PlaySound(AudioManager.instance.LoadShellClip);
            }

        }
        if (shells < 4)
        {
            StartCoroutine(Shellreload(reloadTime));

        }
        else { StopAllCoroutines(); }
    }
    private void OnEnable()
    {
        InputManager.Instance.OnMove.AddListener(MovePlayer);
        InputManager.Instance.OnFire.AddListener(Fire);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnMove.RemoveListener(MovePlayer);
        InputManager.Instance.OnFire.RemoveListener(Fire);
    }

    private void Fire()
    {   
        // Returns if can't fire
        if (!canFire) return;

        GameObject newBullet;
        SimpleBullet bulletScript;
        // Checks what is equipped
        switch (equipped)
        {
            
            case shotType.SIMPLE:
            
                newBullet = Instantiate(simpleBullet, transform.position, transform.rotation);
                bulletScript = newBullet.GetComponent<SimpleBullet>();
                bulletScript.SetSpeed(bulletSpeedMod);
                bulletScript.SetSize(1 * bulletSizeMod);
                bulletScript.SetDamage(1 * damageMod);
                AudioManager.instance.PlaySound(AudioManager.instance.playerShootClip);
                canFire = false;
                StartCoroutine(reload(1f));
                break;

            case shotType.TRIPLE:
            
                for (int i = 0; i < 3; i++)
                {
                    newBullet = Instantiate(simpleBullet, transform.position, transform.rotation);
                    bulletScript = newBullet.GetComponent<SimpleBullet>();
                    float angle = -30 + (i * 30);
                    bulletScript.setAngle(angle);
                    bulletScript.SetSpeed(bulletSpeedMod);
                    bulletScript.SetSize(1 * bulletSizeMod);
                    bulletScript.SetDamage(1 * damageMod);
                }
                AudioManager.instance.PlaySound(AudioManager.instance.playerTripleShotClip);
                canFire = false;
                StartCoroutine(reload(1.5f));
                break;
            case shotType.SHOTGUN:
                
                for (int i = 0; i < 5; i++)
                {
                    newBullet = Instantiate(simpleBullet, transform.position, transform.rotation);
                    bulletScript = newBullet.GetComponent<SimpleBullet>();
                    float spread = 27;
                    float angle = Random.Range(-spread, spread);
                    bulletScript.setAngle(angle);
                    bulletScript.SetSpeed(bulletSpeedMod);
                    bulletScript.SetSize(0.2f * bulletSizeMod);
                    bulletScript.SetDamage(0.1f * damageMod);
                }
                shells--;
                CameraShake.Instance.TriggerShake(0.15f,0.05f);
                AudioManager.instance.PlaySound(AudioManager.instance.playerShotgunClip);
                canFire = false;
                if (shells > 0)
                {
                    StartCoroutine(reload(0.15f));

                }
              
                if (!reloading) 
                {
                    reloading = true;
                     StartCoroutine(Shellreload(0.7f)); 
                } 
               
                    
               
                ShellsUI.SetShells(shells);
                
                break;
        }
    }

    private void MovePlayer(Vector2 moveDirection)
    {
        stayWithinBounds();
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    public void SetWeapon(shotType newWeapon)
    {
        equipped = newWeapon;
        if(newWeapon == shotType.SHOTGUN){
            ShellsUI.enable();
            ShellsUI.SetShells(shells);
        }else{
            shells =5;
            ShellsUI.Disable();
        }
    }

    public void SetFireRate(float newMod)
    {
        fireRateMod = newMod;
    }

    public void SetBulletSizeMod(float newMod)
    {
        bulletSizeMod = newMod;
    }

    public void SetBulletSpeedMod(float newMod)
    {
        bulletSpeedMod = newMod;
    }

    public void SetDamageMod(float newMod)
    {
        damageMod = newMod;
    }

    // helper fn for MovePlayer
    // checks if player is at left or right screen edge, and does not let them go past it
    private void stayWithinBounds()
    {
        float playerWidth = playerBoundingBox.size.x;
        Vector3 pos = transform.position;

        if (pos.x <= (leftBound + playerWidth / 2))
        {
            pos.x = leftBound + playerWidth / 2;
        }

        if (pos.x >= (rightBound - playerWidth / 2))
        {
            pos.x = rightBound - playerWidth / 2;
        }

        transform.position = pos;
    }


}
