using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.MaoOptimizer
{
    [CustomEditor(typeof(IllMaoOptimizer))]
    [AddComponentMenu("")]
    internal class IllMaoOptimizerEditor : Editor
    {
        SerializedProperty ClothFlgProp;
        SerializedProperty AccessoryFlgProp;
        SerializedProperty EarTailFlgProp;
        SerializedProperty HairFlgProp;
        SerializedProperty knifeFlgProp;
        SerializedProperty TPSFlgProp;
        SerializedProperty ClairvoyanceFlgProp;
        SerializedProperty colliderJumpFlgProp;
        SerializedProperty BreastSizeFlgProp;
        SerializedProperty backlightFlgProp;
        SerializedProperty WhiteBreathFlgProp;
        SerializedProperty eightBitFlgProp;
        SerializedProperty PenCtrlFlgProp;
        SerializedProperty HeartGunFlgProp;
        SerializedProperty FaceGestureFlgProp;
        SerializedProperty FaceLockFlgProp;
        SerializedProperty kamitukiFlgProp;
        SerializedProperty nadeFlgProp;
        SerializedProperty candyFlgProp;
        SerializedProperty gamFlgProp;
        SerializedProperty controllerProp;
        SerializedProperty menuProp;
        SerializedProperty paramProp;
        SerializedProperty controllerDefProp;
        SerializedProperty menuDefProp;
        SerializedProperty paramDefProp;
        SerializedProperty IKUSIA_emoteProp;

        private void OnEnable()
        {
            ClothFlgProp = serializedObject.FindProperty("ClothFlg");
            AccessoryFlgProp = serializedObject.FindProperty("AccessoryFlg");
            EarTailFlgProp = serializedObject.FindProperty("EarTailFlg");
            HairFlgProp = serializedObject.FindProperty("HairFlg");
            knifeFlgProp = serializedObject.FindProperty("knifeFlg");
            TPSFlgProp = serializedObject.FindProperty("TPSFlg");
            ClairvoyanceFlgProp = serializedObject.FindProperty("ClairvoyanceFlg");
            colliderJumpFlgProp = serializedObject.FindProperty("colliderJumpFlg");
            BreastSizeFlgProp = serializedObject.FindProperty("BreastSizeFlg");
            backlightFlgProp = serializedObject.FindProperty("backlightFlg");
            WhiteBreathFlgProp = serializedObject.FindProperty("WhiteBreathFlg");
            eightBitFlgProp = serializedObject.FindProperty("eightBitFlg");
            PenCtrlFlgProp = serializedObject.FindProperty("PenCtrlFlg");
            HeartGunFlgProp = serializedObject.FindProperty("HeartGunFlg");
            FaceGestureFlgProp = serializedObject.FindProperty("FaceGestureFlg");
            FaceLockFlgProp = serializedObject.FindProperty("FaceLockFlg");
            kamitukiFlgProp = serializedObject.FindProperty("kamitukiFlg");
            nadeFlgProp = serializedObject.FindProperty("nadeFlg");
            candyFlgProp = serializedObject.FindProperty("candyFlg");
            gamFlgProp = serializedObject.FindProperty("gamFlg");
            controllerProp = serializedObject.FindProperty("controller");
            menuProp = serializedObject.FindProperty("menu");
            paramProp = serializedObject.FindProperty("param");
            controllerDefProp = serializedObject.FindProperty("controllerDef");
            menuDefProp = serializedObject.FindProperty("menuDef");
            paramDefProp = serializedObject.FindProperty("paramDef");
            IKUSIA_emoteProp = serializedObject.FindProperty("IKUSIA_emote");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            // GUIStyle boxStyle = new(GUI.skin.box)
            // {
            //     fontSize = 12,
            //     alignment = TextAnchor.UpperLeft,
            //     padding = new RectOffset(10, 10, 10, 10),
            // };

            // GUILayout.Box(
            //     "選択無しで実行するだけで不要な[パラメーター2bit]と[オブジェクト]\n"
            //         + "が削除されcheckを入れることで該当項目が、削除されます\n"
            //         + "不要な[networkSyncedパラメーター67bit]の同期checkを外します\n"
            //         + "括弧内の数字は削除されるパラメーターの容量になります\n"
            //         + "＊＊＊ツールには、元に戻す機能はありません＊＊＊\n",
            //     boxStyle,
            //     GUILayout.ExpandWidth(true),
            //     GUILayout.Height(95)
            // );

            EditorGUILayout.PropertyField(ClothFlgProp, new GUIContent("衣装削除"));
            EditorGUILayout.PropertyField(AccessoryFlgProp, new GUIContent("アクセ削除"));
            EditorGUILayout.PropertyField(EarTailFlgProp, new GUIContent("耳・尻尾削除"));
            EditorGUILayout.PropertyField(HairFlgProp, new GUIContent("髪毛削除"));
            EditorGUILayout.PropertyField(knifeFlgProp, new GUIContent("ナイフギミック削除"));


            EditorGUILayout.PropertyField(TPSFlgProp, new GUIContent("TPS削除"));
            EditorGUILayout.PropertyField(ClairvoyanceFlgProp, new GUIContent("透視削除"));
            EditorGUILayout.PropertyField(candyFlgProp, new GUIContent("飴削除"));
            EditorGUILayout.PropertyField(gamFlgProp, new GUIContent("ガム削除"));
            EditorGUILayout.PropertyField(
                colliderJumpFlgProp,
                new GUIContent("コライダー・ジャンプ削除")
            );
            EditorGUILayout.PropertyField(BreastSizeFlgProp, new GUIContent("バストサイズ削除"));
            EditorGUILayout.PropertyField(backlightFlgProp, new GUIContent("backlight削除"));

            EditorGUILayout.PropertyField(WhiteBreathFlgProp, new GUIContent("ホワイトブレス削除"));
            EditorGUILayout.PropertyField(eightBitFlgProp, new GUIContent("8bit削除"));
            EditorGUILayout.PropertyField(PenCtrlFlgProp, new GUIContent("ペン操作削除"));
            EditorGUILayout.PropertyField(HeartGunFlgProp, new GUIContent("ハートガン削除"));
            EditorGUILayout.PropertyField(
                FaceGestureFlgProp,
                new GUIContent("デフォルトの表情プリセット削除(faceEmoなど使う場合)")
            );
            EditorGUILayout.PropertyField(FaceLockFlgProp, new GUIContent("FaceLock削除"));

            EditorGUILayout.PropertyField(
                nadeFlgProp,
                new GUIContent("なでギミックをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                kamitukiFlgProp,
                new GUIContent("噛みつきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                IKUSIA_emoteProp,
                new GUIContent("IKUSIA_emoteをメニューのみ削除")
            );
            // Execute ボタンの追加
            if (GUILayout.Button("Execute"))
            {
                IllMaoOptimizer script = (IllMaoOptimizer)target;
                VRCAvatarDescriptor descriptor =
                    script.transform.root.GetComponent<VRCAvatarDescriptor>();
                if (descriptor != null)
                {
                    try
                    {
                        script.Execute(descriptor);
                    }
                    catch (System.Exception)
                    {
                        Debug.LogWarning("変換に失敗しました。再実行します。");
                        script.Execute(descriptor);
                    }
                }
                else
                {
                    Debug.LogWarning("VRCAvatarDescriptor が見つかりません。");
                }
            }
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成する元Asset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            GUI.enabled = false;
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controllerDefProp, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuDefProp, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramDefProp, new GUIContent("Expression Parameters"));
            GUI.enabled = true;
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成されたAsset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controllerProp, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuProp, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramProp, new GUIContent("Expression Parameters"));

            // 変更内容の適用
            serializedObject.ApplyModifiedProperties();
        }

        // ▼ 右クリックメニューで作成できるようにするためのメニュー項目 ▼

        // Validate メソッドで対象が VRC アバターのルートであり、さらにアタッチされている Animator の avatar が "maoAvatar" であることをチェック
        [MenuItem("GameObject/illusive_tools/Create IllMaoOptimizer Object", true)]
        private static bool ValidateCreateIllMaoOptimizerObject(MenuCommand menuCommand)
        {
            GameObject contextGO = menuCommand.context as GameObject;
            if (contextGO == null)
            {
                contextGO = Selection.activeGameObject;
            }
            if (contextGO == null)
            {
                // どちらも null ならエラーを出すか、false を返す
                return false;
            }
            // 対象が VRCAvatarDescriptor を持っているか
            if (contextGO.GetComponent<VRCAvatarDescriptor>() == null)
                return false;
            // さらに、親に VRCAvatarDescriptor が存在しない（＝ルートである）かをチェック
            if (
                contextGO.transform.parent != null
                && contextGO.transform.parent.GetComponent<VRCAvatarDescriptor>() != null
            )
                return false;
            // Animator コンポーネントが存在し、その avatar プロパティの名前が "maoAvatar" であるかをチェック
            Animator animator = contextGO.GetComponent<Animator>();
            if (animator == null)
                return false;
            if (animator.avatar == null)
                return false;
            if (animator.avatar.name != "maoAvatar")
                return false;
            return true;
        }

        // 対象が条件を満たす場合のみ、メニュー項目が有効となる
        [MenuItem("GameObject/illusive_tools/Create IllMaoOptimizer Object", false, 10)]
        private static void CreateIllMaoOptimizerObject(MenuCommand menuCommand)
        {
            // 新しい GameObject を作成し、IllMaoOptimizer コンポーネントを追加
            GameObject go = new GameObject("IllMaoOptimizer");
            go.AddComponent<IllMaoOptimizer>();

            // 右クリックで選択されたオブジェクト（VRCアバターのルート）の子として配置
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
