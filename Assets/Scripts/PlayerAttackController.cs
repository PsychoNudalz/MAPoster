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
        
    }
    public void AddAttack(AttackSet attackSet)
    {
        attackSets.Add(attackSet);
        attackSet.GetParent().parent = transform;
        attackSet.GetParent().transform.position = transform.position;
    }
}
