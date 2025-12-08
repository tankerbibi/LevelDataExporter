using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
// using System; // System.Environment を使うために必要

public class LevelExporter : EditorWindow
{
    [MenuItem("Tools/Export Level to CSV")]
    public static void ExportLevel()
    {
        // 保存先のパス（必要に応じて変更してください。デスクトップ等に出すとわかりやすいです）
        string path = EditorUtility.SaveFilePanel("Save Level", "C:\\Users\\towak\\source\\repos\\tankerbibi\\DX28_GOLF\\asset\\data", "level_data", "csv");
        if (string.IsNullOrEmpty(path)) return;

        StringBuilder sb = new StringBuilder();

        // シーン上の全てのオブジェクトを取得（必要に応じてタグでフィルタリングしても良いです）
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            int type = -1;

            // オブジェクト名やタグでタイプを判定
            if (obj.name.Contains("Floor")) type = 0;      // 床（特別な扱い）
            else if (obj.name.Contains("Block")) type = 1; // 通常ブロック（元のType 0の代わり）
            else if (obj.name.Contains("Tree")) type = 2;  // 木（元のType 1）
            else if (obj.name.Contains("Kirby")) type = 3; // カービィ

            // 対象外のオブジェクトは無視
            if (type == -1) continue;

            // DirectXはY-Up、UnityもY-Upですが、座標系に合わせて調整が必要な場合があります。
            // ここではそのまま出力します。
            // フォーマット: Type, X, Y, Z
            sb.AppendLine($"{type},{obj.transform.position.x},{obj.transform.position.y},{obj.transform.position.z}");
        }

        File.WriteAllText(path, sb.ToString());
        Debug.Log("Level Exported to: " + path);
    }
}