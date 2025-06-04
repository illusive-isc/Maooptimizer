using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.MaoOptimizer
{
    [CustomEditor(typeof(IllMaoOptimizer))]
    [AddComponentMenu("")]
    internal class IllMaoOptimizerEditor : Editor
    {
        SerializedProperty ClothFlg0;
        SerializedProperty ClothFlg;
        SerializedProperty ClothFlg1;
        SerializedProperty ClothFlg5;
        SerializedProperty ClothFlg2;
        SerializedProperty ClothFlg3;
        SerializedProperty ClothFlg4;
        SerializedProperty AccessoryFlg0;
        SerializedProperty AccessoryFlg1;
        SerializedProperty AccessoryFlg2;
        SerializedProperty AccessoryFlg3;
        SerializedProperty AccessoryFlg4;
        SerializedProperty AccessoryFlg5;
        SerializedProperty AccessoryFlg6;
        SerializedProperty AccessoryFlg7;
        SerializedProperty EarTailFlg0;
        SerializedProperty EarTailFlg1;
        SerializedProperty EarTailFlg2;
        SerializedProperty EarTailFlg3;
        SerializedProperty EarTailFlg4;
        SerializedProperty HairFlg0;
        SerializedProperty HairFlg1;
        SerializedProperty HairFlg2;
        SerializedProperty HairFlg;
        SerializedProperty knifeFlg;
        SerializedProperty TPSFlg;
        SerializedProperty ClairvoyanceFlg;
        SerializedProperty colliderJumpFlg;
        SerializedProperty BreastSizeFlg;
        SerializedProperty BreastSizeFlg2;
        SerializedProperty BreastSizeFlg3;
        SerializedProperty BreastSizeFlg4;
        SerializedProperty backlightFlg;
        SerializedProperty WhiteBreathFlg;
        SerializedProperty eightBitFlg;
        SerializedProperty PenCtrlFlg;
        SerializedProperty HeartGunFlg;
        SerializedProperty FaceGestureFlg;
        SerializedProperty FaceLockFlg;
        SerializedProperty kamitukiFlg;
        SerializedProperty nadeFlg;
        SerializedProperty candyFlg;
        SerializedProperty gamFlg;
        SerializedProperty controller;
        SerializedProperty menu;
        SerializedProperty param;
        SerializedProperty controllerDef;
        SerializedProperty menuDef;
        SerializedProperty paramDef;
        SerializedProperty IKUSIA_emote;

        private void OnEnable()
        {
            ClothFlg0 = serializedObject.FindProperty("ClothFlg0");
            ClothFlg = serializedObject.FindProperty("ClothFlg");
            ClothFlg1 = serializedObject.FindProperty("ClothFlg1");
            ClothFlg2 = serializedObject.FindProperty("ClothFlg2");
            ClothFlg3 = serializedObject.FindProperty("ClothFlg3");
            ClothFlg4 = serializedObject.FindProperty("ClothFlg4");
            ClothFlg5 = serializedObject.FindProperty("ClothFlg5");
            AccessoryFlg0 = serializedObject.FindProperty("AccessoryFlg0");
            AccessoryFlg1 = serializedObject.FindProperty("AccessoryFlg1");
            AccessoryFlg2 = serializedObject.FindProperty("AccessoryFlg2");
            AccessoryFlg3 = serializedObject.FindProperty("AccessoryFlg3");
            AccessoryFlg4 = serializedObject.FindProperty("AccessoryFlg4");
            AccessoryFlg5 = serializedObject.FindProperty("AccessoryFlg5");
            AccessoryFlg6 = serializedObject.FindProperty("AccessoryFlg6");
            AccessoryFlg7 = serializedObject.FindProperty("AccessoryFlg7");
            EarTailFlg0 = serializedObject.FindProperty("EarTailFlg0");
            EarTailFlg1 = serializedObject.FindProperty("EarTailFlg1");
            EarTailFlg2 = serializedObject.FindProperty("EarTailFlg2");
            EarTailFlg3 = serializedObject.FindProperty("EarTailFlg3");
            EarTailFlg4 = serializedObject.FindProperty("EarTailFlg4");
            HairFlg0 = serializedObject.FindProperty("HairFlg0");
            HairFlg1 = serializedObject.FindProperty("HairFlg1");
            HairFlg2 = serializedObject.FindProperty("HairFlg2");
            HairFlg = serializedObject.FindProperty("HairFlg");
            knifeFlg = serializedObject.FindProperty("knifeFlg");
            TPSFlg = serializedObject.FindProperty("TPSFlg");
            ClairvoyanceFlg = serializedObject.FindProperty("ClairvoyanceFlg");
            colliderJumpFlg = serializedObject.FindProperty("colliderJumpFlg");
            BreastSizeFlg = serializedObject.FindProperty("BreastSizeFlg");
            BreastSizeFlg2 = serializedObject.FindProperty("BreastSizeFlg2");
            BreastSizeFlg3 = serializedObject.FindProperty("BreastSizeFlg3");
            BreastSizeFlg4 = serializedObject.FindProperty("BreastSizeFlg4");
            backlightFlg = serializedObject.FindProperty("backlightFlg");
            WhiteBreathFlg = serializedObject.FindProperty("WhiteBreathFlg");
            eightBitFlg = serializedObject.FindProperty("eightBitFlg");
            PenCtrlFlg = serializedObject.FindProperty("PenCtrlFlg");
            HeartGunFlg = serializedObject.FindProperty("HeartGunFlg");
            FaceGestureFlg = serializedObject.FindProperty("FaceGestureFlg");
            FaceLockFlg = serializedObject.FindProperty("FaceLockFlg");
            kamitukiFlg = serializedObject.FindProperty("kamitukiFlg");
            nadeFlg = serializedObject.FindProperty("nadeFlg");
            candyFlg = serializedObject.FindProperty("candyFlg");
            gamFlg = serializedObject.FindProperty("gamFlg");
            controller = serializedObject.FindProperty("controller");
            menu = serializedObject.FindProperty("menu");
            param = serializedObject.FindProperty("param");
            controllerDef = serializedObject.FindProperty("controllerDef");
            menuDef = serializedObject.FindProperty("menuDef");
            paramDef = serializedObject.FindProperty("paramDef");
            IKUSIA_emote = serializedObject.FindProperty("IKUSIA_emote");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(ClothFlg3, new GUIContent("ヒールON"));
            EditorGUILayout.PropertyField(ClothFlg4, new GUIContent("ハイヒールON"));

            EditorGUILayout.PropertyField(ClothFlg0, new GUIContent("衣装メニューのみ削除"));
            if (!ClothFlg0.boolValue)
            {
                GUI.enabled = false;
                ClothFlg.boolValue = false;
                ClothFlg1.boolValue = false;
                ClothFlg2.boolValue = false;
            }

            EditorGUILayout.PropertyField(ClothFlg1, new GUIContent("  ├ タンクトップ"));
            EditorGUILayout.PropertyField(ClothFlg5, new GUIContent("  ├ Tシャツ"));
            {
                var MaoOptimizer = (IllMaoOptimizer)target;
                if (ClothFlg1.boolValue != MaoOptimizer.ClothFlg1)
                {
                    ClothFlg5.boolValue = false;
                }
                else if (ClothFlg5.boolValue != MaoOptimizer.ClothFlg5)
                {
                    ClothFlg1.boolValue = false;
                }
            }
            EditorGUILayout.PropertyField(ClothFlg, new GUIContent("  └ 衣装削除"));
            if (!ClothFlg.boolValue)
            {
                GUI.enabled = false;
                ClothFlg2.boolValue = false;
            }
            EditorGUILayout.PropertyField(ClothFlg2, new GUIContent("      └ 下着も削除"));
            GUI.enabled = true;

            EditorGUILayout.PropertyField(AccessoryFlg0, new GUIContent("アクセメニューのみ削除"));
            if (!AccessoryFlg0.boolValue)
            {
                GUI.enabled = false;
                AccessoryFlg1.boolValue = false;
                AccessoryFlg2.boolValue = false;
                AccessoryFlg3.boolValue = false;
                AccessoryFlg4.boolValue = false;
                AccessoryFlg5.boolValue = false;
                AccessoryFlg6.boolValue = false;
                AccessoryFlg7.boolValue = false;
            }
            EditorGUILayout.PropertyField(AccessoryFlg1, new GUIContent("  ├ ネックレスON"));
            EditorGUILayout.PropertyField(AccessoryFlg2, new GUIContent("  ├ チョーカーON"));
            EditorGUILayout.PropertyField(AccessoryFlg3, new GUIContent("  ├ リストバンドON"));
            EditorGUILayout.PropertyField(AccessoryFlg4, new GUIContent("  ├ ニットON"));
            EditorGUILayout.PropertyField(AccessoryFlg7, new GUIContent("  ├ マスクON"));
            EditorGUILayout.PropertyField(AccessoryFlg5, new GUIContent("  └ 眼鏡ON"));
            if (!AccessoryFlg5.boolValue)
            {
                GUI.enabled = false;
                AccessoryFlg6.boolValue = false;
            }
            EditorGUILayout.PropertyField(AccessoryFlg6, new GUIContent("      └ 眼鏡上固定"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(EarTailFlg0, new GUIContent("耳・尻尾メニューのみ削除"));
            if (!EarTailFlg0.boolValue)
            {
                GUI.enabled = false;
                EarTailFlg1.boolValue = false;
                EarTailFlg2.boolValue = false;
                EarTailFlg3.boolValue = false;
                EarTailFlg4.boolValue = false;
            }
            EditorGUILayout.PropertyField(EarTailFlg1, new GUIContent("  ├ 耳削除"));
            EditorGUILayout.PropertyField(EarTailFlg2, new GUIContent("  ├ 尻尾削除"));
            if (EarTailFlg2.boolValue)
            {
                EarTailFlg3.boolValue = true;
                EarTailFlg4.boolValue = true;
            }

            EditorGUILayout.PropertyField(EarTailFlg4, new GUIContent("  ├ 尻尾ベルト削除"));
            EditorGUILayout.PropertyField(EarTailFlg3, new GUIContent("  └ 尻尾巻きつき削除"));
            GUI.enabled = true;

            EditorGUILayout.PropertyField(HairFlg0, new GUIContent("髪毛メニューのみ削除"));
            if (!HairFlg0.boolValue)
            {
                GUI.enabled = false;
                HairFlg1.boolValue = false;
                HairFlg2.boolValue = false;
                HairFlg.boolValue = false;
            }
            EditorGUILayout.PropertyField(HairFlg1, new GUIContent("  ├ ポンパドールON"));
            EditorGUILayout.PropertyField(HairFlg2, new GUIContent("  ├ 長髪ON"));
            EditorGUILayout.PropertyField(HairFlg, new GUIContent("  └ 髪毛削除"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(BreastSizeFlg, new GUIContent("バストサイズ変更削除"));
            if (!BreastSizeFlg.boolValue)
            {
                GUI.enabled = false;
                BreastSizeFlg2.boolValue = false;
                BreastSizeFlg3.boolValue = false;
                BreastSizeFlg4.boolValue = false;
            }
            EditorGUILayout.PropertyField(
                BreastSizeFlg2,
                new GUIContent("  ├ BreastSize100にする")
            );
            EditorGUILayout.PropertyField(
                BreastSizeFlg3,
                new GUIContent("  ├ BreastSizeりりか100にする")
            );
            EditorGUILayout.PropertyField(BreastSizeFlg4, new GUIContent("  └ 男性胸にする"));
            GUI.enabled = true;
            {
                var MaoOptimizer = (IllMaoOptimizer)target;
                if (BreastSizeFlg2.boolValue != MaoOptimizer.BreastSizeFlg2)
                {
                    BreastSizeFlg3.boolValue = false;
                    BreastSizeFlg4.boolValue = false;
                }
                else if (BreastSizeFlg3.boolValue != MaoOptimizer.BreastSizeFlg3)
                {
                    BreastSizeFlg2.boolValue = false;
                    BreastSizeFlg4.boolValue = false;
                }
                else if (BreastSizeFlg4.boolValue != MaoOptimizer.BreastSizeFlg4)
                {
                    BreastSizeFlg2.boolValue = false;
                    BreastSizeFlg3.boolValue = false;
                }
            }
            EditorGUILayout.PropertyField(knifeFlg, new GUIContent("ナイフギミック削除"));

            EditorGUILayout.PropertyField(TPSFlg, new GUIContent("TPS削除"));
            EditorGUILayout.PropertyField(ClairvoyanceFlg, new GUIContent("透視削除"));
            EditorGUILayout.PropertyField(candyFlg, new GUIContent("飴削除"));
            EditorGUILayout.PropertyField(gamFlg, new GUIContent("ガム削除"));
            EditorGUILayout.PropertyField(
                colliderJumpFlg,
                new GUIContent("コライダー・ジャンプ削除")
            );

            EditorGUILayout.PropertyField(backlightFlg, new GUIContent("backlight削除"));

            EditorGUILayout.PropertyField(WhiteBreathFlg, new GUIContent("ホワイトブレス削除"));
            EditorGUILayout.PropertyField(eightBitFlg, new GUIContent("8bit削除"));
            EditorGUILayout.PropertyField(PenCtrlFlg, new GUIContent("ペン操作削除"));
            EditorGUILayout.PropertyField(HeartGunFlg, new GUIContent("ハートガン削除"));
            EditorGUILayout.PropertyField(
                FaceGestureFlg,
                new GUIContent("デフォルトの表情プリセット削除(faceEmoなど使う場合)")
            );
            EditorGUILayout.PropertyField(FaceLockFlg, new GUIContent("FaceLock削除"));

            EditorGUILayout.PropertyField(
                nadeFlg,
                new GUIContent("なでギミックをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                kamitukiFlg,
                new GUIContent("噛みつきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                IKUSIA_emote,
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
            EditorGUILayout.PropertyField(controllerDef, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuDef, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramDef, new GUIContent("Expression Parameters"));
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
            EditorGUILayout.PropertyField(controller, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menu, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(param, new GUIContent("Expression Parameters"));

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
