using UnityEngine;
using UnityEngine.SceneManagement;

// Packaging support for the existing prototype; no time-shifting mechanic.
public sealed class PrototypeSession : MonoBehaviour
{
    private Transform player;
    private float fallLimit;
    private bool losingLife;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        score.totalScore = 0;
        score.lives = 3;
        var host = new GameObject("Prototype Session");
        DontDestroyOnLoad(host);
        host.AddComponent<PrototypeSession>();
    }

    private void OnEnable() { SceneManager.sceneLoaded += SceneLoaded; }
    private void OnDisable() { SceneManager.sceneLoaded -= SceneLoaded; }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        losingLife = false;
        if ((scene.buildIndex == 1 || scene.buildIndex == 2) && score.lives <= 0)
        {
            SceneManager.LoadScene(4);
            return;
        }
        var hero = GameObject.FindGameObjectWithTag("Player");
        player = hero != null ? hero.transform : null;
        fallLimit = -30f;
        if (player == null) return;
        float lowestGround = float.PositiveInfinity;
        foreach (var collider in FindObjectsByType<Collider2D>(FindObjectsSortMode.None))
        {
            if (collider.CompareTag("Zemin")) lowestGround = Mathf.Min(lowestGround, collider.bounds.min.y);
        }
        if (!float.IsPositiveInfinity(lowestGround)) fallLimit = lowestGround - 8f;
    }

    private static void Restart()
    {
        score.totalScore = 0;
        score.lives = 3;
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        if (Input.GetKeyDown(KeyCode.R) || ((scene == 0 || scene >= 3) && Input.GetKeyDown(KeyCode.Return)))
        {
            Restart();
            return;
        }
        if (player != null && player.position.y < fallLimit && !losingLife)
        {
            losingLife = true;
            score.lives--;
            SceneManager.LoadScene(score.lives <= 0 ? 4 : scene);
        }
    }

    private void OnGUI()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        var label = new GUIStyle(GUI.skin.label) { fontSize = Mathf.Max(14, Screen.height / 40), alignment = TextAnchor.MiddleCenter };
        if (scene == 1 || scene == 2)
        {
            GUI.Box(new Rect(0, Screen.height - 38, Screen.width, 38), "");
            GUI.Label(new Rect(0, Screen.height - 38, Screen.width, 38), "A/D or Arrows: Move    W/Space/Up: Jump    R: Restart    |    Lives: " + score.lives + "    Diamonds: " + score.totalScore, label);
        }
        else if (scene >= 3)
        {
            GUI.Label(new Rect(0, Screen.height * 0.58f, Screen.width, 40), "Diamonds collected: " + score.totalScore, label);
            if (GUI.Button(new Rect(Screen.width / 2f - 110, Screen.height * 0.7f, 220, 48), "Play again (Enter / R)")) Restart();
        }
    }
}
