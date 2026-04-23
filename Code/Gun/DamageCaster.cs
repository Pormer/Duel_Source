using System;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField] ContactFilter2D targetFilter;
    [SerializeField] private Vector2 castSize;

    public event Action<bool> OnShoot;

    private Collider2D[] cols;
    private void Awake()
    {
        cols = new Collider2D[1];
    }

    public void Initialize(int range)
    {
        castSize = new Vector2(2 * range + 1, 0.6f);
    }

    public void CastDamage(int damage)
    {
        var col = Physics2D.OverlapBox(transform.position, castSize, 0, targetFilter, cols);

        if (col > 0)
        {
            if (cols[0].TryGetComponent(out Player player))
            {
                print("Hit");
                player.GetCompo<Health>().TakeDamage(damage);
                
                OnShoot?.Invoke(true);
                return;
            }
        }
        
        OnShoot?.Invoke(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, castSize);
        Gizmos.color = Color.white;
    }
}
