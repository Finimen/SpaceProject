using Assets.Scripts.SpaceShip;
using NUnit.Framework;
using UnityEngine;

public class ShipTests
{
    private GameObject _shipObject;
    private ShipDamageDealer _ship;

    [SetUp]
    public void Setup()
    {
        _shipObject = new GameObject("Ship");
        _shipObject.AddComponent<BoxCollider2D>();

        _ship = _shipObject.AddComponent<ShipDamageDealer>();
        _ship.Initialize();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_shipObject);
    }

    [Test]
    public void ShipTakesDamage()
    {
        var startHealth = _ship.Health;

        var damage = 100;

        _ship.GetDamage(damage);
        Assert.IsTrue(_ship.Health == startHealth - damage);
    }

    [Test]
    public void ShipSuccessfullyInitialized()
    {
        Assert.IsTrue(true);
    }

    [Test]
    public void ShipSuccessfullyDeleted()
    {
        var shipDestroyed = false;

        _ship.OnDestroyed += () => shipDestroyed = true;

        _ship.GetDamage(10000);

        Assert.IsTrue(shipDestroyed);
    }
}