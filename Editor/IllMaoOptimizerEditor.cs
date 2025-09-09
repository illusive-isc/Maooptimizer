using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
#if AVATAR_OPTIMIZER_FOUND
using Anatawa12.AvatarOptimizer;
#endif
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
        SerializedProperty knifeFlg2;
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
        SerializedProperty questFlg1;
        bool questArea;
        SerializedProperty Butt;
        SerializedProperty Breast;
        SerializedProperty ear_004;
        SerializedProperty ear_hat_006;
        SerializedProperty ahoge;
        SerializedProperty back_long_C;
        SerializedProperty back_long_014;
        SerializedProperty back_long_root_001;
        SerializedProperty front_root;
        SerializedProperty side;
        SerializedProperty side_1_004;
        SerializedProperty side_short_root;
        SerializedProperty mask;
        SerializedProperty glass;
        SerializedProperty neckless;
        SerializedProperty neckless_2;
        SerializedProperty outer;
        SerializedProperty Pants;
        SerializedProperty tail;
        SerializedProperty tail_belt;

        SerializedProperty upperArm_collider1;
        SerializedProperty plane_collider1;
        SerializedProperty chest_collider1;
        SerializedProperty plane_collider2;
        SerializedProperty chest_collider2;
        SerializedProperty chestPanel_collider1;
        SerializedProperty chestPanel_collider2;
        SerializedProperty upperleg_collider1;
        SerializedProperty sode_collider;
        SerializedProperty plane_collider3;
        SerializedProperty upperleg_collider2;
        SerializedProperty hip_collider1;
        SerializedProperty chest_collider3;
        SerializedProperty AFK_collider1;

        SerializedProperty plane_collider4;

        SerializedProperty upperleg_collider3;
        SerializedProperty lowerleg_collider1;
        SerializedProperty plane_collider5;
        SerializedProperty textureResize;
        SerializedProperty AAORemoveFlg;

        // PB情報とコライダー情報のクラス定義（namespace内、Editorクラス外に移動）
        public class PhysBoneInfo
        {
            public int AffectedCount; //:Transform数
            public int Count; //:Transform数
            public int ColliderCount; //:Collider数
            public int[] ColliderCounts; //:Collider数
        }

        public static readonly Dictionary<string, PhysBoneInfo> physBoneList = new()
        {
            {
                "Butt",
                new PhysBoneInfo { AffectedCount = 4 }
            },
            {
                "Breast",
                new PhysBoneInfo { AffectedCount = 6, ColliderCount = 8 }
            },
            {
                "ear_004",
                new PhysBoneInfo { AffectedCount = 8 }
            },
            {
                "ear_hat_006",
                new PhysBoneInfo { AffectedCount = 13 }
            },
            {
                "ahoge",
                new PhysBoneInfo { AffectedCount = 5 }
            },
            {
                "back_long_C",
                new PhysBoneInfo { AffectedCount = 5, ColliderCount = 4 }
            },
            {
                "back_long_014",
                new PhysBoneInfo { AffectedCount = 22, ColliderCount = 16 }
            },
            {
                "back_long_root_001",
                new PhysBoneInfo { AffectedCount = 7 }
            },
            {
                "front_root",
                new PhysBoneInfo { AffectedCount = 23 }
            },
            {
                "side",
                new PhysBoneInfo { AffectedCount = 12, ColliderCount = 10 }
            },
            {
                "side_1_004",
                new PhysBoneInfo { AffectedCount = 6 }
            },
            {
                "side_short_root",
                new PhysBoneInfo { AffectedCount = 3, ColliderCount = 2 }
            },
            {
                "glass",
                new PhysBoneInfo { AffectedCount = 2 }
            },
            {
                "mask",
                new PhysBoneInfo { AffectedCount = 2 }
            },
            {
                "neckless",
                new PhysBoneInfo { AffectedCount = 3 }
            },
            {
                "neckless_2",
                new PhysBoneInfo { AffectedCount = 11 }
            },
            {
                "outer",
                new PhysBoneInfo { AffectedCount = 58, ColliderCounts = new[] { 2, 64 } }
            },
            {
                "tail",
                new PhysBoneInfo { AffectedCount = 17, ColliderCount = 16 }
            },
            {
                "tail_belt",
                new PhysBoneInfo { AffectedCount = 12, ColliderCount = 14 }
            },
            {
                "Pants",
                new PhysBoneInfo { AffectedCount = 38, ColliderCounts = new[] { 16, 28, 10 } }
            },
        };

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
            knifeFlg2 = serializedObject.FindProperty("knifeFlg2");
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
            questFlg1 = serializedObject.FindProperty("questFlg1");
            Butt = serializedObject.FindProperty("Butt");
            Breast = serializedObject.FindProperty("Breast");
            ear_004 = serializedObject.FindProperty("ear_004");
            ear_hat_006 = serializedObject.FindProperty("ear_hat_006");
            ahoge = serializedObject.FindProperty("ahoge");
            back_long_C = serializedObject.FindProperty("back_long_C");
            back_long_014 = serializedObject.FindProperty("back_long_014");
            back_long_root_001 = serializedObject.FindProperty("back_long_root_001");
            front_root = serializedObject.FindProperty("front_root");
            side = serializedObject.FindProperty("side");
            side_1_004 = serializedObject.FindProperty("side_1_004");
            side_short_root = serializedObject.FindProperty("side_short_root");
            glass = serializedObject.FindProperty("glass");
            mask = serializedObject.FindProperty("mask");
            neckless = serializedObject.FindProperty("neckless");
            neckless_2 = serializedObject.FindProperty("neckless_2");
            outer = serializedObject.FindProperty("outer");
            tail = serializedObject.FindProperty("tail");
            tail_belt = serializedObject.FindProperty("tail_belt");
            Pants = serializedObject.FindProperty("Pants");

            upperArm_collider1 = serializedObject.FindProperty("upperArm_collider1");
            plane_collider1 = serializedObject.FindProperty("plane_collider1");
            chest_collider1 = serializedObject.FindProperty("chest_collider1");
            plane_collider2 = serializedObject.FindProperty("plane_collider2");
            chest_collider2 = serializedObject.FindProperty("chest_collider2");
            chestPanel_collider1 = serializedObject.FindProperty("chestPanel_collider1");
            chestPanel_collider2 = serializedObject.FindProperty("chestPanel_collider2");
            upperleg_collider1 = serializedObject.FindProperty("upperleg_collider1");
            sode_collider = serializedObject.FindProperty("sode_collider");
            plane_collider3 = serializedObject.FindProperty("plane_collider3");
            upperleg_collider2 = serializedObject.FindProperty("upperleg_collider2");
            chest_collider2 = serializedObject.FindProperty("chest_collider2");
            hip_collider1 = serializedObject.FindProperty("hip_collider1");
            chest_collider3 = serializedObject.FindProperty("chest_collider3");
            AFK_collider1 = serializedObject.FindProperty("AFK_collider1");
            plane_collider4 = serializedObject.FindProperty("plane_collider4");
            upperleg_collider3 = serializedObject.FindProperty("upperleg_collider3");
            lowerleg_collider1 = serializedObject.FindProperty("lowerleg_collider1");
            plane_collider5 = serializedObject.FindProperty("plane_collider5");

            textureResize = serializedObject.FindProperty("textureResize");
            AAORemoveFlg = serializedObject.FindProperty("AAORemoveFlg");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
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

            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(knifeFlg, new GUIContent("ナイフギミック削除"));
            EditorGUILayout.PropertyField(knifeFlg2, new GUIContent("  └ ライト削除"));

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
                new GUIContent("なでられギミックをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                kamitukiFlg,
                new GUIContent("噛みつきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                IKUSIA_emote,
                new GUIContent("IKUSIA_emoteをメニューのみ削除")
            );

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            questArea = EditorGUILayout.Foldout(questArea, "Quest用調整項目(素体のみ)", true);
            if (questArea)
            {
                var maoOptimizer = (IllMaoOptimizer)target;
#if AVATAR_OPTIMIZER_FOUND
                if (maoOptimizer.transform.root.GetComponent<TraceAndOptimize>() == null)
                    EditorGUILayout.HelpBox(
                        "アバターにTraceAndOptimizeを追加してください",
                        MessageType.Error
                    );
#else
                EditorGUILayout.HelpBox(
                    "AvatarOptimizerが見つかりませんVCCに追加して有効化してください",
                    MessageType.Error
                );
#endif
                EditorGUILayout.HelpBox(
                    "Quest化に対応してないコンポーネントやシェーダーを使っているためペット、TPS、透視、コライダー・ジャンプ、ホワイトブレス、8bit、ペン操作、ハートガンのparticle、AFKの演出の一部を削除します。\n"
                        + "",
                    MessageType.Info
                );
                EditorGUILayout.PropertyField(questFlg1, new GUIContent("quest用にギミックを削除"));

                if (questFlg1.boolValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    TPSFlg.boolValue = true;
                    knifeFlg2.boolValue = true;
                    ClairvoyanceFlg.boolValue = true;
                    colliderJumpFlg.boolValue = true;
                    backlightFlg.boolValue = true;
                    WhiteBreathFlg.boolValue = true;
                    eightBitFlg.boolValue = true;
                    PenCtrlFlg.boolValue = true;
                    HeartGunFlg.boolValue = true;
                    candyFlg.boolValue = true;
                    serializedObject.ApplyModifiedProperties();
                }
                if (GUILayout.Button("おすすめ設定にする(長髪用)"))
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();

                    Butt.boolValue = true;
                    outer.boolValue = true;
                    Pants.boolValue = true;
                    ahoge.boolValue = true;
                    side_short_root.boolValue = true;
                    side_1_004.boolValue = true;
                    front_root.boolValue = true;
                    back_long_root_001.boolValue = true;
                    ear_hat_006.boolValue = true;
                    glass.boolValue = true;
                    mask.boolValue = true;
                    neckless.boolValue = true;
                    neckless_2.boolValue = true;
                    tail_belt.boolValue = true;
                    side.boolValue = true;


                    Breast.boolValue = false;
                    ear_004.boolValue = false;
                    back_long_C.boolValue = false;
                    back_long_014.boolValue = false;
                    tail.boolValue = false;

                    plane_collider1.boolValue = false;
                    chest_collider1.boolValue = false;
                    plane_collider2.boolValue = false;
                    chest_collider2.boolValue = false;

                    plane_collider3.boolValue = true;
                    upperleg_collider2.boolValue = true;
                    hip_collider1.boolValue = false;
                    chest_collider3.boolValue = true;
                    AFK_collider1.boolValue = true;
                    serializedObject.ApplyModifiedProperties();
                }

                if (GUILayout.Button("おすすめ設定にする(短髪用)"))
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();

                    Butt.boolValue = true;
                    outer.boolValue = true;
                    Pants.boolValue = true;
                    ahoge.boolValue = true;
                    back_long_root_001.boolValue = true;
                    ear_hat_006.boolValue = true;
                    glass.boolValue = true;
                    mask.boolValue = true;
                    neckless.boolValue = true;
                    neckless_2.boolValue = true;
                    tail_belt.boolValue = true;
                    side.boolValue = true;
                    back_long_C.boolValue = true;
                    back_long_014.boolValue = true;
                    side_1_004.boolValue = true;

                    front_root.boolValue = false;
                    side_short_root.boolValue = false;
                    Breast.boolValue = false;
                    ear_004.boolValue = false;
                    tail.boolValue = false;

                    plane_collider1.boolValue = false;
                    chest_collider1.boolValue = false;
                    plane_collider2.boolValue = false;
                    chest_collider2.boolValue = false;

                    plane_collider3.boolValue = false;
                    upperleg_collider2.boolValue = false;
                    hip_collider1.boolValue = false;
                    chest_collider3.boolValue = true;
                    AFK_collider1.boolValue = true;
                    serializedObject.ApplyModifiedProperties();
                }
                if (questFlg1.boolValue)
                {
                    if (ClothFlg.boolValue)
                    {
                        outer.boolValue = true;
                        Pants.boolValue = true;
                    }
                    if (HairFlg.boolValue)
                    {
                        ahoge.boolValue = true;
                        back_long_root_001.boolValue = true;
                        side.boolValue = true;
                        back_long_C.boolValue = true;
                        back_long_014.boolValue = true;
                        side_1_004.boolValue = true;
                        front_root.boolValue = true;
                        side_short_root.boolValue = true;
                    }
                    if (EarTailFlg1.boolValue)
                    {
                        ear_004.boolValue = true;
                    }
                    if (EarTailFlg2.boolValue)
                    {
                        tail.boolValue = true;
                        tail_belt.boolValue = true;
                    }
                }
                PbTransform("お尻", "Butt", Butt);
                PbTransform("胸", "Breast", Breast);
                DisplayColliderSettings(
                    Breast,
                    "Breast",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "腕干渉", upperArm_collider1 },
                    }
                );
                PbTransform("耳", "ear_004", ear_004);
                PbTransform("ニット", "ear_hat_006", ear_hat_006);
                PbTransform("アホ毛", "ahoge", ahoge);
                PbTransform("後ろ髪真ん中", "back_long_C", back_long_C);
                DisplayColliderSettings(
                    back_long_C,
                    "back_long_C",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "胸部干渉", chest_collider1 },
                        { "床干渉", plane_collider1 },
                    }
                );
                PbTransform("後ろ髪左右", "back_long_014", back_long_014);
                DisplayColliderSettings(
                    back_long_014,
                    "back_long_014",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "胸部干渉", chest_collider2 },
                        { "床干渉", plane_collider2 },
                    }
                );
                PbTransform("後ろ髪小", "back_long_root_001", back_long_root_001);
                PbTransform("前髪", "front_root", front_root);
                PbTransform("胸にかかる髪", "side", side);
                DisplayColliderSettings(
                    side,
                    "side",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "胸部干渉", chestPanel_collider1 },
                    }
                );
                PbTransform("Baseの横髪", "side_1_004", side_1_004);
                PbTransform("Baseの肩にかかる髪", "side_short_root", side_short_root);
                DisplayColliderSettings(
                    side_short_root,
                    "side_short_root",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "胸部干渉", chestPanel_collider2 },
                    }
                );
                PbTransform("眼鏡", "glass", glass);
                PbTransform("マスク", "mask", mask);
                PbTransform("チョーカー", "neckless", neckless);
                PbTransform("ネックレス", "neckless_2", neckless_2);
                PbTransform("アウター", "outer", outer);
                DisplayColliderSettings(
                    outer,
                    "outer",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "袖干渉", sode_collider },
                        { "脚干渉", upperleg_collider1 },
                    }
                );

                PbTransform("ボトム", "Pants", Pants);
                DisplayColliderSettings(
                    Pants,
                    "Pants",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "脚干渉", upperleg_collider3 },
                        { "お尻干渉", lowerleg_collider1 },
                        { "地面干渉", plane_collider5 },
                    }
                );
                PbTransform("尻尾", "tail", tail);
                DisplayColliderSettings(
                    tail,
                    "tail",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "地面干渉", plane_collider3 },
                        { "脚干渉", upperleg_collider2 },
                        { "お尻干渉", hip_collider1 },
                        { "胸部干渉", chest_collider3 },
                        { "AFK干渉", AFK_collider1 },
                    }
                );
                PbTransform("尻尾ベルト", "tail_belt", tail_belt);
                DisplayColliderSettings(
                    tail_belt,
                    "tail_belt",
                    new Dictionary<string, SerializedProperty>() { { "地面干渉", plane_collider4 } }
                );

                int count = 257;
                if (Butt.boolValue)
                    count -= physBoneList["Butt"].AffectedCount;
                if (Breast.boolValue)
                    count -= physBoneList["Breast"].AffectedCount;
                if (ear_004.boolValue)
                    count -= physBoneList["ear_004"].AffectedCount;
                if (ear_hat_006.boolValue)
                    count -= physBoneList["ear_hat_006"].AffectedCount;
                if (ahoge.boolValue)
                    count -= physBoneList["ahoge"].AffectedCount;
                if (back_long_C.boolValue)
                    count -= physBoneList["back_long_C"].AffectedCount;
                if (back_long_014.boolValue)
                    count -= physBoneList["back_long_014"].AffectedCount;
                if (back_long_root_001.boolValue)
                    count -= physBoneList["back_long_root_001"].AffectedCount;
                if (front_root.boolValue)
                    count -= physBoneList["front_root"].AffectedCount;
                if (side.boolValue)
                    count -= physBoneList["side"].AffectedCount;
                if (side_1_004.boolValue)
                    count -= physBoneList["side_1_004"].AffectedCount;
                if (side_short_root.boolValue)
                    count -= physBoneList["side_short_root"].AffectedCount;
                if (glass.boolValue)
                    count -= physBoneList["glass"].AffectedCount;
                if (mask.boolValue)
                    count -= physBoneList["mask"].AffectedCount;
                if (neckless.boolValue)
                    count -= physBoneList["neckless"].AffectedCount;
                if (neckless_2.boolValue)
                    count -= physBoneList["neckless_2"].AffectedCount;
                if (outer.boolValue)
                    count -= physBoneList["outer"].AffectedCount;
                if (Pants.boolValue)
                    count -= physBoneList["Pants"].AffectedCount;
                if (tail.boolValue)
                    count -= physBoneList["tail"].AffectedCount;
                if (tail_belt.boolValue)
                    count -= physBoneList["tail_belt"].AffectedCount;

                if (count > 64)
                    EditorGUILayout.HelpBox(
                        "影響transform数 :" + count + "/64 (64以下に調整してください)",
                        MessageType.Error
                    );
                else
                    EditorGUILayout.HelpBox("影響transform数 :" + count + "/64", MessageType.Info);

                int count2 = 290;
                if (upperArm_collider1.boolValue)
                    count2 -= physBoneList["Breast"].ColliderCount;

                if (plane_collider1.boolValue)
                    count2 -= physBoneList["back_long_C"].ColliderCount;
                if (chest_collider1.boolValue)
                    count2 -= physBoneList["back_long_C"].ColliderCount;

                if (plane_collider2.boolValue)
                    count2 -= physBoneList["back_long_014"].ColliderCount;
                if (chest_collider2.boolValue)
                    count2 -= physBoneList["back_long_014"].ColliderCount;

                if (chestPanel_collider1.boolValue)
                    count2 -= physBoneList["side"].ColliderCount;

                if (chestPanel_collider2.boolValue)
                    count2 -= physBoneList["side_short_root"].ColliderCount;

                if (sode_collider.boolValue)
                    count2 -= physBoneList["outer"].ColliderCounts[0];
                if (upperleg_collider1.boolValue)
                    count2 -= physBoneList["outer"].ColliderCounts[1];

                if (plane_collider3.boolValue)
                    count2 -= physBoneList["tail"].ColliderCount;
                if (upperleg_collider2.boolValue)
                    count2 -= physBoneList["tail"].ColliderCount * 2;
                if (hip_collider1.boolValue)
                    count2 -= physBoneList["tail"].ColliderCount;
                if (chest_collider3.boolValue)
                    count2 -= physBoneList["tail"].ColliderCount;
                if (AFK_collider1.boolValue)
                    count2 -= physBoneList["tail"].ColliderCount;

                if (plane_collider4.boolValue)
                    count2 -= physBoneList["Pants"].ColliderCounts[0];
                if (upperleg_collider3.boolValue)
                    count2 -= physBoneList["Pants"].ColliderCounts[1];
                if (lowerleg_collider1.boolValue)
                    count2 -= physBoneList["Pants"].ColliderCounts[2];

                if (plane_collider5.boolValue)
                    count2 -= physBoneList["tail_belt"].ColliderCount;
                if (count2 > 64)
                    EditorGUILayout.HelpBox(
                        "コライダー干渉数 :" + count2 + "/64 (64以下に調整してください)",
                        MessageType.Error
                    );
                else
                    EditorGUILayout.HelpBox(
                        "コライダー干渉数 :" + count2 + "/64",
                        MessageType.Info
                    );
                int selected = textureResize.enumValueIndex;
                textureResize.enumValueIndex = EditorGUILayout.Popup(
                    "メニュー画像解像度設定",
                    selected,
                    new[] { "下げる", "削除" }
                );

