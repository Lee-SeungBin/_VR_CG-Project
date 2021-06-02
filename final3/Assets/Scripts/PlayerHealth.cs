using System.Collections;
using UnityEngine;
// UI 관련 코드

// 플레이어 캐릭터의 생명체로서의 동작을 담당
public class PlayerHealth : LivingEntity
{
    //public Slider healthSlider; // 체력을 표시할 UI 슬라이더

    //public AudioClip deathClip; // 사망 소리
    //public AudioClip hitClip; // 피격 소리
    //public AudioClip itemPickupClip; // 아이템 습득 소리

    //private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    private Animator playerAnimator; // 플레이어의 애니메이터

    private PlayerMovement playerMovement; // 플레이어 움직임 컴포넌트
    //private PlayerShooter playerShooter; // 플레이어 슈터 컴포넌트

    private bool[] buffAt = { false, false, false };
    private void Awake()
    {
        // 사용할 컴포넌트를 가져오기
        playerAnimator = GetComponent<Animator>();
        //playerAudioPlayer = GetComponent<AudioSource>();

        playerMovement = GetComponent<PlayerMovement>();
        //playerShooter = GetComponent<PlayerShooter>();

    }

    protected override void OnEnable()
    {
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();

        //healthSlider.gameObject.SetActive(true);
        //healthSlider.maxValue = startingHealth;
        //healthSlider.value = health;

        playerMovement.enabled = true;
        //playerShooter.enabled = true;
    }

    // 체력 회복
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity의 RestoreHealth() 실행 (체력 증가)
        base.RestoreHealth(newHealth);

        //healthSlider.value = health;
    }

    // 데미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // LivingEntity의 OnDamage() 실행(데미지 적용)
        if (!dead)
        {
            //playerAudioPlayer.PlayOneShot(hitClip);
        }
        base.OnDamage(damage, hitPoint, hitDirection);

        //healthSlider.value = health;
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Slow")
        {
            Debug.Log("Slowhit");
            if(Random.Range(1,100) <= 20)
            {
                TakeBuff("Slow");
            }
        }
        else if(coll.gameObject.tag == "Poison")
        {
            Debug.Log("Poisonhit");
            if (Random.Range(1, 100) <= 10)
            {
                TakeBuff("Poison");
            }
        }
        else if (coll.gameObject.tag == "MonsterHitBox")
        {
            Debug.Log("Hit");
        }
        else if (coll.gameObject.tag == "Critical")
        {
            if(Random.Range(1,100) <= 5)
            {
                Debug.Log("CriticalHit");
            }
            else
            {
                Debug.Log("Hit");
            }
        }
        else if (coll.gameObject.tag == "Skill")
        {
            Debug.Log("SkillHit");
            //Destroy(coll.gameObject);
        }
    }

    public void TakeBuff(string name)
    {
        switch (name)
        {
            case "Poison":
                if (!buffAt[0]) { StartCoroutine(OnBuffCoroutine("Subtraction", 10, 5, 0)); }
                break;
            case "Slow":
                if (!buffAt[1]) { StartCoroutine(OnBuffCoroutine("DeBuff", 5, 0, 1)); }
                break;
            case "Recovery":
                if (!buffAt[2]) { StartCoroutine(OnBuffCoroutine("Addition", 5, 5, 2)); }
                break;
        }
    }

    IEnumerator OnBuffCoroutine(string operation, int time, int damage, int index)
    {

        buffAt[index] = true;

        while (time > 0)
        {
            if (operation == "Addition")
            {
                //currentHealth += damage;
            }
            else if (operation == "Subtraction")
            {
                //currentHealth -= damage;
                Debug.Log("Poison1");
            }
            else if (operation == "DeBuff")
            {
                Debug.Log("Slow");
                playerMovement.moveSpeed = 2.5f;
            }
            time--;
            yield return new WaitForSeconds(1);
        }
        buffAt[index] = false;
        playerMovement.moveSpeed = 5f;

    }
    // 사망 처리
    public override void Die()
    {
        // LivingEntity의 Die() 실행(사망 적용)
        base.Die();

        //healthSlider.gameObject.SetActive(false);

        //playerAudioPlayer.PlayOneShot(deathClip);
        playerAnimator.SetTrigger("Die");

        playerMovement.enabled = false;
        //playerShooter.enabled = false;
    }
}