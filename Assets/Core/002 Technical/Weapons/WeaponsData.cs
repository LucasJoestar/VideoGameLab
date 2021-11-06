using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons Data", menuName = "Weapons/Data", order = 101)]
public class WeaponsData : ScriptableObject
{
    [SerializeField] private ParticleSystem projectiles;
    public ParticleSystem Projectiles => projectiles;

}
