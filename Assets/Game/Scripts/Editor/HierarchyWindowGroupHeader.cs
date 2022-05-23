using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Colorful Hierarchy Window Group Header
/// Author: https://github.com/omerasikoglu
/// Thanks for concept of idea : http://diegogiacomelli.com.br/unitytips-hierarchy-window-group-header
/// Samples: "#9856CC HEADER1" , "#magenta header2" , "# HEADER3"
/// </summary>
[InitializeOnLoad]
public static class HierarchyWindowGroupHeader {
    static HierarchyWindowGroupHeader() {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject == null) return;
        if (!gameObject.name.StartsWith("#", System.StringComparison.Ordinal)) return;

        // format is #color and #FF0012
        string hexCode = gameObject.name[1..].Split(' ')[0];

        // convert to color
        Color color = IsHexCode() ? GetColorFromString(hexCode) :
            (ColorUtility.TryParseHtmlString(hexCode.ToLower(), out var _color) ? _color : Color.black);

        EditorGUI.DrawRect(selectionRect, color);
        EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace($"#{hexCode}", string.Empty).ToUpperInvariant());

        bool IsHexCode() {

            if (hexCode.Length < 6) return false;

            for (int i = 0; i < 6; i++) {

                if (char.IsDigit(hexCode, i)) continue;

                char[] hexLetters = { 'A', 'B', 'C', 'D', 'E', 'F' };

                if (hexLetters.Any(o => char.ToUpper(hexCode[i]).Equals(o))) continue;
                
                return false;
            }
            return true;
        }

        Color GetColorFromString(string color) {
            float red = System.Convert.ToInt32(color[..2], 16) / 255f;
            float green = System.Convert.ToInt32(color.Substring(2, 2), 16) / 255f;
            float blue = System.Convert.ToInt32(color[^2..], 16) / 255f;

            return new Color(red, green, blue);
        }
    }

}