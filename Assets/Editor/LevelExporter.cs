using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class LevelExporter : EditorWindow
{
    [MenuItem("Tools/Export Level to CSV")]
    public static void ExportLevel()
    {
        // 保存先のパスをプロジェクトのデータフォルダに直接指定
        string path = EditorUtility.SaveFilePanel("Save Level", "C:\\Users\\towak\\source\\repos\\tankerbibi\\DX28_GOLF\\asset\\data", "levelData_1", "csv");
        if (string.IsNullOrEmpty(path)) return;

        StringBuilder sb = new StringBuilder();
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            string name = "aaa";

            // 名前でType判定（DirectX側の配列インデックスに合わせる）
            if (obj.name.Contains("Block")) name = "Block";
            if (obj.name.Contains("Tree")) name = "Tree";
            //if (obj.name.Contains("Kirby")) name = "Kirby";
            if (obj.name.Contains("BreakableBlock")) name = "BreakableBlock";
            if (obj.name.Contains("StartP")) name = "StartPoint";
            if (obj.name.Contains("StartFl")) name = "StartFlag";
            if (obj.name.Contains("GoalP")) name = "GoalPoint";
            if (obj.name.Contains("GoalFl")) name = "GoalFlag";
            if (obj.name.Contains("BackgroundB")) name = "BackgroundBlock";
            if (obj.name.Contains("BillboardTree")) name = "BillboardTree";
            if (obj.name.Contains("Grass")) name = "Grass";
            if (obj.name.Contains("slope")) name = "Slope";
            // if (obj.name.StartsWith("Grass")) name = "Grass";

            //if (obj.name.StartsWith("Background")) name = "BackgroundBlock";

            // 判定に該当しないオブジェクト（CameraやLightなど）はスキップ
            if (name == "aaa") continue;

            // --- 座標の補正（四捨五入） ---
            // 小数点第2位までで丸める処理（1.00001 -> 1.0）
            float posX = Mathf.Round(obj.transform.position.x * 100f) / 100f;
            float posY = Mathf.Round(obj.transform.position.y * 100f) / 100f;
            float posZ = Mathf.Round(obj.transform.position.z * 100f) / 100f;

            float rotX = Mathf.Round(obj.transform.rotation.x * 100f) / 100f;
            float rotY = Mathf.Round(obj.transform.rotation.y * 100f) / 100f;
            float rotZ = Mathf.Round(obj.transform.rotation.z * 100f) / 100f;

            float sizeX = Mathf.Round(obj.transform.localScale.x * 100f) / 100f;
            float sizeY = Mathf.Round(obj.transform.localScale.y * 100f) / 100f;
            float sizeZ = Mathf.Round(obj.transform.localScale.z * 100f) / 100f;

            // フォーマット: Type, X, Y, Z
            sb.AppendLine($"{name},{posX},{posY},{posZ},{rotX},{rotY},{rotZ},{sizeX},{sizeY},{sizeZ}");
        }
        File.WriteAllText(path, sb.ToString());
        Debug.Log("Level Exported with rounded coordinates to: " + path);
    }
}