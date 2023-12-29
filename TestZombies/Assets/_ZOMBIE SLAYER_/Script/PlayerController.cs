using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.Events;
using Spine.Unity;

public enum GunHandlerState { AVAILABLE, SWAPPING, RELOADING, EMPTY }
public enum ShootingMethob { SingleShoot, AutoShoot}
public enum WEAPON_STATE { MELEE, GUN}
[RequireComponent(typeof(PlayerSpineHelper))]
public class PlayerController : MonoBehaviour, ICanTakeDamage
{
    public Text testText;
    [Header("SPINE ANIMATION")]
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset runAnim;
    public AnimationReferenceAsset idleAnim;
    public AnimationReferenceAsset shootAnim;
    public string currentPlayerState;
    public string currentAnim;


    [Header("SET UP")]

    public Animator anim;
    public float moveSpeed = 10;
    public float limitAbovePos = -0.8f;
    public float limitBelowPos = -3.65f;
    [ReadOnly] public float limitLeft;
    [ReadOnly] public float limitRight;
    public AudioClip soundHurt, soundDie;

    [Header("BLOCK LAYER")]
    public LayerMask blockWayLayerMask;

    [Header("HEALTH")]
    [Range(0, 5000)]
    public int health = 100;
    //public Vector2 healthBarOffset = new Vector2(0, 1.5f);
    public Slider healthbarSlider;

    float currentHealth;

    [Header("GRENADE")]
    public GameObject grenade;
    public Transform throwPoint;

    [Header("WEAPONS")]
    [ReadOnly] public WEAPON_STATE weaponState;
    [ReadOnly] public GunHandlerState GunState;
    //[ReadOnly] public GunTypeID gunTypeID;
    [SerializeField] private PlayerLoadout playerLoadout;
    [ReadOnly] public CustomWeapon currentWeapon;
    private int currentFirstWeaponIdx = 0;
    private int currentSecondWeaponIdx = 0;

    public LayerMask targetLayer;
    float lastTimeShooting = -999;
    public AudioClip throwGrenadeSound;
    bool allowShooting = true;
    //protected HealthBarEnemyNew healthBar;
    PlayerSpineHelper playerSpineHelper;
    public bool isFacingRight { get { return transform.rotation.eulerAngles.y == 0; } }

    public static UnityEvent<CustomWeapon> onWeaponChanged = new UnityEvent<CustomWeapon>();


    private void Awake()
    {
        CollectGunItem.onWeaponCollected.AddListener(SetGun);
    }
    private void OnDestroy()
    {
        CollectGunItem.onWeaponCollected.RemoveListener(SetGun);
    }

    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        GameManager.Instance.Player = this;

        if (playerLoadout.firstWeapons.Count != 0 /*&& playerLoadout.firstWeaponIdx <= playerLoadout.firstWeapons.Count*/)
        {
            //SetGun(playerLoadout.firstWeapons[playerLoadout.firstWeaponIdx]);
            //currentFirstWeaponIdx = playerLoadout.firstWeaponIdx;
            //now we have only one weapon of each type
            SetGun(playerLoadout.firstWeapons[0]);
            currentFirstWeaponIdx = 0;
        }
        else if (playerLoadout.secondWeapons.Count != 0 /*&& playerLoadout.secondWeaponIdx <= playerLoadout.secondWeapons.Count*/)
        {
            //SetGun(playerLoadout.secondWeapons[playerLoadout.secondWeaponIdx]);
            //currentSecondWeaponIdx = playerLoadout.secondWeaponIdx;
            //now we have only one weapon of each type
            SetGun(playerLoadout.secondWeapons[0]);
            currentSecondWeaponIdx = 0;
        }
        else
            Debug.Log("We don't have any weapons in playerLoadout or incrorrect indexes");

        //GunManager.Instance.ResetGunBullet();
        ResetAllBullets();


        playerSpineHelper = GetComponent<PlayerSpineHelper>();
        playerMeleeWeapon = GetComponent<PlayerMeleeWeapon>();

