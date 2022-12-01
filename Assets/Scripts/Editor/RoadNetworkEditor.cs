using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(RoadNetworkCreator))]
    public class RoadNetworkEditor : UnityEditor.Editor
    {
        private RoadNetworkCreator t;
        private Connection connectionToSelect;

        private bool showRoadDetail;
        private bool showIntersectionDetail;
        
        private RoadNetwork RoadNetwork => t.roadNetwork;

        public override void OnInspectorGUI()
        {
            if (t == null)
                t = target as RoadNetworkCreator;

            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying) return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            showRoadDetail = EditorGUILayout.Toggle("Show Roads Detail", showRoadDetail);
            showIntersectionDetail = EditorGUILayout.Toggle("Show Intersections Detail", showIntersectionDetail);


            if (showIntersectionDetail)
            {
                foreach (var connection in RoadNetwork.GetIntersections())
                {
                    DrawIntersectionDetail(connection);
                    connection.selectedInEditor = false;
                }
            }
            if(connectionToSelect != null)
                connectionToSelect.selectedInEditor = true;
        }

        private void DrawIntersectionDetail(Connection intersection)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUI.enabled = false;
                
                EditorGUILayout.Vector2Field("", intersection.Point);
                EditorGUILayout.Space();
                EditorGUILayout.EnumPopup(intersection.type);
                EditorGUILayout.EnumPopup(intersection.direction);
                
                GUI.enabled = true;
                if (GUILayout.Button("select"))
                    connectionToSelect = intersection;
            }
        }
    }
}