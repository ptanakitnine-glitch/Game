using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// Press 1-5 to choose a tower, then RIGHT-CLICK to spawn a square at the mouse.
// All towers are the same size, only the color is different:
//   1 = "tower"  red      2 = "tower2" orange   3 = "tower3" yellow
//   4 = "tower4" green    5 = "tower5" blue
// Works with the new Input System (Unity 6 default) and the old Input Manager.
public class TowerSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TowerType
    {
        public string name = "tower";
        [Tooltip("Diameter in world units")]
        public float size = 1f;
        public Color color = Color.white;
        [Tooltip("Optional: use your own prefab instead of the plain square")]
        public GameObject prefab;
    }

    public TowerType[] towers =
    {
        new TowerType { name = "tower",  size = 1f,    color = new Color(1f, 0.35f, 0.35f) },
        new TowerType { name = "tower2", size = 1f,    color = new Color(1f, 0.7f, 0.25f) },
        new TowerType { name = "tower3", size = 1f,    color = new Color(1f, 1f, 0.35f) },
        new TowerType { name = "tower4", size = 1f,    color = new Color(0.4f, 1f, 0.45f) },
        new TowerType { name = "tower5", size = 1f,    color = new Color(0.4f, 0.65f, 1f) },
    };

    public bool addCollider = true;
    public int sortingOrder = 10;

    int selected = -1;      // nothing selected until you press a number
    Sprite squareSprite;
    Camera cam;

    // Creates the spawner automatically when you press Play,
    // so you do NOT need to attach this script to anything.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AutoCreate()
    {
        if (FindAnyObjectByType<TowerSpawner>() == null)
            new GameObject("TowerSpawner").AddComponent<TowerSpawner>();
    }

    void Awake()
    {
        squareSprite = CreateSquareSprite(128);
    }

    void Update()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null) cam = FindAnyObjectByType<Camera>();

        // Number keys 1..5 select the tower type
        for (int i = 0; i < towers.Length && i < 9; i++)
        {
            if (NumberPressed(i + 1))
                selected = i;
        }

        // Right click spawns the selected tower
        if (RightClicked() && selected >= 0 && cam != null)
            Spawn(towers[selected], MouseWorldPosition());
    }

    void Spawn(TowerType t, Vector3 pos)
    {
        GameObject go;

        if (t.prefab != null)
        {
            go = Instantiate(t.prefab, pos, Quaternion.identity);
        }
        else
        {
            go = new GameObject();
            go.transform.position = pos;
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = squareSprite;
            sr.color = t.color;
            sr.sortingOrder = sortingOrder;
            if (addCollider) go.AddComponent<BoxCollider2D>();
        }

        go.name = t.name;
        go.transform.localScale = Vector3.one * t.size;
    }

    // ---------- Input (new + old system) ----------

    bool NumberPressed(int n)
    {
#if ENABLE_INPUT_SYSTEM
        var kb = Keyboard.current;
        if (kb == null) return false;
        return kb[Key.Digit1 + (n - 1)].wasPressedThisFrame ||
               kb[Key.Numpad1 + (n - 1)].wasPressedThisFrame;
#else
        return Input.GetKeyDown(KeyCode.Alpha1 + (n - 1)) ||
               Input.GetKeyDown(KeyCode.Keypad1 + (n - 1));
#endif
    }

    bool RightClicked()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame;
#else
        return Input.GetMouseButtonDown(1);
#endif
    }

    Vector3 MouseWorldPosition()
    {
#if ENABLE_INPUT_SYSTEM
        Vector3 screen = Mouse.current.position.ReadValue();
#else
        Vector3 screen = Input.mousePosition;
#endif
        screen.z = -cam.transform.position.z;   // distance from camera to z = 0
        Vector3 world = cam.ScreenToWorldPoint(screen);
        world.z = 0f;
        return world;
    }

    // ---------- Makes a plain white square sprite (1 unit wide) ----------

    static Sprite CreateSquareSprite(int res)
    {
        var tex = new Texture2D(res, res, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;

        var pixels = new Color32[res * res];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = new Color32(255, 255, 255, 255);
        tex.SetPixels32(pixels);
        tex.Apply();

        return Sprite.Create(tex, new Rect(0, 0, res, res), new Vector2(0.5f, 0.5f), res);
    }
}
