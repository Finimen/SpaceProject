namespace Assets.Scripts.Damageable
{
    internal interface IDamageable
    {
        public float Health { get; }
        public float MaxHealth { get; }

        public void GetDamage(float amount);
    }
}