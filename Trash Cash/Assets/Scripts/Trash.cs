using UnityEngine;

public class Trash : MonoBehaviour
{
    private CircleCollider2D myCollider;
    private Animator myAnim;
    private float destructionTime = 1.5f;
    private float fadeOutTime = 4f;
    [SerializeField] private float health = 100;
    [SerializeField] private float cashForTrash = 0.5f;
    [SerializeField] private float invulnerableTime = 0.3f;

    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        cashForTrash = PlayerPrefs.GetFloat(GameManager.CASH_FOR_TRASH);
        health = PlayerPrefs.GetFloat(GameManager.TRASH_HEALTH);
        fadeOutTime = PlayerPrefs.GetFloat(GameManager.TRASH_FADE_DELAY);
        Invoke(nameof(FadeOutTrash), fadeOutTime);
    }


    private void FadeOutTrash()
    {
        myAnim.SetTrigger("isFading");
        Destroy(gameObject, destructionTime);
    }
    public void TakeDamage(float damage)
    {
        myAnim.SetBool("isTakingDamage", true);
        health -= damage;
        DisableAndEnableColliderWhenHit();
        if(health < 0)
        {
            TrashCan.Instance.cash += cashForTrash;
            TrashCan.Instance.UpdateCash();
            Destroy(gameObject);
        }
    }

    private void DisableAndEnableColliderWhenHit()
    {
        myCollider.enabled = false;
        Invoke(nameof(EnableColliderAfterInvulnerabilityTime), invulnerableTime);
    }

    private void EnableColliderAfterInvulnerabilityTime()
    {
        myAnim.SetBool("isTakingDamage", false);
        myCollider.enabled = true;
    }
}

