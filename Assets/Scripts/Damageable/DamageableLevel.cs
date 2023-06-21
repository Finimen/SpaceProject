using Assets.Scripts.Environment;
using System;
using UnityEngine;

namespace Assets.Scripts.Damageable
{
    [Serializable]
    public struct DamageableLevel
    {
        public float MinHealth;
        public float RandomSpawnOffset;
        public ObjectChaser EffectsTemplate;
    }
}