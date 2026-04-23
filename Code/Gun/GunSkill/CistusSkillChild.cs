using System;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class CistusSkillChild : PoolableMono
{
    [SerializeField] ContactFilter2D filter;
    private CistusSkill skill;
    private Collider2D[] cols;
    
    
    public void Initialize(CistusSkill owner)
    {
        skill = owner;
        filter.layerMask = LayerMask.GetMask("Player");
        filter.useLayerMask = true;
        
        cols = new Collider2D[1];
        
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);
        
        transform.position += new Vector3(x, y, 0);
    }

    private void Update()
    {
        int col = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0f, filter, cols);

        skill.IsFire = col > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f, 0.5f, 1));
    }

    public override void ResetItem()
    {
        
    }
}
