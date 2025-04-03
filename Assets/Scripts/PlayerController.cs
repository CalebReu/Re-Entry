
using UnityEngine;
using System.Collections;

public class PlayerController : SingletonMonoBehavior<PlayerController>
{
    // delete this comment
    [SerializeField] private  float basemoveSpeed;
    [SerializeField] private float moveSpeed;
    private int shells = 5;
    // for bounding movement to within screen bounds only
    [SerializeField] private float leftBound, rightBound;
    private BoxCollider2D playerBoundingBox;
    private DisplayShells ShellsUI;

    // bullet stuff
    [SerializeField] private shotType equipped;
    public enum shotType { SIMPLE, TRIPLE, SHOTGUN, GATTLING };

    [SerializeField] private float fireRateMod = 1f;
    private float bulletSpeedMod = 1f;
    private float bulletSizeMod = 0.3f;
    private float damageMod = 1f;
    public float RPM = 1;
    private float firingTimer= 0;
    private bool reloading = false;
    private bool canFire = true;
    private Rigidbody2D rb;
    public GameObject simpleBullet;

    private void Start()
    { 
        InputManager.Instance.OnMove.AddListener(MovePlayer);
        InputManager.Instance.OnFire.AddListener(Fire);
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
           
        }
        else
        {
         
        }
        setupWeapon();
    }
    public void setupWeapon() {
        switch (equipped) {
            case shotType.SHOTGUN:
                ShellsUI.enable();
                ShellsUI.SetShells(shells); break;
            case shotType.SIMPLE: 
                    shells = 5; 
                ShellsUI.Disable(); 
                moveSpeed = basemoveSpeed * 1.5f; break;
            case shotType.TRIPLE:
                shells = 5;
                ShellsUI.Disable();
                moveSpeed = basemoveSpeed * 1.1f; break;
            case shotType.GATTLING:
                shells = 5;
                ShellsUI.Disable();
                moveSpeed = basemoveSpeed * 0.8f; break;


        }
    
    }
    IEnumerator reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime / fireRateMod);
        canFire = true;
    }
    IEnumerator Shellreload(float reloadTime) // for reloading shotgun shells 1 round at a time. Yay Recursion!
    {    
            yield return new WaitForSeconds(reloadTime / fireRateMod);
        if (shells < 5)
        {
            shells++;
            ShellsUI.SetShells(shells);
            
            if (shells == 5)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.PumpActionClip);
                reloading = false;
                canFire = true;
                StopAllCoroutines();
            }
            else
            {
                AudioManager.instance.PlaySound(AudioManager.instance.LoadShellClip);
            }

        }
        if (shells < 5)
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
                bulletScript.SetSize(1.2f * bulletSizeMod);
                bulletScript.SetDamage(2f * damageMod);
                CameraShake.Instance.TriggerShake(0.25f, 0.1f);
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
                    bulletScript.SetSize(0.8f * bulletSizeMod);
                    bulletScript.SetDamage(0.8f * damageMod);
                }
                CameraShake.Instance.TriggerShake(0.15f, 0.05f);
                AudioManager.instance.PlaySound(AudioManager.instance.playerTripleShotClip);
                canFire = false;
                StartCoroutine(reload(1.5f));
                break;
            case shotType.SHOTGUN:
                
                for (int i = 0; i < 3; i++)
                {
                    newBullet = Instantiate(simpleBullet, transform.position, transform.rotation);
                    bulletScript = newBullet.GetComponent<SimpleBullet>();
                    float spread = 23;
                    float angle = Random.Range(-spread, spread);
                    bulletScript.setAngle(angle);
                    bulletScript.SetSpeed(bulletSpeedMod);
                    bulletScript.SetSize(0.2f * bulletSizeMod);
                    bulletScript.SetDamage(0.2f * damageMod);
                }
                shells--;
                CameraShake.Instance.TriggerShake(0.15f,0.05f);
                AudioManager.instance.PlaySound(AudioManager.instance.playerShotgunClip);
                canFire = false;
                if (shells > 0)
                {
                    StartCoroutine(reload(0.08f));

                }
              
                if (!reloading) 
                {
                    reloading = true;
                     StartCoroutine(Shellreload(0.3f)); 
                } 
               
                    
               
                ShellsUI.SetShells(shells);
                
                break;
            case shotType.GATTLING:
                firingTimer = 1;
                newBullet = Instantiate(simpleBullet, transform.position, transform.rotation);
                bulletScript = newBullet.GetComponent<SimpleBullet>();
                bulletScript.SetSpeed(bulletSpeedMod);
                float spread2 = RPM *4f;
                float angle2 = Random.Range(-spread2, spread2);
                bulletScript.setAngle(angle2);
                bulletScript.SetSize(0.1f * bulletSizeMod);
                bulletScript.SetDamage(0.2f * damageMod);
                CameraShake.Instance.TriggerShake(0.15f, 0.02f);
                AudioManager.instance.PlaySound(AudioManager.instance.playerShootClip);
                canFire = false;
                if (RPM < 2) { RPM += 0.08f; }
             
                float reloadTime = Mathf.Max(0.08f, 1.5f - RPM);
                StartCoroutine(reload(reloadTime));
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
    public void UpdateRPM()
    {
        if (firingTimer > 0)
        {
            firingTimer -= Time.deltaTime;
        }
        else { firingTimer = 0; }
       

        if (firingTimer > 0.5f)
        {

            moveSpeed = Mathf.Max((basemoveSpeed*0.8f) - RPM, 0.5f);
        }
        else
        {
            if (RPM > 1)
            {
                RPM -= 1f * Time.deltaTime;
            }
            else { RPM = 1; }
            moveSpeed = basemoveSpeed * 0.8f;
        }
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
