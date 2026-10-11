using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PrototypeFlowTests
{
    private static Type ScoreType => AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetType("score")).First(t => t != null);
    private static int Score(string field) => (int)ScoreType.GetField(field).GetValue(null);
    private static void SetScore(string field, int value) => ScoreType.GetField(field).SetValue(null, value);

    private static IEnumerator NewRun()
    {
        SetScore("lives", 3);
        SetScore("totalScore", 0);
        SceneManager.LoadScene("start");
        yield return null;
        var start = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).First(c => c != null && c.GetType().Name == "NextLevel");
        start.SendMessage("StartLevel");
        yield return new WaitForSeconds(0.2f);
        Assert.AreEqual("2dScene", SceneManager.GetActiveScene().name);
        Assert.NotNull(GameObject.FindGameObjectWithTag("Player"));
    }

    private static void TeleportTo(Collider2D target)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = target.bounds.center;
        Physics2D.SyncTransforms();
    }

    [UnityTest]
    public IEnumerator DiamondPickupIncreasesScore()
    {
        yield return NewRun();
        var player = GameObject.FindGameObjectWithTag("Player");
        var diamond = GameObject.FindGameObjectsWithTag("Elmas").OrderBy(d => Vector2.Distance(d.transform.position, player.transform.position)).First();
        TeleportTo(diamond.GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.2f);
        Assert.Greater(Score("totalScore"), 0);
        Assert.IsTrue(diamond == null, "Picked diamond must be removed.");
    }

    [UnityTest]
    public IEnumerator BothLevelExitsReachWinScene()
    {
        yield return NewRun();
        foreach (var expected in new[] { "2sScene3", "Finish" })
        {
            var exit = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).First(c => c != null && c.GetType().Name == "NextLevel");
            TeleportTo(exit.GetComponent<Collider2D>());
            yield return new WaitForSeconds(0.2f);
            Assert.AreEqual(expected, SceneManager.GetActiveScene().name);
        }
    }

    [UnityTest]
    public IEnumerator ThreeFallsReachGameOverAndRestartResetsRun()
    {
        yield return NewRun();
        for (int remaining = 2; remaining >= 0; remaining--)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, -1000, 0);
            yield return new WaitForSeconds(0.2f);
            Assert.AreEqual(remaining, Score("lives"));
        }
        Assert.AreEqual("gameover", SceneManager.GetActiveScene().name);
        yield return NewRun();
        Assert.AreEqual(3, Score("lives"));
        Assert.AreEqual(0, Score("totalScore"));
    }
}