#if !AVATAR_OPTIMIZER_FOUND
                GUI.enabled = false;
                EditorGUILayout.HelpBox(
                    "AAOがインストールされている場合のみ「頬染めを削除」が有効になります。",
                    MessageType.Info
                );
#endif
                EditorGUILayout.PropertyField(AAORemoveFlg, new GUIContent("頬染めを削除"));
                GUI.enabled = true;
            }

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

        private void DisplayColliderSettings(
            SerializedProperty pbDelFlg,
            string pbname,
            Dictionary<string, SerializedProperty> ColliderList
        )
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginVertical();
            if (
                physBoneList[pbname].ColliderCount != 0
                && physBoneList[pbname].ColliderCounts == null
            )
                foreach (var item in ColliderList)
                {
                    EditorGUILayout.PropertyField(
                        item.Value,
                        new GUIContent(item.Key + " : " + physBoneList[pbname].ColliderCount)
                    );
                }
            else if (physBoneList[pbname].ColliderCounts != null)
                for (int i = 0; i < ColliderList.Count; i++)
                {
                    var item = ColliderList.ElementAt(i);
                    EditorGUILayout.PropertyField(
                        item.Value,
                        new GUIContent(item.Key + " : " + physBoneList[pbname].ColliderCounts[i])
                    );
                }
            if (pbDelFlg.boolValue)
            {
                foreach (var item in ColliderList)
                {
                    item.Value.boolValue = true;
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private void PbTransform(string name, string pbname, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(
                property,
                new GUIContent(name + ":Transform : " + physBoneList[pbname].AffectedCount)
            );
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
