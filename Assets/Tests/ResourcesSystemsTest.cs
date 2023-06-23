using Assets.Scripts.ResourcesSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts;
using NUnit.Framework;

public class ResourcesSystemsTest : MonoBehaviour
{
    [SetUp]
    public void Setup()
    {
        World.Initialize();
    }

    [UnityTest]
    public IEnumerator OreIsCollectedByCollector()
    {
        var isCollected = false;

        var oreObject = new GameObject("Ore");
        var ore = oreObject.AddComponent<Ore>();
        ore.GetComponent<IInitializable>().Initialize();
        
        ore.OnOreCollected += () => isCollected = true;

        ore.StartCollecting(-1);

        yield return null;

        Assert.IsTrue(isCollected);
    }
}