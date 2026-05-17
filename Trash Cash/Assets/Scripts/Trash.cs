using UnityEngine;

public class Trash : MonoBehaviour
{
    private CircleCollider2D myCollider;
    [SerializeField] private int health = 100;
    [SerializeField] private float cashForTrash = 0.5f;
    [SerializeField] private float invulnerableTime = 0.3f;

    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
    }
    public void TakeDamage(int damage)
    {
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
        myCollider.enabled = true;
    }
}

