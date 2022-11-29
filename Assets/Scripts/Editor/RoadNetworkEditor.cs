﻿using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(RoadNetworkCreator))]
    public class RoadNetworkEditor : UnityEditor.Editor
    {
        private RoadNetworkCreator t;
        private Road roadToRemove;
        private Road roadToSelect;
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

            if(roadToRemove != null)
                RoadNetwork.RemoveRoad(roadToRemove);
            if(connectionToSelect != null)
                connectionToSelect.selectedInEditor = true;
        }

        private void DrawIntersectionDetail(Connection intersection)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUI.enabled = false;
                
                EditorGUILayout.LabelField("intersection", GUILayout.Width(50));
                EditorGUILayout.Space();
                EditorGUILayout.Vector2Field("", intersection.Point);
                EditorGUILayout.Space();
                
                GUI.enabled = true;
                if (GUILayout.Button("select"))
                    connectionToSelect = intersection;
            }
        }

        private void DrawRoadDetails(Road road)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUI.enabled = false;
                EditorGUILayout.LabelField("road line", GUILayout.Width(30));
                EditorGUILayout.Space();
                EditorGUILayout.Vector2Field("", road.roadLine.start);
                EditorGUILayout.Space();
                EditorGUILayout.Vector2Field("", road.roadLine.end);
                EditorGUILayout.Space();
                GUI.enabled = true;
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("delete"))
                    roadToRemove = road;
                if (GUILayout.Button("select"))
                    roadToSelect = road;
            }

            EditorGUILayout.Space();
        }
    }
}