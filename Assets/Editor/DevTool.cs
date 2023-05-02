using UnityEngine;
using UnityEditor;

public class DevTool : EditorWindow
{
    string layoutName = "";
    int layoutID = 1;
    GameObject layoutToSpawn;
    float layoutScale;
    float spawnRadius = 10f;

    [MenuItem("Window/Layout Selector")]

    public static void ShowWindow()
    {
        GetWindow<DevTool>("Layout Selector");
    }

    public void GenerateTooltip(string text)
    {
        var propRect = GUILayoutUtility.GetLastRect();
        GUI.Label(propRect, new GUIContent("", text));
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawns Selected Layout", EditorStyles.boldLabel);

        layoutName = EditorGUILayout.TextField("Layout Name", layoutName);
        GenerateTooltip("Give your chosen layout a name");

        layoutID = EditorGUILayout.IntField("Layout ID", layoutID);
        GenerateTooltip("Assigns a custom # after the chosen Layout Name");

        layoutToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", layoutToSpawn, typeof(GameObject), false) as GameObject;
        GenerateTooltip("Place prefered Prefab here");

        layoutScale = EditorGUILayout.Slider("Layout Scale", layoutScale, 0.1f, 100f);
        GenerateTooltip("Choose Specified Scale for Layout");

        spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
        GenerateTooltip("Assigns a default radius between each Spawned Layout");

        if (GUILayout.Button("Spawn Layout"))
        {
            SpawnLayout();
        }
    }

    private void SpawnLayout()
    {
        if (layoutName == string.Empty)
        {
            Debug.LogError("Error: Please assign a name for your chosen layout prefab.");
            return;
        }

        if (layoutToSpawn == null)
        {
            Debug.LogError("Error: Please assign a layout prefab to be spawned.");
            return;
        }

        Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(spawnCircle.x, 0f, spawnCircle.y);

        GameObject newLayout = Instantiate(layoutToSpawn, spawnPosition, Quaternion.identity);
        newLayout.name = layoutName + layoutID;
        newLayout.transform.localScale = Vector3.one * layoutScale;

        layoutID++;
    }
}
