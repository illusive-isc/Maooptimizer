using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
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
        public AnimatorController controllerDef;
        public VRCExpressionsMenu menuDef;
        public VRCExpressionParameters paramDef;

        public AnimatorController controller;
        public VRCExpressionsMenu menu;
        public VRCExpressionParameters param;

        private string pathDir;

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
            menu = DuplicateExpressionMenu(menuDef, pathDir);

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
            // 新規に複製した AnimatorController をアセットとして保存
            EditorUtility.SetDirty(controller);
            EditorUtility.SetDirty(menu);
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

        /// <summary>
        /// Expression Menu の複製（サブメニューも再帰的に複製）
        /// </summary>
        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath
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
            // サブメニューの複製
            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (control.subMenu != null)
                {
                    string subMenuFolderPath = Path.Combine(menuFolderPath, control.subMenu.name);
                    VRCExpressionsMenu existingSubMenu =
                        AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                            Path.Combine(subMenuFolderPath, control.subMenu.name + ".asset")
                        );
                    if (existingSubMenu == null)
                    {
                        control.subMenu = DuplicateExpressionMenu(control.subMenu, menuFolderPath);
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
