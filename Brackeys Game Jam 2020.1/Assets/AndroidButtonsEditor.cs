//using UnityEditor;
// 
//[CustomEditor(typeof(JumpButtonScript))]
//public class AndroidButtonsEditor : UnityEditor.UI.ButtonEditor
//{
//    public override void OnInspectorGUI()
//    {
//        JumpButtonScript targetMenuButton = (JumpButtonScript)target;

//        targetMenuButton.androidHandler = (PlayerControllerAndroidHUDHandler)EditorGUILayout.ObjectField(targetMenuButton.androidHandler, typeof(PlayerControllerAndroidHUDHandler), true);

//        // Show default inspector property editor
//        base.OnInspectorGUI();
//        
//        serializedObject.ApplyModifiedProperties();
//    }
//}