        currentHealth = health;
        //var healthBarObj = (HealthBarEnemyNew)Resources.Load("HealthBar", typeof(HealthBarEnemyNew));
        //healthBar = (HealthBarEnemyNew)Instantiate(healthBarObj, healthBarOffset, Quaternion.identity);

        //healthBar.Init(transform, (Vector3)healthBarOffset);
        healthbarSlider.maxValue = health;
        healthbarSlider.value = currentHealth;

    }

    private void OnEnable()
    {
        if (GameManager.Instance)
            GameManager.Instance.Player = this;

    }
    private void OnDisable()
    {
    }

    public Vector2 finalSpeed;

    private void ResetAllBullets() 
    {
        if(playerLoadout.firstWeapons.Count != 0)
            foreach (var item in playerLoadout.firstWeapons)
            {
                item.ResetBullet();
            }
        if (playerLoadout.secondWeapons.Count != 0)
            foreach (var item in playerLoadout.secondWeapons)
            {
                item.ResetBullet();
            }
    }

    public void GetLimitHorizontal()
    {
        limitLeft = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 0.5f;
        limitRight = Camera.main.ViewportToWorldPoint(Vector3.right).x - 0.5f;
    }

    void Update()
    {
        //Vector2 testInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        //testText.text = testInput.x.ToString() + testInput.y.ToString();
        testText.text = "1 = "+isWayBlocked().ToString()+"\n";

        if (GameManager.Instance.State != GameManager.GameState.Playing)
            return;
        testText.text += "2 = " + isWayBlocked().ToString() + "\n";
        GetInput();
        testText.text += "3 = " + isWayBlocked().ToString() + "\n";
        finalSpeed = input * moveSpeed * Time.deltaTime;
        testText.text += "4 = " + isWayBlocked().ToString() + "\n";
        GetLimitHorizontal();
        testText.text += "5 = " + isWayBlocked().ToString() + "\n";
        if (finalSpeed.x > 0 && transform.position.x >= limitRight)
        {
            testText.text += "6 = " + isWayBlocked().ToString() + "\n";
            finalSpeed.x = 0;
        }
        else if (finalSpeed.x < 0 && transform.position.x <= limitLeft)
        {
            testText.text += "7 = " + isWayBlocked().ToString() + "\n";
            finalSpeed.x = 0;
        }
        testText.text += "8 = " + isWayBlocked().ToString() + "\n";
        if ((finalSpeed.x > 0 && !isFacingRight) || (finalSpeed.x < 0 && isFacingRight))
            Flip();


        testText.text += "9 = " + isWayBlocked().ToString() + "\n";
        //testText.text = isWayBlocked().ToString();
        if (!isWayBlocked())
        {
            testText.text += "10 = " + isWayBlocked().ToString() + "\n";
            transform.Translate(finalSpeed, Space.World);
            //testText.text = input.x.ToString() + input.y.ToString();

            if (transform.position.y > limitAbovePos)
            {
                transform.position = new Vector3(transform.position.x, limitAbovePos, transform.position.z);
            }else if (transform.position.y < limitBelowPos)
            {
                transform.position = new Vector3(transform.position.x, limitBelowPos, transform.position.z);
            }
        }

        //healthBar.transform.localScale = new Vector2(transform.localScale.x > 0 ? Mathf.Abs(healthBar.transform.localScale.x) : -Mathf.Abs(healthBar.transform.localScale.x), healthBar.transform.localScale.y);
        //healthbarSlider.value = currentHealth;

        //old working move animation
        //AnimSetFloat("speed", input.magnitude);

        //new Spine move animation

        if (input.magnitude > 0.1)
        {
            SetPlayerState("Run");
        }
        else if (input.magnitude <= 0.1)
        {
            SetPlayerState("Idle");
        }
        
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnim))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnim = animation.name;
    }

    public void SetPlayerState(string state)
    {
        if (state.Equals("Idle"))
        {
            SetAnimation(idleAnim, true, 1f);
        }
        else if (state.Equals("Run"))
        {
            SetAnimation(runAnim, true, 1f);
        }
        else if (state.Equals("Shoot"))
        {
            SetAnimation(shootAnim, true, 1f);
        }
    }

    bool isWayBlocked()
    {
        return Physics2D.Raycast(transform.position, input, 0.2f, blockWayLayerMask);
    }

    Vector2 input;
    void GetInput()
    {
        //Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        //testText.text += "11 = " + isWayBlocked().ToString() + "\n";
        //input = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        //input = joystick.GetJoystickMovement();
        //testText.text = input.x.ToString() + input.y.ToString();
        input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") + Input.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical") + Input.GetAxis("Vertical"));
        //testText.text += "12 = " + isWayBlocked().ToString() + "\n";
        //testText.text += gunTypeID.ToString() + "\n";
        //testText.text += gunTypeID.shootingMethob.ToString() + "\n";

        if (currentWeapon.shootingMethob == ShootingMethob.SingleShoot)
        {
            testText.text += "13 = " + isWayBlocked().ToString() + "\n";
            //if (playerInput.PlayerMain.Shoot.triggered)
            if (CrossPlatformInputManager.GetButtonDown("Shoot"))
            {
                Shoot();
            }
        }
        //testText.text += "14 = " + isWayBlocked().ToString() + "\n";
        else if (currentWeapon.shootingMethob == ShootingMethob.AutoShoot)
        {
            if (CrossPlatformInputManager.GetButton("Shoot"))
            {
                Shoot();
            }
        }

        //we try to make auto melee attack when enemy is close eneught
        //if (CrossPlatformInputManager.GetButtonDown("Melee"))
        //{
        //    MeleeAttack();
        //}
    }

    void Flip()
    {
        transform.rotation = Quaternion.Euler(0, isFacingRight ? 180 : 0, 0);
    }

    public void AnimSetTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    public void AnimSetSpeed(float value)
    {
        if (anim)
            anim.speed = value;
    }

    public void AnimSetFloat(string name, float value)
    {
        anim.SetFloat(name, value);
    }

    public void AnimSetBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    public void SetState(GunHandlerState state)
    {
        GunState = state;
    }

    private void SetAvailabeAfterSwap()
    {

        SetState(GunHandlerState.AVAILABLE);
        Debug.Log("SetAvailabeAfterSwap");
        CheckBulletRemain();
    }

    public void Shoot()
    {
        RaycastHit2D hit = Physics2D.CircleCast(playerMeleeWeapon.checkPoint.position, playerMeleeWeapon.radiusCheck, Vector2.zero, 0, targetLayer);

        if (hit)
        {
            MeleeAttack();
            return;
        }

        //if (weaponState == WEAPON_STATE.MELEE)
        //{
        //    //SetGun(GunManager.Instance.getGunID());
        //    return;
        //}

        if (!allowShooting || currentWeapon.bullet <= 0)
            return;

        if (Time.time < (lastTimeShooting + currentWeapon.rate))
            return;

        if (GunState != GunHandlerState.AVAILABLE)
            return;

        lastTimeShooting = Time.time;
        currentWeapon.bullet--;
        AnimSetTrigger("shoot");
        for (int i = 0; i < currentWeapon.maxBulletPerShoot; i++)
        {
            StartCoroutine(FireCo());
        }

        if (currentWeapon.shellFX)
        {
            Vector2 shellPos = currentWeapon.shellPoint.position;
            var _tempFX = SpawnSystemHelper.GetNextObject(currentWeapon.shellFX, true);
            _tempFX.transform.position = shellPos;
        }

        SoundManager.PlaySfx(currentWeapon.soundFire, currentWeapon.soundFireVolume);
        SoundManager.PlaySfx(currentWeapon.shellSound, currentWeapon.shellSoundVolume);

        CancelInvoke("CheckBulletRemain");
        Invoke("CheckBulletRemain", currentWeapon.rate);

        if (currentWeapon.dualShot)
            Invoke("ShootSecondGun", currentWeapon.fireSecondGunDelay);
    }

    void ShootSecondGun()
    {
        //gunTypeID.bullet--;
        //SubtractBullet(1);
        for (int i = 0; i < currentWeapon.maxBulletPerShoot; i++)
        {
            StartCoroutine(FireCo());
        }
        SoundManager.PlaySfx(currentWeapon.soundFire, currentWeapon.soundFireVolume);
        SoundManager.PlaySfx(currentWeapon.shellSound, currentWeapon.shellSoundVolume);
    }

    public IEnumerator FireCo()
    {
        
        yield return null;

        var _dir = (isFacingRight ? Vector2.right : Vector2.left) + new Vector2(0, Random.Range(-(1f - currentWeapon.accuracy), (1f - currentWeapon.accuracy)));
        RaycastHit2D hit = Physics2D.Raycast(playerSpineHelper.GetFireWorldPoint() + (isFacingRight ? Vector2.left : Vector2.right), _dir, 100, targetLayer);

        if (currentWeapon.muzzleTracerFX)
        {
            var _tempFX = SpawnSystemHelper.GetNextObject(currentWeapon.muzzleTracerFX, true);
            _tempFX.transform.position = playerSpineHelper.GetFireWorldPoint();
            _tempFX.transform.right = _dir;
        }

        if (currentWeapon.muzzleFX)
        {
            var _muzzle = SpawnSystemHelper.GetNextObject(currentWeapon.muzzleFX, playerSpineHelper.GetFireWorldPoint(), true);

            _muzzle.transform.right = (isFacingRight ? Vector2.right : Vector2.left);
            //_muzzle.transform.parent = firePoint;
        }

        if (hit)
        {
            var takeDamage = (ICanTakeDamage)hit.collider.gameObject.GetComponent(typeof(ICanTakeDamage));
            if (takeDamage != null)
            {
                var finalDamage = (int)(Random.Range(currentWeapon.minPercentAffect * 0.01f, 1f) * currentWeapon.UpgradeRangeDamage);

                takeDamage.TakeDamage(finalDamage, Vector2.zero, hit.point, gameObject);
            }
        }

        if (currentWeapon.reloadPerShoot)
        {
            StartCoroutine(ReloadGunSub());
        }
    }

    void CheckBulletRemain()
    {
        if (currentWeapon.bullet <= 0)
        {
            GunManager.Instance.NextGun();
        }
    }

    public void ReloadGun()
    {
        SetState(GunHandlerState.RELOADING);
        //SoundManager.PlaySfx (soundReload, soundReloadVolume);
        AnimSetTrigger("reload");
        AnimSetBool("reloading", true);
        Invoke("ReloadComplete", currentWeapon.reloadTime);

        SoundManager.PlaySfx(currentWeapon.reloadSound, currentWeapon.reloadSoundVolume);
    }

    IEnumerator ReloadGunSub()
    {
        SetState(GunHandlerState.RELOADING);
        AnimSetBool("isReloadPerShootNeeded", true);

        yield return new WaitForSeconds(currentWeapon.reloadTime);

        SetState(GunHandlerState.AVAILABLE);
        AnimSetBool("isReloadPerShootNeeded", false);
    }

    public void ReloadComplete()
    {
        lastTimeShooting = Time.time;
        AnimSetBool("reloading", false);
        SetState(GunHandlerState.AVAILABLE);
    }

    public void ThrowGrenade()
    {
        var obj = (GameObject)SpawnSystemHelper.GetNextObject(grenade, false);
        SoundManager.PlaySfx(throwGrenadeSound);
        obj.GetComponent<Grenade>().SetDirection(isFacingRight);
        obj.transform.position = throwPoint.position;
        obj.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isCollectItem = (ICanCollect)collision.GetComponent(typeof(ICanCollect));
        if (isCollectItem != null)
        {
            isCollectItem.Collect();
        }
    }

    public void SetGun(CustomWeapon weapon/*GunTypeID gunID*/)
    {
        Debug.Log("SetGun");
        weaponState = WEAPON_STATE.GUN;
        anim.runtimeAnimatorController = weapon.animatorOverride;
        currentWeapon = weapon;
        AnimSetTrigger("swap-gun");
        allowShooting = false;
        SoundManager.PlaySfx(SoundManager.Instance.swapGun);
        Invoke("AllowShooting", 0.3f);
        
        onWeaponChanged.Invoke(weapon);
    }

    public void NextWeapon()
    {
        if(currentWeapon.weaponType == CustomWeapon.WEAPON_TYPE.first)
        {
            if (playerLoadout.secondWeapons.Count != 0 && currentSecondWeaponIdx + 1 <= playerLoadout.secondWeapons.Count - 1)
            {
                currentSecondWeaponIdx++;
                SetGun(playerLoadout.secondWeapons[currentSecondWeaponIdx]);
            }
            else if (playerLoadout.secondWeapons.Count != 0)
            {
                SetGun(playerLoadout.secondWeapons[0]);
                currentSecondWeaponIdx = 0;
            }
        }
        else if (currentWeapon.weaponType == CustomWeapon.WEAPON_TYPE.second)
        {
            if (playerLoadout.firstWeapons.Count != 0 &&  currentFirstWeaponIdx + 1 <= playerLoadout.firstWeapons.Count - 1)
            {
                currentFirstWeaponIdx++;
                SetGun(playerLoadout.firstWeapons[currentFirstWeaponIdx]);
            }
            else if (playerLoadout.firstWeapons.Count != 0)
            {
                SetGun(playerLoadout.firstWeapons[0]);
                currentSecondWeaponIdx = 0;
            }
        }
    }

    void AllowShooting()
    {
        allowShooting = true;
    }

    #region MELEE ATTACK
    PlayerMeleeWeapon playerMeleeWeapon;

    public void SetMelee()
    {
        weaponState = WEAPON_STATE.MELEE;
        anim.runtimeAnimatorController = playerMeleeWeapon.animatorOverride;
        SoundManager.PlaySfx(playerMeleeWeapon.soundSwap);
        AnimSetTrigger("swap-gun");
    }

    public void MeleeAttack()
    {
        if (weaponState == WEAPON_STATE.GUN)
        {
            SetMelee();
            return;
        }
        if (Time.time > (playerMeleeWeapon.lastAttackTime + playerMeleeWeapon.rate))
        {
            playerMeleeWeapon.lastAttackTime = Time.time;
            AnimSetTrigger("melee-attack");
            
            Invoke("MeleeCheckEnemy", playerMeleeWeapon.delayToSync);
        }
    }

    void MeleeCheckEnemy()
    {
        SoundManager.PlaySfx(playerMeleeWeapon.soundAttack);
        RaycastHit2D hit = Physics2D.CircleCast(playerMeleeWeapon.checkPoint.position, playerMeleeWeapon.radiusCheck, Vector2.zero, 0, targetLayer);

        if (hit)
        {
            var takeDamage = (ICanTakeDamage)hit.collider.gameObject.GetComponent(typeof(ICanTakeDamage));
            if (takeDamage != null)
            {
                var finalDamage = (int)(Random.Range(0.8f, 1f) * playerMeleeWeapon.damage);

                takeDamage.TakeDamage(finalDamage, new Vector2(5,0), hit.point, gameObject);
            }
        }
    }

    public void TakeDamage(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null, WEAPON_EFFECT forceEffect = WEAPON_EFFECT.NONE)
    {
        currentHealth -= damage;
        //if (healthBar)
        //    healthBar.UpdateValue(currentHealth / (float)health);
        healthbarSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
            AnimSetTrigger("dead");
            SoundManager.PlaySfx(soundDie);
        }
        else
        {
            AnimSetTrigger("hurt");
            SoundManager.PlaySfx(soundHurt);
        }
    }
    #endregion

    public void AddHearth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, health);
        //if (healthBar)
        //    healthBar.UpdateValue(currentHealth / (float)health);
        healthbarSlider.value = currentHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, limitAbovePos, 0));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, limitBelowPos, 0));
    }
}