using System.Collections.Generic;
using System.IO;
using System.Linq;
using Anatawa12.AvatarOptimizer;
using UnityEditor;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.MaoOptimizer
{
    [AddComponentMenu("MaoOptimizer")]
    public class IllMaoOptimizer : MonoBehaviour, IEditorOnly
    {
        // 保存先のパス設定
        string pathDirPrefix = "Assets/MaoOptimizer/";
        string pathDirSuffix = "/FX/";
        string pathName = "paryi_FX.controller";

        [SerializeField]
        private bool ClothFlg0 = false;

        [SerializeField]
        private bool ClothFlg = false;

        public bool ClothFlg1 = false;

        [SerializeField]
        private bool ClothFlg2 = false;

        [SerializeField]
        private bool ClothFlg3 = true;

        [SerializeField]
        private bool ClothFlg4 = false;

        public bool ClothFlg5 = false;

        [SerializeField]
        private bool AccessoryFlg0 = false;

        [SerializeField]
        private bool AccessoryFlg = false;

        [SerializeField]
        private bool AccessoryFlg1 = false;

        [SerializeField]
        private bool AccessoryFlg2 = false;

        [SerializeField]
        private bool AccessoryFlg3 = false;

        [SerializeField]
        private bool AccessoryFlg4 = false;

        [SerializeField]
        private bool AccessoryFlg5 = false;

        [SerializeField]
        private bool AccessoryFlg6 = false;

        [SerializeField]
        private bool AccessoryFlg7 = false;

        [SerializeField]
        private bool EarTailFlg0 = false;

        [SerializeField]
        private bool EarTailFlg1 = false;

        [SerializeField]
        private bool EarTailFlg2 = false;

        [SerializeField]
        private bool EarTailFlg3 = false;

        [SerializeField]
        private bool EarTailFlg4 = false;

        [SerializeField]
        private bool HairFlg0 = false;

        [SerializeField]
        private bool HairFlg1 = false;

        [SerializeField]
        private bool HairFlg2 = false;

        [SerializeField]
        private bool HairFlg = false;

        [SerializeField]
        private bool TailFlg = false;

        [SerializeField]
        private bool knifeFlg = false;

        [SerializeField]
        private bool knifeFlg2 = false;

        [SerializeField]
        private bool TPSFlg = false;

        [SerializeField]
        private bool ClairvoyanceFlg = false;

        [SerializeField]
        private bool colliderJumpFlg = false;

        [SerializeField]
        private bool BreastSizeFlg = false;

        public bool BreastSizeFlg2 = false;

        public bool BreastSizeFlg3 = false;

        public bool BreastSizeFlg4 = false;

        [SerializeField]
        private bool WhiteBreathFlg = false;

        [SerializeField]
        private bool eightBitFlg = false;

        [SerializeField]
        private bool PenCtrlFlg = false;

        [SerializeField]
        private bool HeartGunFlg = false;

        [SerializeField]
        private bool FaceFlg = false;

        [SerializeField]
        private bool FaceGestureFlg = false;

        [SerializeField]
        private bool FaceLockFlg = false;

        [SerializeField]
        private bool FaceValFlg = false;

        [SerializeField]
        private bool kamitukiFlg = false;

        [SerializeField]
        private bool nadeFlg = false;

        [SerializeField]
        private bool candyFlg = false;

        [SerializeField]
        private bool gamFlg = false;

        [SerializeField]
        private bool IKUSIA_emote = false;

        [SerializeField]
        private bool backlightFlg;

        [SerializeField]
        private bool questFlg1 = false;
        public bool Butt;
        public bool Breast;
        public bool ear_004;
        public bool ear_hat_006;
        public bool ahoge;
        public bool back_long_C;
        public bool back_long_014;
        public bool back_long_root_001;
        public bool front_root;
        public bool side;
        public bool side_1_004;
        public bool side_short_root;
        public bool glass;
        public bool mask;
        public bool neckless;
        public bool neckless_2;
        public bool outer;
        public bool tail;
        public bool tail_belt;
        public bool Pants;

        public bool upperArm_collider1;
        public bool plane_collider1;
        public bool chest_collider1;
        public bool plane_collider2;
        public bool chest_collider2;
        public bool chestPanel_collider1;
        public bool chestPanel_collider2;
        public bool sode_collider;
        public bool upperleg_collider1;
        public bool plane_collider3;
        public bool upperleg_collider2;
        public bool hip_collider1;
        public bool chest_collider3;
        public bool AFK_collider1;

        public bool plane_collider4;
        public bool upperleg_collider3;
        public bool lowerleg_collider1;
        public bool plane_collider5;

        public bool AAORemoveFlg;

        [SerializeField]
        bool plane_tail_collider;
        public AnimatorController controllerDef;
        public VRCExpressionsMenu menuDef;
        public VRCExpressionParameters paramDef;

        public AnimatorController controller;
        public VRCExpressionsMenu menu;
        public VRCExpressionParameters param;

        private string pathDir;

        public enum TextureResizeOption
        {
            LowerResolution, // 下げる
            Delete, // 削除
        }

        // Inspector で選択する値
        public TextureResizeOption textureResize = TextureResizeOption.LowerResolution;

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            // 保存先ディレクトリの作成
            pathDir = pathDirPrefix + descriptor.gameObject.name + pathDirSuffix;
            if (AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName) != null)
            {
                AssetDatabase.DeleteAsset(pathDir + pathName);
                AssetDatabase.DeleteAsset(pathDir + "Menu");
                AssetDatabase.DeleteAsset(pathDir + "paryi_paraments.asset");
            }
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }

            // 基本コントローラの参照取得（なければ baseAnimationLayers[4] から取得）
            if (!controllerDef)
            {
                controllerDef =
                    descriptor.baseAnimationLayers[4].animatorController as AnimatorController;
            }
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(controllerDef), pathDir + pathName);

            controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName);

            // ExpressionMenu の複製
            if (!menuDef)
            {
                menuDef = descriptor.expressionsMenu;
            }
            Dictionary<string, string> menu1 = new();
            var iconPath = pathDir + "/icon";
            if (!Directory.Exists(iconPath))
            {
                Directory.CreateDirectory(iconPath);
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir, iconPath, questFlg1, textureResize);

            // ExpressionParameters の複製
            if (!paramDef)
            {
                paramDef = descriptor.expressionParameters;
                paramDef.name = descriptor.expressionParameters.name;
            }
            param = ScriptableObject.CreateInstance<VRCExpressionParameters>();
            EditorUtility.CopySerialized(paramDef, param);
            param.name = paramDef.name;
            EditorUtility.SetDirty(param);
            AssetDatabase.CreateAsset(param, pathDir + param.name + ".asset");
            IllMaoParamDef illMaoParamDef = ScriptableObject.CreateInstance<IllMaoParamDef>();
            illMaoParamDef
                .Initialize(descriptor, controller)
                .DeleteFx()
                .DeleteFxBT()
                .DeleteParam()
                .DeleteVRCExpressions(menu, param)
                .ParticleOptimize()
                .DestroyObj();

            if (ClothFlg0 || ClothFlg)
            {
                IllMaoParamCloth illMaoParamCloth =
                    ScriptableObject.CreateInstance<IllMaoParamCloth>();
                illMaoParamCloth
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
                if (descriptor.transform.Find("tanktop") is Transform itemObj1)
                {
                    itemObj1.gameObject.SetActive(ClothFlg1);
                }
                if (descriptor.transform.Find("Tsyatu") is Transform itemObj2)
                {
                    itemObj2.gameObject.SetActive(ClothFlg5);
                }
                if (ClothFlg)
                    illMaoParamCloth.DestroyObjects(ClothFlg2);
            }

            if (AccessoryFlg0)
            {
                IllMaoParamAccessory illMaoParamAccessory =
                    ScriptableObject.CreateInstance<IllMaoParamAccessory>();
                illMaoParamAccessory
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj(
                        AccessoryFlg1,
                        AccessoryFlg2,
                        AccessoryFlg3,
                        AccessoryFlg4,
                        AccessoryFlg5,
                        AccessoryFlg6,
                        AccessoryFlg7
                    );
            }
            if (EarTailFlg0)
            {
                IllMaoParamEarTail illMaoParamEarTail =
                    ScriptableObject.CreateInstance<IllMaoParamEarTail>();
                illMaoParamEarTail.Initialize(descriptor, controller);
                if (EarTailFlg3)
                    illMaoParamEarTail.DeleteFx();
                illMaoParamEarTail
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param, EarTailFlg3)
                    .DestroyObj(EarTailFlg1, EarTailFlg2, EarTailFlg4);
            }
            if (HairFlg || HairFlg0)
            {
                IllMaoParamHair illMaoParamHair =
                    ScriptableObject.CreateInstance<IllMaoParamHair>();
                illMaoParamHair
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);

                var hair1 = HairFlg1 ? 100 : 0;
                if (descriptor.transform.Find("hair_base") is Transform itemObj)
                {
                    itemObj
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(1, hair1);
                    itemObj
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(2, hair1);
                }

                if (descriptor.transform.Find("hair_long") is Transform itemObj1)
                {
                    itemObj1.gameObject.SetActive(HairFlg2);
                }
                var hair2 = HairFlg2 ? 100 : 0;
                if (descriptor.transform.Find("hat") is Transform hair2Obj2)
                {
                    hair2Obj2
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(1, hair2);
                }
                if (descriptor.transform.Find("hair_base") is Transform hair2Obj)
                {
                    hair2Obj
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(6, hair2);
                    hair2Obj
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(7, hair2);
                    hair2Obj
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(8, hair2);
                }

                if (HairFlg)
                    illMaoParamHair.DestroyObj();
            }
            if (knifeFlg)
            {
                IllMaoParamknife illMaoParamKIllMaoParamknife =
                    ScriptableObject.CreateInstance<IllMaoParamknife>();
                illMaoParamKIllMaoParamknife
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (knifeFlg2)
            {
                IllMaoParam.DestroyObj(
                    descriptor.transform.Find(
                        "Advanced/knife/4/hand/knife position/knife rotation/light/Spot Light"
                    )
                );
                IllMaoParam.DestroyObj(
                    descriptor.transform.Find(
                        "Advanced/knifeL/4/hand/knife position/knife rotation/light/Spot Light"
                    )
                );
            }
            if (TPSFlg)
            {
                IllMaoParamTPS illMaoParamTPS = ScriptableObject.CreateInstance<IllMaoParamTPS>();
                illMaoParamTPS
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (ClairvoyanceFlg)
            {
                IllMaoParamClairvoyance illMaoParamClairvoyance =
                    ScriptableObject.CreateInstance<IllMaoParamClairvoyance>();
                illMaoParamClairvoyance
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (TPSFlg & ClairvoyanceFlg)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "Gimmick")
                    {
                        var expressionsSubMenu = control.subMenu;

                        foreach (var control2 in expressionsSubMenu.controls)
                        {
                            if (control2.name == "camera")
                            {
                                expressionsSubMenu.controls.Remove(control2);
                                break;
                            }
                        }
                        control.subMenu = expressionsSubMenu;
                        break;
                    }
                }
                var targetLayer = controller.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
                foreach (var state in targetLayer.stateMachine.states)
                {
                    if (state.state.motion is not BlendTree rootTree)
                        continue;
                    rootTree.children = rootTree
                        .children.Where(c => !(c.motion.name == "VRMode"))
                        .ToArray();
                }
            }
            if (candyFlg)
            {
                IllMaoParamCandy illMaoParamCIllMaoParamCandy =
                    ScriptableObject.CreateInstance<IllMaoParamCandy>();
                illMaoParamCIllMaoParamCandy
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (gamFlg)
            {
                IllMaoParamGam illMaoParamGamIllMaoParamGam =
                    ScriptableObject.CreateInstance<IllMaoParamGam>();
                illMaoParamGamIllMaoParamGam
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if (candyFlg & gamFlg)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "Gimmick")
                    {
                        var expressionsSubMenu = control.subMenu;

                        foreach (var control2 in expressionsSubMenu.controls)
                        {
                            if (control2.name == "food")
                            {
                                expressionsSubMenu.controls.Remove(control2);
                                break;
                            }
                        }
                        control.subMenu = expressionsSubMenu;
                        break;
                    }
                }
            }
            if (colliderJumpFlg)
            {
                IllMaoParamCollider illMaoParamCollider =
                    ScriptableObject.CreateInstance<IllMaoParamCollider>();
                illMaoParamCollider
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (BreastSizeFlg)
            {
                IllMaoParamBreastSize illMaoParamBreastSize =
                    ScriptableObject.CreateInstance<IllMaoParamBreastSize>();
                illMaoParamBreastSize
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .ChangeObj(BreastSizeFlg2, BreastSizeFlg3, BreastSizeFlg4);
            }
            if (backlightFlg)
            {
                IllMaoParamBacklight illMaoParamBacklight =
                    ScriptableObject.CreateInstance<IllMaoParamBacklight>();
                illMaoParamBacklight
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if (WhiteBreathFlg)
            {
                IllMaoParamWhiteBreath illMaoParamWhiteBreath =
                    ScriptableObject.CreateInstance<IllMaoParamWhiteBreath>();
                illMaoParamWhiteBreath
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (eightBitFlg)
            {
                IllMaoParam8bit illMaoParam8bit =
                    ScriptableObject.CreateInstance<IllMaoParam8bit>();
                illMaoParam8bit
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (HeartGunFlg)
            {
                IllMaoParamHeartGun illMaoParamHeartGun =
                    ScriptableObject.CreateInstance<IllMaoParamHeartGun>();
                illMaoParamHeartGun
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (PenCtrlFlg)
            {
                IllMaoParamPenCtrl illMaoParamPenCtrl =
                    ScriptableObject.CreateInstance<IllMaoParamPenCtrl>();
                illMaoParamPenCtrl
                    .Initialize(descriptor, controller)
                    .DeleteFx(HeartGunFlg)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (HeartGunFlg && PenCtrlFlg && knifeFlg)
            {
                IllMaoParam.DestroyObj(
                    descriptor.transform.Find("Advanced/Constraint/Hand_R_Constraint0")
                );
                IllMaoParam.DestroyObj(
                    descriptor.transform.Find("Advanced/Constraint/Hand_L_Constraint0")
                );
            }

            if (FaceGestureFlg || FaceLockFlg)
            {
                IllMaoParamFaceGesture illMaoParamFaceGesture =
                    ScriptableObject.CreateInstance<IllMaoParamFaceGesture>();
                illMaoParamFaceGesture
                    .Initialize(descriptor, controller, FaceGestureFlg, FaceLockFlg)
                    .DeleteFx()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if (kamitukiFlg || nadeFlg)
            {
                IllMaoParamFaceContact illMaoParamFaceContact =
                    ScriptableObject.CreateInstance<IllMaoParamFaceContact>();
                illMaoParamFaceContact
                    .Initialize(descriptor, controller, kamitukiFlg, nadeFlg)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if ((FaceGestureFlg || FaceLockFlg) && kamitukiFlg && nadeFlg)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "Gimmick")
                    {
                        var expressionsSubMenu = control.subMenu;

                        foreach (var control2 in expressionsSubMenu.controls)
                        {
                            if (control2.name == "Face")
                            {
                                expressionsSubMenu.controls.Remove(control2);
                                break;
                            }
                        }
                        control.subMenu = expressionsSubMenu;
                        break;
                    }
                }
            }

            if (IKUSIA_emote)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "IKUSIA_emote")
                    {
                        menu.controls.Remove(control);
                        break;
                    }
                }
            }

            if (ClothFlg0 && HairFlg0 && AccessoryFlg0 && EarTailFlg0)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "closet")
                    {
                        menu.controls.Remove(control);
                        break;
                    }
                }
                var targetLayer = controller.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
                foreach (var state in targetLayer.stateMachine.states)
                {
                    if (state.state.motion is not BlendTree rootTree)
                        continue;
                    rootTree.children = rootTree
                        .children.Where(c => !(c.motion.name == "mao closet"))
                        .ToArray();
                }
            }
            if (eightBitFlg && PenCtrlFlg && WhiteBreathFlg)
            {
                IllMaoParam.DestroyObj(descriptor.transform.Find("Advanced/Particle"));
                if (HeartGunFlg)
                    foreach (var control in menu.controls)
                    {
                        if (control.name == "Particle")
                        {
                            menu.controls.Remove(control);
                            break;
                        }
                    }
            }
            if (descriptor.transform.Find("body_b") is Transform body_b)
            {
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(4, ClothFlg3 ? 0 : 100);
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(5, ClothFlg3 ? 0 : 100);
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(6, ClothFlg4 ? 100 : 0);
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(7, ClothFlg4 ? 100 : 0);
            }
            if (questFlg1)
            {
                IllMaoParam.DestroyObj(descriptor.transform.Find("Advanced/NadeCamera"));
            }
            if (Butt)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[] { "Armature/Hips/Butt_L", "Armature/Hips/Butt_R" }
                );
            }
            if (Breast)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Breast_L",
                        "Armature/Hips/Spine/Chest/Breast_R",
                    }
                );
            }
            if (upperArm_collider1)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "Upperarm_L", "Upperarm_R" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Breast_L",
                        "Armature/Hips/Spine/Chest/Breast_R",
                    }
                );
            }
            if (ear_004)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/ear_root/ear_L/ear_L.004",
                        "Armature/Hips/Spine/Chest/Neck/Head/ear_root/ear_R/ear_R.004",
                    }
                );
            }
            if (ear_hat_006)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/ear_root_hat/ear_hat_L/ear_hat_L.007/ear_hat_L.006",
                        "Armature/Hips/Spine/Chest/Neck/Head/ear_root_hat/ear_hat_R/ear_hat_R.007/ear_hat_R.006",
                    }
                );
            }
            if (ahoge)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[] { "Armature/Hips/Spine/Chest/Neck/Head/hair_root/ahoge" }
                );
            }
            if (back_long_C)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_C.005/back_long_C",
                    }
                );
            }
            if (plane_collider1)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "plane" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_C.005/back_long_C",
                    }
                );
            }
            if (chest_collider1)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "Chest" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_C.005/back_long_C",
                    }
                );
            }
            if (back_long_014)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_L.010/back_long_L.014",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_R.010/back_long_R.014",
                    }
                );
            }
            if (plane_collider2)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "plane" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_L.010/back_long_L.014",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_R.010/back_long_R.014",
                    }
                );
            }
            if (chest_collider2)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "Chest" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_L.010/back_long_L.014",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_R.010/back_long_R.014",
                    }
                );
            }
            if (back_long_root_001)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_root.001",
                    }
                );
            }
            if (front_root)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[] { "Armature/Hips/Spine/Chest/Neck/Head/hair_root/front_root" }
                );
            }
            if (side)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_long_root/side_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_long_root/side_R",
                    }
                );
            }
            if (chestPanel_collider1)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "collider" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_long_root/side_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_long_root/side_R",
                    }
                );
            }
            if (side_1_004)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_short_root/side_1_L.004",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_short_root/side_1_R.004",
                    }
                );
            }
            if (side_short_root)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_short_root/side_short_root.001",
                    }
                );
            }
            if (chestPanel_collider2)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "collider" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_short_root/side_short_root.001",
                    }
                );
            }
            if (glass)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[] { "Armature/Hips/Spine/Chest/Neck/Head/glass" }
                );
            }
            if (mask)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[] { "Armature/Hips/Spine/Chest/Neck/Head/mask" }
                );
            }
            if (neckless)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[] { "Armature/Hips/Spine/Chest/Neck/neckless" }
                );
            }
            if (neckless_2)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[] { "Armature/Hips/Spine/Chest/neckless_2" }
                );
            }
            if (outer)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/sholder_L/Upperarm_L/Z_sode_1_L",
                        "Armature/Hips/Spine/Chest/sholder_R/Upperarm_R/Z_sode_1_R",
                        "Armature/Hips/Spine/Chest/Z_chest_string_root",
                        "Armature/Hips/Spine/outer",
                    }
                );
            }
            if (sode_collider)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "sode_collider_L", "sode_collider_R" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/sholder_L/Upperarm_L/Z_sode_1_L",
                        "Armature/Hips/Spine/Chest/sholder_R/Upperarm_R/Z_sode_1_R",
                        "Armature/Hips/Spine/Chest/Z_chest_string_root",
                        "Armature/Hips/Spine/outer",
                    }
                );
            }
            if (upperleg_collider1)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "UpperLeg_L_collider", "UpperLeg_R_collider" },
                    new string[]
                    {
                        "Armature/Hips/Spine/Chest/sholder_L/Upperarm_L/Z_sode_1_L",
                        "Armature/Hips/Spine/Chest/sholder_R/Upperarm_R/Z_sode_1_R",
                        "Armature/Hips/Spine/Chest/Z_chest_string_root",
                        "Armature/Hips/Spine/outer",
                    }
                );
            }
            if (Pants)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/String/string_L/string_L.004",
                        "Armature/Hips/String/string_R/string_R.004",
                        "Armature/Hips/Upperleg_L/Lowerleg_L/String_L",
                        "Armature/Hips/Upperleg_L/Pants_hook_L",
                        "Armature/Hips/Upperleg_L/Pants_string_L",
                        "Armature/Hips/Upperleg_R/Lowerleg_R/String_R",
                        "Armature/Hips/Upperleg_R/Pants_hook_R",
                        "Armature/Hips/Upperleg_R/Pants_string_R",
                    }
                );
            }
            if (plane_collider4)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "plane" },
                    new string[]
                    {
                        "Armature/Hips/String/string_L/string_L.004",
                        "Armature/Hips/String/string_R/string_R.004",
                        "Armature/Hips/Upperleg_L/Lowerleg_L/String_L",
                        "Armature/Hips/Upperleg_L/Pants_hook_L",
                        "Armature/Hips/Upperleg_L/Pants_string_L",
                        "Armature/Hips/Upperleg_R/Lowerleg_R/String_R",
                        "Armature/Hips/Upperleg_R/Pants_hook_R",
                        "Armature/Hips/Upperleg_R/Pants_string_R",
                    }
                );
            }
            if (upperleg_collider3)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "Upperleg_L", "Upperleg_R" },
                    new string[]
                    {
                        "Armature/Hips/String/string_L/string_L.004",
                        "Armature/Hips/String/string_R/string_R.004",
                        "Armature/Hips/Upperleg_L/Lowerleg_L/String_L",
                        "Armature/Hips/Upperleg_L/Pants_hook_L",
                        "Armature/Hips/Upperleg_L/Pants_string_L",
                        "Armature/Hips/Upperleg_R/Lowerleg_R/String_R",
                        "Armature/Hips/Upperleg_R/Pants_hook_R",
                        "Armature/Hips/Upperleg_R/Pants_string_R",
                    }
                );
            }
            if (lowerleg_collider1)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "Lowerleg_L", "Lowerleg_R" },
                    new string[]
                    {
                        "Armature/Hips/String/string_L/string_L.004",
                        "Armature/Hips/String/string_R/string_R.004",
                        "Armature/Hips/Upperleg_L/Lowerleg_L/String_L",
                        "Armature/Hips/Upperleg_L/Pants_hook_L",
                        "Armature/Hips/Upperleg_L/Pants_string_L",
                        "Armature/Hips/Upperleg_R/Lowerleg_R/String_R",
                        "Armature/Hips/Upperleg_R/Pants_hook_R",
                        "Armature/Hips/Upperleg_R/Pants_string_R",
                    }
                );
            }

            if (tail)
            {
                DelPBByPathArray(descriptor, new string[] { "Armature/Hips/tail/tail.001" });
            }
            if (plane_collider3)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "plane" },
                    new string[] { "Armature/Hips/tail/tail.001" }
                );
            }
            if (upperleg_collider2)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "Upperleg_L", "Upperleg_R" },
                    new string[] { "Armature/Hips/tail/tail.001" }
                );
            }
            if (hip_collider1)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "Hips" },
                    new string[] { "Armature/Hips/tail/tail.001" }
                );
            }
            if (chest_collider3)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "Chest" },
                    new string[] { "Armature/Hips/tail/tail.001" }
                );
            }
            if (AFK_collider1)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "AFK head collider" },
                    new string[] { "Armature/Hips/tail/tail.001" }
                );
            }

            if (tail_belt)
            {
                DelPBByPathArray(
                    descriptor,
                    new string[]
                    {
                        "Armature/Hips/tail/tail.001/tail.002/tail.003/tail.004/tail.005/tail_belt_L",
                        "Armature/Hips/tail/tail.001/tail.002/tail.003/tail.004/tail.005/tail_belt_R",
                    }
                );
            }

            if (plane_collider5)
            {
                DelColliderSettingByPathArray(
                    descriptor,
                    new string[] { "plane" },
                    new string[]
                    {
                        "Armature/Hips/tail/tail.001/tail.002/tail.003/tail.004/tail.005/tail_belt_L",
                        "Armature/Hips/tail/tail.001/tail.002/tail.003/tail.004/tail.005/tail_belt_R",
                    }
                );
            }
            if (AAORemoveFlg)
            {
#if AVATAR_OPTIMIZER_FOUND
                if (
                    !descriptor
                        .transform.Find("Body")
                        .TryGetComponent<RemoveMeshByBlendShape>(out var removeMesh)
                )
                {
                    removeMesh = descriptor
                        .transform.Find("Body")
                        .gameObject.AddComponent<RemoveMeshByBlendShape>();
                    removeMesh.Initialize(1);
                }
                removeMesh.ShapeKeys.Add("照れ");
#endif
            }
            var assetGuids = AssetDatabase.FindAssets(
                "t:VRCExpressionsMenu",
                new[] { pathDir + "Menu" }
            );

            Dictionary<string, VRCExpressionsMenu> menus = new();
            foreach (var guid in assetGuids)
            {
                menus.Add(
                    guid,
                    AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                        AssetDatabase.GUIDToAssetPath(guid)
                    )
                );
            }
            foreach (var menuItem in menus)
            {
                var delFlg = true;
                if (menuItem.Value.controls.Any(p => p.parameter.name == ""))
                    continue;
                foreach (var control in menuItem.Value.controls)
                    if (!string.IsNullOrEmpty(control.parameter.name))
                        if (param.parameters.Any(p => p.name == control.parameter.name))
                        {
                            delFlg = false;
                            break;
                        }
                if (delFlg)
                    AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(menuItem.Key));
            }
            // 新規に複製した AnimatorController をアセットとして保存
            EditorUtility.SetDirty(controller);
            MarkAllMenusDirty(menu);
            EditorUtility.SetDirty(param);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // AvatarDescriptor への適用と変更登録
            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            EditorUtility.SetDirty(descriptor);

            Debug.Log("最適化を実行しました！");
        }

        private static void DelColliderSettingByPathArray(
            VRCAvatarDescriptor descriptor,
            string[] colliderNames,
            string[] pbPaths
        )
        {
            foreach (var pbPath in pbPaths)
            {
                if (descriptor.transform.Find(pbPath))
                {
                    var physBone = descriptor
                        .transform.Find(pbPath)
                        .GetComponent<VRCPhysBoneBase>();
                    if (physBone != null && physBone.colliders != null)
                    {
                        foreach (var colliderName in colliderNames)
                        {
                            for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                            {
                                var collider = physBone.colliders[i];
                                if (collider != null && collider.name.Contains(colliderName))
                                {
                                    physBone.colliders.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void DelPBByPathArray(VRCAvatarDescriptor descriptor, string[] paths)
        {
            foreach (var path in paths)
            {
                IllMaoParam.DestroyComponent<VRCPhysBoneBase>(descriptor.transform.Find(path));
            }
        }

        private static void MarkAllMenusDirty(VRCExpressionsMenu menu)
        {
            if (menu == null)
                return;

            EditorUtility.SetDirty(menu);

            foreach (var control in menu.controls)
            {
                if (control.subMenu != null)
                {
                    MarkAllMenusDirty(control.subMenu);
                }
            }
        }

        /// <summary>
        /// Expression Menu の複製（サブメニューも再帰的に複製）
        /// </summary>
        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize
        )
        {
            if (originalMenu == null)
            {
                Debug.LogError("元のExpression Menuがありません");
                return null;
            }
            // このメニュー用のフォルダを作成
            string menuFolderPath = Path.Combine(parentPath, originalMenu.name);
            if (!Directory.Exists(menuFolderPath))
            {
                Directory.CreateDirectory(menuFolderPath);
                AssetDatabase.Refresh();
            }
            // メニューの新規保存パス
            string menuAssetPath = Path.Combine(menuFolderPath, originalMenu.name + ".asset");

            VRCExpressionsMenu newMenu = AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                menuAssetPath
            );
            if (newMenu != null)
            {
                return newMenu;
            }
            newMenu = Instantiate(originalMenu);

            // サブメニューの複製とアイコンのディープコピー
            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (questFlg1)
                {
                    if (textureResize == TextureResizeOption.LowerResolution)
                    {
                        var originalControl = originalMenu.controls[i];

                        // --- アイコンのディープコピー処理 ---
                        if (originalControl.icon != null)
                        {
                            string iconAssetPath = AssetDatabase.GetAssetPath(originalControl.icon);
                            if (!string.IsNullOrEmpty(iconAssetPath))
                            {
                                string iconFileName = Path.GetFileName(iconAssetPath);
                                string destPath = Path.Combine(iconPath, iconFileName);
                                // 既にコピー済みでなければコピー
                                if (!File.Exists(destPath))
                                {
                                    File.Copy(iconAssetPath, destPath, true);
                                    AssetDatabase.ImportAsset(destPath);
                                }
                                // コピーしたアイコンをロードしてcontrol.iconにセット
                                var copiedIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(destPath);
                                if (copiedIcon != null)
                                {
                                    // Max Sizeを変更
                                    var importer =
                                        AssetImporter.GetAtPath(destPath) as TextureImporter;
                                    if (importer != null)
                                    {
                                        importer.maxTextureSize = 32;
                                        importer.SaveAndReimport();
                                    }
                                    control.icon = copiedIcon;
                                }
                            }
                        }
                    }
                    else
                    {
                        control.icon = null;
                    }
                }
                // サブメニューの複製
                if (control.subMenu != null)
                {
                    string subMenuFolderPath = Path.Combine(menuFolderPath, control.subMenu.name);
                    VRCExpressionsMenu existingSubMenu =
                        AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                            Path.Combine(subMenuFolderPath, control.subMenu.name + ".asset")
                        );
                    if (existingSubMenu == null)
                    {
                        control.subMenu = DuplicateExpressionMenu(
                            control.subMenu,
                            menuFolderPath,
                            iconPath,
                            questFlg1,
                            textureResize
                        );
                    }
                    else
                    {
                        control.subMenu = existingSubMenu;
                    }
                }
            }
            EditorUtility.SetDirty(newMenu);
            AssetDatabase.CreateAsset(newMenu, menuAssetPath);
            return newMenu;
        }
    }
}
#endif
