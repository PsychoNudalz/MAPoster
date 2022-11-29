using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField]
    private List<AttackSet> attackSets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackSets.Count > 0)
        {
            UpdateAttackSets();
        }
        
    }
    public void AddAttack(AttackSet attackSet)
    {
        if (!attackSets.Contains(attackSet))
        {
            attackSets.Add(attackSet);
            attackSet.GetParent().parent = transform;
            attackSet.GetParent().transform.position = transform.position;
        }
    }

    public void UpdateAttackSets()
    {
        for (int i = 0; i < attackSets.Count; i++)
        {
            if (attackSets.Count == 0)
            {
                return;
            }
            if (attackSets[i].ExceededDuration())
            {
                Destroy(attackSets[i].gameObject);
                attackSets.RemoveAt(i);
                i--;
            }
        }
    }
}
