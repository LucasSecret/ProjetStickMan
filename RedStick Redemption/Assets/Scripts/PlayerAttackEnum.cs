using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEnum : MonoBehaviour
{
    // Start is called before the first frame update

    private enum PlayerAttack { kick, lowkick, punch, uppercut, flyingKick }
    public PlayerAttack PlayerAttackType { get; set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
