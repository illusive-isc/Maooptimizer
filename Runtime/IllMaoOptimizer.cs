using System.Collections.Generic;
using System.Diagnostics;
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
        string pathDirPrefix = "Assets/MaoOptimizer/";
        string pathDirSuffix = "/FX/";
        string pathName = "paryi_FX.controller";

        [SerializeField]
        private bool ClothFlg0 = false;

        public bool ClothFlg = false;

        public bool ClothFlg1 = false;

        public bool ClothFlg2 = false;

        [SerializeField]
        private bool ClothFlg3 = true;

        [SerializeField]
        private bool ClothFlg4 = false;

        public bool ClothFlg5 = false;

        [SerializeField]
        private bool AccessoryFlg0 = false;

        [SerializeField]
        private bool AccessoryFlg = false;

        public bool AccessoryFlg1 = false;

        public bool AccessoryFlg2 = false;

        public bool AccessoryFlg3 = false;

        public bool AccessoryFlg4 = false;

        public bool AccessoryFlg5 = false;

        public bool AccessoryFlg6 = false;

        public bool AccessoryFlg7 = false;

        [SerializeField]
        private bool EarTailFlg0 = false;

        public bool EarTailFlg1 = false;

        public bool EarTailFlg2 = false;

        public bool EarTailFlg3 = false;

        public bool EarTailFlg4 = false;

        public bool HairFlg0 = false;

        public bool HairFlg1 = false;

        public bool HairFlg2 = false;

        public bool HairFlg = false;

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

        public bool HeartGunFlg = false;

        [SerializeField]
        private bool FaceFlg = false;

        public bool FaceGestureFlg = false;

        public bool FaceLockFlg = false;

        [SerializeField]
        private bool FaceValFlg = false;

        public bool kamitukiFlg = false;

        public bool nadeFlg = false;

        [SerializeField]
        private bool candyFlg = false;

        [SerializeField]
        private bool gamFlg = false;

        [SerializeField]
        private bool IKUSIA_emote = false;

        [SerializeField]
        private bool backlightFlg;

        public bool questFlg1 = false;
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
            LowerResolution,
            Delete,
        }

        public TextureResizeOption textureResize = TextureResizeOption.LowerResolution;

        private static readonly Dictionary<
            System.Type,
            System.Reflection.MethodInfo[]
        > methodCache = new();

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            var stopwatch = Stopwatch.StartNew();
            var stepTimes = new Dictionary<string, long>();

            var step1 = Stopwatch.StartNew();
            InitializeAssets(descriptor);
            step1.Stop();
            stepTimes["InitializeAssets"] = step1.ElapsedMilliseconds;

            var step2 = Stopwatch.StartNew();
            foreach (var config in GetParamConfigs(descriptor))
            {
                if (config.condition())
                {
                    config.processAction();
                }
                config.afterAction?.Invoke();
            }

            if (TPSFlg & ClairvoyanceFlg)
            {
                var targetLayer = controller.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
                foreach (var state in targetLayer.stateMachine.states)
                {
                    if (state.state.motion is not BlendTree rootTree)
                        continue;

                    var filteredChildren = new List<ChildMotion>();
                    foreach (var child in rootTree.children)
                    {
                        if (child.motion.name != "VRMode")
                        {
                            filteredChildren.Add(child);
                        }
                    }
                    rootTree.children = filteredChildren.ToArray();
                }
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
                var targetLayer = controller.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
                foreach (var state in targetLayer.stateMachine.states)
                {
                    if (state.state.motion is not BlendTree rootTree)
                        continue;

                    var filteredChildren = new List<ChildMotion>();
                    foreach (var child in rootTree.children)
                    {
                        if (child.motion.name != "mao closet")
                        {
                            filteredChildren.Add(child);
                        }
                    }
                    rootTree.children = filteredChildren.ToArray();
                }
            }
            if (eightBitFlg && PenCtrlFlg && WhiteBreathFlg)
            {
                IllMaoParam.DestroyObj(descriptor.transform.Find("Advanced/Particle"));
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

            IllMaoDel4Quest.ProcessPhysicsAndColliders(
                descriptor,
                IllMaoDel4Quest.GetPhysicsSettings(this)
            );

            step2.Stop();
            stepTimes["editProcessing"] = step2.ElapsedMilliseconds;
            var step4 = Stopwatch.StartNew();
            FinalizeAssets(descriptor);
            step4.Stop();
            stepTimes["FinalizeAssets"] = step4.ElapsedMilliseconds;

            stopwatch.Stop();

            UnityEngine.Debug.Log(
                $"最適化を実行しました！総処理時間: {stopwatch.ElapsedMilliseconds}ms ({stopwatch.Elapsed.TotalSeconds:F2}秒)"
            );

            foreach (var kvp in stepTimes)
            {
                UnityEngine.Debug.Log($"[Performance] {kvp.Key}: {kvp.Value}ms");
            }
        }

        private void InitializeAssets(VRCAvatarDescriptor descriptor)
        {
            pathDir = pathDirPrefix + descriptor.gameObject.name + pathDirSuffix;

            var assetsToDelete = new[]
            {
                pathDir + pathName,
                pathDir + "Menu",
                pathDir + "paryi_paraments.asset",
            }
                .Where(path => AssetDatabase.LoadAssetAtPath<Object>(path) != null)
                .ToArray();

            if (assetsToDelete.Length > 0)
            {
                var failedPaths = new List<string>();
                AssetDatabase.DeleteAssets(assetsToDelete, failedPaths);
            }

            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }

            if (!controllerDef)
            {
                controllerDef =
                    descriptor.baseAnimationLayers[4].animatorController as AnimatorController;
            }
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(controllerDef), pathDir + pathName);

            controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName);

            if (!menuDef)
            {
                menuDef = descriptor.expressionsMenu;
            }

            var iconPath = pathDir + "/icon";
            if (!Directory.Exists(iconPath))
            {
                Directory.CreateDirectory(iconPath);
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir, iconPath, questFlg1, textureResize);

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
        }

        private struct ParamProcessConfig
        {
            public System.Func<bool> condition;
            public System.Action processAction;
            public System.Action afterAction;
        }

        private ParamProcessConfig[] GetParamConfigs(VRCAvatarDescriptor descriptor)
        {
            return new ParamProcessConfig[]
            {
                new()
                {
                    condition = () => true,
                    processAction = () => ProcessParam<IllMaoParamDef>(descriptor),
                },
                new()
                {
                    condition = () => ClothFlg0 || ClothFlg,
                    processAction = () => ProcessParam<IllMaoParamCloth>(descriptor),
                },
                new()
                {
                    condition = () => AccessoryFlg0,
                    processAction = () => ProcessParam<IllMaoParamAccessory>(descriptor),
                },
                new()
                {
                    condition = () => EarTailFlg0,
                    processAction = () => ProcessParam<IllMaoParamEarTail>(descriptor),
                },
                new()
                {
                    condition = () => HairFlg || HairFlg0,
                    processAction = () => ProcessParam<IllMaoParamHair>(descriptor),
                },
                new()
                {
                    condition = () => knifeFlg,
                    processAction = () => ProcessParam<IllMaoParamknife>(descriptor),
                    afterAction = () =>
                    {
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
                    },
                },
                new()
                {
                    condition = () => TPSFlg,
                    processAction = () => ProcessParam<IllMaoParamTPS>(descriptor),
                },
                new()
                {
                    condition = () => ClairvoyanceFlg,
                    processAction = () => ProcessParam<IllMaoParamClairvoyance>(descriptor),
                },
                new()
                {
                    condition = () => candyFlg,
                    processAction = () => ProcessParam<IllMaoParamCandy>(descriptor),
                },
                new()
                {
                    condition = () => gamFlg,
                    processAction = () => ProcessParam<IllMaoParamGam>(descriptor),
                },
                new()
                {
                    condition = () => colliderJumpFlg,
                    processAction = () => ProcessParam<IllMaoParamCollider>(descriptor),
                },
                new()
                {
                    condition = () => BreastSizeFlg,
                    processAction = () => ProcessParam<IllMaoParamBreastSize>(descriptor),
                },
                new()
                {
                    condition = () => backlightFlg,
                    processAction = () => ProcessParam<IllMaoParamBacklight>(descriptor),
                },
                new()
                {
                    condition = () => WhiteBreathFlg,
                    processAction = () => ProcessParam<IllMaoParamWhiteBreath>(descriptor),
                },
                new()
                {
                    condition = () => eightBitFlg,
                    processAction = () => ProcessParam<IllMaoParam8bit>(descriptor),
                },
                new()
                {
                    condition = () => HeartGunFlg,
                    processAction = () => ProcessParam<IllMaoParamHeartGun>(descriptor),
                },
                new()
                {
                    condition = () => PenCtrlFlg,
                    processAction = () => ProcessParam<IllMaoParamPenCtrl>(descriptor),
                },
                new()
                {
                    condition = () => FaceGestureFlg || FaceLockFlg,
                    processAction = () => ProcessParam<IllMaoParamFaceGesture>(descriptor),
                },
                new()
                {
                    condition = () => kamitukiFlg || nadeFlg,
                    processAction = () => ProcessParam<IllMaoParamFaceContact>(descriptor),
                },
            };
        }

        private void ProcessParam<T>(VRCAvatarDescriptor descriptor)
            where T : ScriptableObject
        {
            var instance = ScriptableObject.CreateInstance<T>();
            var type = typeof(T);

            if (!methodCache.TryGetValue(type, out var methods))
            {
                methods = new[]
                {
                    type.GetMethod("Initialize"),
                    type.GetMethod("DeleteFx"),
                    type.GetMethod("DeleteFxBT"),
                    type.GetMethod("DeleteParam"),
                    type.GetMethod("DeleteVRCExpressions"),
                    type.GetMethod("ParticleOptimize"),
                    type.GetMethod("ChangeObj"),
                };
                methodCache[type] = methods;
            }

            var initializeMethod = methods[0];
            var deleteFxMethod = methods[1];
            var deleteFxBTMethod = methods[2];
            var deleteParamMethod = methods[3];
            var deleteVRCExpressionsMethod = methods[4];
            var ParticleOptimizeMethod = methods[5];
            var changeObjMethod = methods[6];

            if (initializeMethod != null)
            {
                try
                {
                    int count = initializeMethod.GetParameters().Length;
                    object result = initializeMethod.Invoke(
                        instance,
                        count == 3
                            ? new object[] { descriptor, controller, this }
                            : new object[] { descriptor, controller }
                    );

                    deleteFxMethod?.Invoke(result, null);
                    deleteFxBTMethod?.Invoke(result, null);
                    deleteParamMethod?.Invoke(result, null);
                    deleteVRCExpressionsMethod?.Invoke(result, new object[] { menu, param });
                    ParticleOptimizeMethod?.Invoke(result, null);
                    changeObjMethod?.Invoke(result, null);
                }
                catch (System.Exception ex)
                {
                    UnityEngine.Debug.LogError(
                        $"[ProcessParam] Error processing {type.Name}: {ex.Message}"
                    );
                    UnityEngine.Debug.LogError($"[ProcessParam] Stack trace: {ex.StackTrace}");
                }
            }
        }

        private void FinalizeAssets(VRCAvatarDescriptor descriptor)
        {
            RemoveUnusedMenuControls(menu, param);
            EditorUtility.SetDirty(controller);
            MarkAllMenusDirty(menu);
            EditorUtility.SetDirty(param);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            EditorUtility.SetDirty(descriptor);
        }

        private static void RemoveUnusedMenuControls(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            if (menu == null)
                return;

            for (int i = menu.controls.Count - 1; i >= 0; i--)
            {
                var control = menu.controls[i];
                bool shouldRemove = true;

                if (control.subMenu != null)
                {
                    RemoveUnusedMenuControls(control.subMenu, param);

                    if (control.subMenu.controls.Count == 0)
                    {
                        shouldRemove = true;
                    }
                    else
                    {
                        shouldRemove = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(control.parameter.name))
                    {
                        shouldRemove = false;
                    }
                    else
                    {
                        bool paramExists = param.parameters.Any(p =>
                            p.name == control.parameter.name
                        );
                        if (paramExists)
                        {
                            shouldRemove = false;
                        }
                        else
                        {
                            shouldRemove = true;
                        }
                    }
                }

                if (shouldRemove)
                {
                    menu.controls.RemoveAt(i);
                }
            }
        }

        private static void MarkAllMenusDirty(VRCExpressionsMenu menu)
        {
            if (menu == null)
                return;

            EditorUtility.SetDirty(menu);

            foreach (var control in menu.controls)
                if (control.subMenu != null)
                    MarkAllMenusDirty(control.subMenu);
        }

        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize
        )
        {
            return DuplicateExpressionMenu(
                originalMenu,
                parentPath,
                iconPath,
                questFlg1,
                textureResize,
                null,
                null,
                null
            );
        }

        private static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize,
            VRCExpressionsMenu rootMenuAsset = null,
            Dictionary<VRCExpressionsMenu, VRCExpressionsMenu> processedMenus = null,
            Dictionary<string, Texture2D> processedIcons = null
        )
        {
            if (originalMenu == null)
            {
                UnityEngine.Debug.LogError("元のExpression Menuがありません");
                return null;
            }

            bool isRootCall = processedMenus == null;
            if (isRootCall)
            {
                processedMenus = new Dictionary<VRCExpressionsMenu, VRCExpressionsMenu>();
                processedIcons = new Dictionary<string, Texture2D>();
            }

            if (processedMenus.ContainsKey(originalMenu))
            {
                return processedMenus[originalMenu];
            }

            VRCExpressionsMenu newMenu = Instantiate(originalMenu);
            newMenu.name = originalMenu.name;

            processedMenus[originalMenu] = newMenu;

            if (isRootCall)
            {
                string menuAssetPath = Path.Combine(parentPath, originalMenu.name + ".asset");
                AssetDatabase.CreateAsset(newMenu, menuAssetPath);
                rootMenuAsset = newMenu;
            }
            else if (rootMenuAsset != null)
            {
                AssetDatabase.AddObjectToAsset(newMenu, rootMenuAsset);
            }

            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (questFlg1)
                {
                    if (textureResize == TextureResizeOption.LowerResolution)
                    {
                        var originalControl = originalMenu.controls[i];

                        if (originalControl.icon != null)
                        {
                            string iconAssetPath = AssetDatabase.GetAssetPath(originalControl.icon);
                            if (!string.IsNullOrEmpty(iconAssetPath))
                            {
                                if (processedIcons.ContainsKey(iconAssetPath))
                                {
                                    control.icon = processedIcons[iconAssetPath];
                                }
                                else
                                {
                                    string iconFileName = Path.GetFileName(iconAssetPath);
                                    string destPath = Path.Combine(iconPath, iconFileName);

                                    if (!File.Exists(destPath))
                                    {
                                        File.Copy(iconAssetPath, destPath, true);
                                    }

                                    AssetDatabase.ImportAsset(destPath);
                                    var copiedIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                                        destPath
                                    );

                                    if (copiedIcon != null)
                                    {
                                        var importer =
                                            AssetImporter.GetAtPath(destPath) as TextureImporter;
                                        if (importer != null && importer.maxTextureSize != 32)
                                        {
                                            importer.maxTextureSize = 32;
                                            AssetDatabase.ImportAsset(
                                                destPath,
                                                ImportAssetOptions.ForceUpdate
                                            );
                                        }

                                        if (rootMenuAsset != null)
                                        {
                                            AssetDatabase.AddObjectToAsset(
                                                copiedIcon,
                                                rootMenuAsset
                                            );
                                        }

                                        processedIcons[iconAssetPath] = copiedIcon;
                                        control.icon = copiedIcon;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        control.icon = null;
                    }
                }
                if (control.subMenu != null)
                {
                    control.subMenu = DuplicateExpressionMenu(
                        control.subMenu,
                        parentPath,
                        iconPath,
                        questFlg1,
                        textureResize,
                        rootMenuAsset,
                        processedMenus,
                        processedIcons
                    );
                }
            }
            EditorUtility.SetDirty(newMenu);
            if (isRootCall)
            {
                AssetDatabase.SaveAssets();
            }
            return newMenu;
        }
    }
}
#endif
