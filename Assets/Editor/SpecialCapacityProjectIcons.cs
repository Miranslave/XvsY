using Script;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
    public static class SpecialCapacityProjectIcons
    {
        static SpecialCapacityProjectIcons()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemOnGUI;
        }

        private static void OnProjectWindowItemOnGUI(string guid, Rect rect)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(path)) return;

            // charge l'asset (retourne null si ce n'est pas un SpecialCapacity)
            var asset = AssetDatabase.LoadAssetAtPath<SpecialCapacity>(path);
            if (asset == null) return;
            // Efface le fond d'origine
             // gris neutre, même ton que l'UI Unity
            Texture2D tex = asset.Icon.texture;

            // si tu utilises Sprite dans SpecialCapacity (public Sprite icon), remplace la ligne ci-dessous par:
            // Texture2D tex = asset.icon != null ? asset.icon.texture : null;

            if (tex == null) return;

            // on dessine l'icône dans la petite case à gauche
            Rect r = new Rect(rect.x+rect.x/2, rect.y+rect.y/2, rect.height/2, rect.height/2);
            GUI.DrawTexture(r, tex, ScaleMode.ScaleToFit);
        } 
    }
