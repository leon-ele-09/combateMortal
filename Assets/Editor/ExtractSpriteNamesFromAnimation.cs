using UnityEditor;
using UnityEngine;

public class ExtractSpriteNamesFromAnimation : EditorWindow
{
    private AnimationClip selectedClip;

    [MenuItem("Tools/Extract Sprite Names")]
    public static void ShowWindow()
    {
        GetWindow<ExtractSpriteNamesFromAnimation>("Extract Sprite Names");
    }

    void OnGUI()
    {
        GUILayout.Label("Extract Sprite Names from Animation Clip", EditorStyles.boldLabel);

        selectedClip = (AnimationClip)EditorGUILayout.ObjectField("Animation Clip", selectedClip, typeof(AnimationClip), false);

        if (GUILayout.Button("Extract Sprite Names"))
        {
            if (selectedClip == null)
            {
                Debug.LogError("Please select an animation clip.");
                return;
            }

            ExtractSpriteNames(selectedClip);
        }
    }

    private void ExtractSpriteNames(AnimationClip clip)
    {
        var bindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);

        foreach (var binding in bindings)
        {
            if (binding.type == typeof(SpriteRenderer) && binding.propertyName == "m_Sprite")
            {
                var keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);

                Debug.Log($"Animation Clip: {clip.name}");
                foreach (var keyframe in keyframes)
                {
                    if (keyframe.value is Sprite sprite)
                    {
                        Debug.Log($"Time: {keyframe.time}, Sprite: {sprite.name}");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"No SpriteRenderer bindings found in clip: {clip.name}");
            }
        }
    }
}
