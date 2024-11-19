using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ReplaceSpritesFromFolder : EditorWindow
{
    public string spriteSheetName;
    public string oldSheetname;// New sprite name prefix (e.g., "Toad_")
    public Object spriteSheet; // The new sprite sheet
    public DefaultAsset targetFolder; // The folder containing animation clips

    [MenuItem("Tools/Replace Sprites in Folder")]
    static void Init()
    {
        ReplaceSpritesFromFolder window = (ReplaceSpritesFromFolder)EditorWindow.GetWindow(typeof(ReplaceSpritesFromFolder));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Replace Sprites in Folder", EditorStyles.boldLabel);
        spriteSheetName = EditorGUILayout.TextField("Sprite Name Prefix", spriteSheetName);
        oldSheetname = EditorGUILayout.TextField("Old Sprite Name Prefix", oldSheetname);
        spriteSheet = EditorGUILayout.ObjectField("Sprite Sheet", spriteSheet, typeof(Object), false);
        targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Target Folder", targetFolder, typeof(DefaultAsset), false);

        if (GUILayout.Button("Replace Sprites"))
        {
            if (string.IsNullOrEmpty(spriteSheetName) || spriteSheet == null || targetFolder == null)
            {
                Debug.LogError("Please provide a sprite name prefix, select a sprite sheet, and specify a target folder.");
            }
            else
            {
                ReplaceSprites();
            }
        }
    }

    void ReplaceSprites()
    {
        string folderPath = AssetDatabase.GetAssetPath(targetFolder);

        if (string.IsNullOrEmpty(folderPath) || !AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogError("Invalid target folder selected.");
            return;
        }

        // Load sprites from the sprite sheet
        var newSprites = LoadSpritesFromSheet(spriteSheet);
        if (newSprites.Count == 0)
        {
            Debug.LogError("No sprites found in the selected sprite sheet.");
            return;
        }

        // Process animation clips in the folder
        var animationClips = FindAnimationClipsInFolder(folderPath);
        foreach (var clip in animationClips)
        {
            Debug.Log($"Processing animation clip: {clip.name}");
            bool updated = ReplaceSpritesInClip(clip, newSprites);
            if (updated)
                Debug.Log($"Updated sprites in animation clip: {clip.name}");
            else
                Debug.LogWarning($"No sprites replaced in animation clip: {clip.name}. Check sprite names or bindings.");
        }

        Debug.Log("Sprite replacement process completed!");
    }

    Dictionary<string, Sprite> LoadSpritesFromSheet(Object spriteSheet)
    {
        var sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(spriteSheet));
        var spriteDict = new Dictionary<string, Sprite>();

        foreach (var asset in sprites)
        {
            if (asset is Sprite sprite && sprite.name.StartsWith(spriteSheetName)) // Check if sprite starts with the new name prefix
            {
                spriteDict[sprite.name] = sprite;
            }
        }

        Debug.Log($"Loaded {spriteDict.Count} sprites from sprite sheet: {spriteSheet.name}");
        return spriteDict;
    }

    bool ReplaceSpritesInClip(AnimationClip clip, Dictionary<string, Sprite> newSprites)
    {
        bool replaced = false;
        var bindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);

        foreach (var binding in bindings)
        {
            if (binding.type == typeof(SpriteRenderer))
            {
                var keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);

                for (int i = 0; i < keyframes.Length; i++)
                {
                    var oldSpriteName = keyframes[i].value.name;
                    string newSpriteName = GetNewSpriteName(oldSpriteName); // Get the transformed sprite name

                    if (newSpriteName == null)
                    {
                        Debug.LogWarning($"Sprite '{oldSpriteName}' could not be matched with the new prefix. Skipping replacement.");
                        continue;
                    }

                    if (newSprites.TryGetValue(newSpriteName, out Sprite newSprite))
                    {
                        Debug.Log($"Replacing sprite '{oldSpriteName}' with '{newSprite.name}' in clip: {clip.name}");
                        keyframes[i].value = newSprite;
                        replaced = true;
                    }
                    else
                    {
                        Debug.LogWarning($"Sprite '{newSpriteName}' not found in sprite sheet for clip: {clip.name}");
                    }
                }

                AnimationUtility.SetObjectReferenceCurve(clip, binding, keyframes);
            }
        }

        return replaced;
    }


    string GetNewSpriteName(string oldSpriteName)
    {
        // Modify the old prefix to match your actual old sprite name prefix
        string oldPrefix = oldSheetname; // Change this to match your old sprite name prefix
        string newPrefix = spriteSheetName; // This is the new prefix for the sprite sheet

        if (oldSpriteName.StartsWith(oldPrefix))
        {
            string newSpriteName = newPrefix + oldSpriteName.Substring(oldPrefix.Length);
            return newSpriteName;
        }
        else
        {
            // If the old sprite name doesn't match the expected prefix, log a warning
            Debug.LogWarning($"Old sprite name '{oldSpriteName}' does not start with expected prefix '{oldPrefix}'");
            return null; // Return null so we can debug
        }
    }


    List<AnimationClip> FindAnimationClipsInFolder(string folderPath)
    {
        var results = new List<AnimationClip>();
        var guids = AssetDatabase.FindAssets("t:AnimationClip", new[] { folderPath });

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
            if (clip != null)
                results.Add(clip);
        }

        Debug.Log($"Found {results.Count} animation clips in folder: {folderPath}");
        return results;
    }
}
