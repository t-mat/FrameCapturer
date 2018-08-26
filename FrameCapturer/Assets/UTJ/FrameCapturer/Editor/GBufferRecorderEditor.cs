using System;
using UnityEditor;
using UnityEngine;

namespace UTJ.FrameCapturer
{
    [CustomEditor(typeof(GBufferRecorder))]
    public class ImageSequenceRecorderEditor : RecorderBaseEditor
    {
        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();

            var recorder = target as GBufferRecorder;
            var so = serializedObject;

            CommonConfig();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Capture Components");
            EditorGUI.indentLevel++;
            {
                EditorGUI.BeginChangeCheck();
                var fbc = recorder.fbComponents;
                var fnp = recorder.filenamePrefixes;

                fbc.frameBuffer = EditorGUILayout.Toggle("Frame Buffer", fbc.frameBuffer);
                if(fbc.frameBuffer)
                {
                    EditorGUI.indentLevel++;
                    ToggleTextField("RGB"  , ref fbc.fbRGB  , ref fnp.fbRGB);
                    ToggleTextField("Alpha", ref fbc.fbAlpha, ref fnp.fbAlpha);
                    ToggleTextField("RGBA" , ref fbc.fbRGBA , ref fnp.fbRGBA);
                    EditorGUI.indentLevel--;
                }

                fbc.GBuffer = EditorGUILayout.Toggle("GBuffer", fbc.GBuffer);
                if (fbc.GBuffer)
                {
                    EditorGUI.indentLevel++;
                    ToggleTextField("Albedo"    , ref fbc.gbAlbedo    , ref fnp.gbAlbedo);
                    ToggleTextField("Occlusion" , ref fbc.gbOcclusion , ref fnp.gbOcclusion);
                    ToggleTextField("Specular"  , ref fbc.gbSpecular  , ref fnp.gbSpecular);
                    ToggleTextField("Smoothness", ref fbc.gbSmoothness, ref fnp.gbSmoothness);
                    ToggleTextField("Normal"    , ref fbc.gbNormal    , ref fnp.gbNormal);
                    ToggleTextField("Emission"  , ref fbc.gbEmission  , ref fnp.gbEmission);
                    ToggleTextField("Depth"     , ref fbc.gbDepth     , ref fnp.gbDepth);
                    ToggleTextField("Velocity"  , ref fbc.gbVelocity  , ref fnp.gbVelocity);
                    EditorGUI.indentLevel--;
                }
                if (EditorGUI.EndChangeCheck())
                {
                    recorder.filenamePrefixes = fnp;
                    recorder.fbComponents = fbc;
                    EditorUtility.SetDirty(recorder);
                }
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            ResolutionControl();
            FramerateControl();

            EditorGUILayout.Space();

            RecordingControl();

            so.ApplyModifiedProperties();
        }

        static void ToggleTextField(string label, ref bool enable, ref string text)
        {
            EditorGUILayout.BeginHorizontal();
            enable = EditorGUILayout.Toggle(label, enable);
            if (enable)
            {
                text = EditorGUILayout.TextField("", text);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
