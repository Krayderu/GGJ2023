using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class ChangeMaterialEditor : EditorWindow
{
    public Material newMaterial;

    [MenuItem("Tools/Change Material")]
    public static void ShowWindow()
    {
        GetWindow<ChangeMaterialEditor>("Change Material");
    }

    private void OnGUI()
    {
        newMaterial = (Material)EditorGUILayout.ObjectField("New Material", newMaterial, typeof(Material), false);

        if (GUILayout.Button("Change Material"))
        {
            SpriteRenderer[] spriteRenderers = FindObjectsOfType<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                Undo.RecordObject(spriteRenderer, "Change Material");
                spriteRenderer.sharedMaterial = newMaterial;
                spriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                spriteRenderer.receiveShadows = true;
            }
        }
    }
}
#endif