using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Collider coll;
    [SerializeField] private Animator animator;

    [Header("VFX")]
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private GameObject dieVfx;

    [Header("DEBUG")]
    [SerializeField] private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ApplyDamage(int damage, ContactPoint contactPoint)
    {
        currentHealth -= damage;
        PlayVfx(hitVfx, contactPoint.point);

        if (currentHealth > 0)
        {
            animator.SetTrigger(AnimationConstants.Hit);
            return;
        }
        
        Die();
        animator.applyRootMotion = true;
        animator.SetBool(AnimationConstants.IsDead, true);
    }

    private void Die()
    {
        PlayVfx(dieVfx, transform.position);
        AudioManager.Instance.PLaySfx(SfxType.Death);

        coll.enabled = false;
    }

    private void PlayVfx(GameObject vfx, Vector3 position)
    {
        if (vfx == null) return;

        Instantiate(vfx, position, Quaternion.identity);
    }
}
