#if UNITY_EDITOR
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.Dynamics;
#if AVATAR_OPTIMIZER_FOUND
using Anatawa12.AvatarOptimizer;
#endif
namespace jp.illusive_isc.MaoOptimizer
{
    [AddComponentMenu("")]
    public class IllMaoDel4Quest : ScriptableObject
    {
        public static void ProcessPhysicsAndColliders(
            VRCAvatarDescriptor descriptor,
            PhysicsSettings settings
        )
        {
            foreach (var config in GetProcessConfigs())
                if (config.condition(settings))
                    config.action(descriptor);

            foreach (var config in GetPhysBoneConfigs())
                if (config.condition(settings))
                    DelPBByPathArray(descriptor, config.paths);

            foreach (var config in GetColliderConfigs())
                if (config.condition(settings))
                    DelColliderSettingByPathArray(descriptor, config.colliderNames, config.pbPaths);

            // AAO処理
            if (settings.AAORemoveFlg)
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
        }

        private struct ProcessConfig
        {
            public System.Func<PhysicsSettings, bool> condition;
            public System.Action<VRCAvatarDescriptor> action;
            public string description;
        }

        private struct PhysBoneConfig
        {
            public System.Func<PhysicsSettings, bool> condition;
            public string[] paths;
            public string description;
        }

        private struct ColliderConfig
        {
            public System.Func<PhysicsSettings, bool> condition;
            public string[] colliderNames;
            public string[] pbPaths;
            public string description;
        }

        public struct PhysicsSettings
        {
            public bool questFlg1;
            public bool Butt;
            public bool Breast;
            public bool upperArm_collider1;
            public bool ear_004;
            public bool ear_hat_006;
            public bool ahoge;
            public bool back_long_C;
            public bool plane_collider1;
            public bool chest_collider1;
            public bool back_long_014;
            public bool plane_collider2;
            public bool chest_collider2;
            public bool back_long_root_001;
            public bool front_root;
            public bool side;
            public bool chestPanel_collider1;
            public bool side_1_004;
            public bool side_short_root;
            public bool chestPanel_collider2;
            public bool glass;
            public bool mask;
            public bool neckless;
            public bool neckless_2;
            public bool outer;
            public bool sode_collider;
            public bool upperleg_collider1;
            public bool Pants;
            public bool plane_collider4;
            public bool upperleg_collider3;
            public bool lowerleg_collider1;
            public bool tail;
            public bool plane_collider3;
            public bool upperleg_collider2;
            public bool hip_collider1;
            public bool chest_collider3;
            public bool AFK_collider1;
            public bool tail_belt;
            public bool plane_collider5;
            public bool AAORemoveFlg;
        }

        public static PhysicsSettings GetPhysicsSettings(IllMaoOptimizer optimizer)
        {
            return new PhysicsSettings
            {
                questFlg1 = optimizer.questFlg1,
                Butt = optimizer.Butt,
                Breast = optimizer.Breast,
                upperArm_collider1 = optimizer.upperArm_collider1,
                ear_004 = optimizer.ear_004,
                ear_hat_006 = optimizer.ear_hat_006,
                ahoge = optimizer.ahoge,
                back_long_C = optimizer.back_long_C,
                plane_collider1 = optimizer.plane_collider1,
                chest_collider1 = optimizer.chest_collider1,
                back_long_014 = optimizer.back_long_014,
                plane_collider2 = optimizer.plane_collider2,
                chest_collider2 = optimizer.chest_collider2,
                back_long_root_001 = optimizer.back_long_root_001,
                front_root = optimizer.front_root,
                side = optimizer.side,
                chestPanel_collider1 = optimizer.chestPanel_collider1,
                side_1_004 = optimizer.side_1_004,
                side_short_root = optimizer.side_short_root,
                chestPanel_collider2 = optimizer.chestPanel_collider2,
                glass = optimizer.glass,
                mask = optimizer.mask,
                neckless = optimizer.neckless,
                neckless_2 = optimizer.neckless_2,
                outer = optimizer.outer,
                sode_collider = optimizer.sode_collider,
                upperleg_collider1 = optimizer.upperleg_collider1,
                Pants = optimizer.Pants,
                plane_collider4 = optimizer.plane_collider4,
                upperleg_collider3 = optimizer.upperleg_collider3,
                lowerleg_collider1 = optimizer.lowerleg_collider1,
                tail = optimizer.tail,
                plane_collider3 = optimizer.plane_collider3,
                upperleg_collider2 = optimizer.upperleg_collider2,
                hip_collider1 = optimizer.hip_collider1,
                chest_collider3 = optimizer.chest_collider3,
                AFK_collider1 = optimizer.AFK_collider1,
                tail_belt = optimizer.tail_belt,
                plane_collider5 = optimizer.plane_collider5,
                AAORemoveFlg = optimizer.AAORemoveFlg,
            };
        }

        private static ProcessConfig[] GetProcessConfigs()
        {
            return new ProcessConfig[]
            {
                new()
                {
                    condition = s => s.questFlg1,
                    action = d => IllMaoParam.DestroyObj(d.transform.Find("Advanced/NadeCamera")),
                },
            };
        }

        private static PhysBoneConfig[] GetPhysBoneConfigs()
        {
            return new PhysBoneConfig[]
            {
                new()
                {
                    condition = s => s.Butt,
                    paths = new[] { "Armature/Hips/Butt_L", "Armature/Hips/Butt_R" },
                },
                new()
                {
                    condition = s => s.Breast,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Breast_L",
                        "Armature/Hips/Spine/Chest/Breast_R",
                    },
                },
                new()
                {
                    condition = s => s.ear_004,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/ear_root/ear_L/ear_L.004",
                        "Armature/Hips/Spine/Chest/Neck/Head/ear_root/ear_R/ear_R.004",
                    },
                },
                new()
                {
                    condition = s => s.ear_hat_006,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/ear_root_hat/ear_hat_L/ear_hat_L.007/ear_hat_L.006",
                        "Armature/Hips/Spine/Chest/Neck/Head/ear_root_hat/ear_hat_R/ear_hat_R.007/ear_hat_R.006",
                    },
                },
                new()
                {
                    condition = s => s.ahoge,
                    paths = new[] { "Armature/Hips/Spine/Chest/Neck/Head/hair_root/ahoge" },
                },
                new()
                {
                    condition = s => s.back_long_C,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_C.005/back_long_C",
                    },
                },
                new()
                {
                    condition = s => s.back_long_014,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_L.010/back_long_L.014",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_R.010/back_long_R.014",
                    },
                },
                new()
                {
                    condition = s => s.back_long_root_001,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_root.001",
                    },
                },
                new()
                {
                    condition = s => s.front_root,
                    paths = new[] { "Armature/Hips/Spine/Chest/Neck/Head/hair_root/front_root" },
                },
                new()
                {
                    condition = s => s.side,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_long_root/side_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_long_root/side_R",
                    },
                },
                new()
                {
                    condition = s => s.side_1_004,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_short_root/side_1_L.004",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_short_root/side_1_R.004",
                    },
                },
                new()
                {
                    condition = s => s.side_short_root,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_short_root/side_short_root.001",
                    },
                },
                new()
                {
                    condition = s => s.glass,
                    paths = new[] { "Armature/Hips/Spine/Chest/Neck/Head/glass" },
                },
                new()
                {
                    condition = s => s.mask,
                    paths = new[] { "Armature/Hips/Spine/Chest/Neck/Head/mask" },
                },
                new()
                {
                    condition = s => s.neckless,
                    paths = new[] { "Armature/Hips/Spine/Chest/Neck/neckless" },
                },
                new()
                {
                    condition = s => s.neckless_2,
                    paths = new[] { "Armature/Hips/Spine/Chest/neckless_2" },
                },
                new()
                {
                    condition = s => s.outer,
                    paths = new[]
                    {
                        "Armature/Hips/Spine/Chest/sholder_L/Upperarm_L/Z_sode_1_L",
                        "Armature/Hips/Spine/Chest/sholder_R/Upperarm_R/Z_sode_1_R",
                        "Armature/Hips/Spine/Chest/Z_chest_string_root",
                        "Armature/Hips/Spine/outer",
                    },
                },
                new()
                {
                    condition = s => s.Pants,
                    paths = new[]
                    {
                        "Armature/Hips/String/string_L/string_L.004",
                        "Armature/Hips/String/string_R/string_R.004",
                        "Armature/Hips/Upperleg_L/Lowerleg_L/String_L",
                        "Armature/Hips/Upperleg_L/Pants_hook_L",
                        "Armature/Hips/Upperleg_L/Pants_string_L",
                        "Armature/Hips/Upperleg_R/Lowerleg_R/String_R",
                        "Armature/Hips/Upperleg_R/Pants_hook_R",
                        "Armature/Hips/Upperleg_R/Pants_string_R",
                    },
                },
                new() { condition = s => s.tail, paths = new[] { "Armature/Hips/tail/tail.001" } },
                new()
                {
                    condition = s => s.tail_belt,
                    paths = new[]
                    {
                        "Armature/Hips/tail/tail.001/tail.002/tail.003/tail.004/tail.005/tail_belt_L",
                        "Armature/Hips/tail/tail.001/tail.002/tail.003/tail.004/tail.005/tail_belt_R",
                    },
                },
            };
        }

        private static ColliderConfig[] GetColliderConfigs()
        {
            return new ColliderConfig[]
            {
                new()
                {
                    condition = s => s.upperArm_collider1,
                    colliderNames = new[] { "Upperarm_L", "Upperarm_R" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Breast_L",
                        "Armature/Hips/Spine/Chest/Breast_R",
                    },
                },
                new()
                {
                    condition = s => s.plane_collider1,
                    colliderNames = new[] { "plane" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_C.005/back_long_C",
                    },
                },
                new()
                {
                    condition = s => s.chest_collider1,
                    colliderNames = new[] { "Chest" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_C.005/back_long_C",
                    },
                },
                new()
                {
                    condition = s => s.plane_collider2,
                    colliderNames = new[] { "plane" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_L.010/back_long_L.014",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_R.010/back_long_R.014",
                    },
                },
                new()
                {
                    condition = s => s.chest_collider2,
                    colliderNames = new[] { "Chest" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_L.010/back_long_L.014",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/back_long_root/back_long_R.010/back_long_R.014",
                    },
                },
                new()
                {
                    condition = s => s.chestPanel_collider1,
                    colliderNames = new[] { "collider" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_long_root/side_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_long_root/side_R",
                    },
                },
                new()
                {
                    condition = s => s.chestPanel_collider2,
                    colliderNames = new[] { "collider" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/Neck/Head/hair_root/side_short_root/side_short_root.001",
                    },
                },
                new()
                {
                    condition = s => s.sode_collider,
                    colliderNames = new[] { "sode_collider_L", "sode_collider_R" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/sholder_L/Upperarm_L/Z_sode_1_L",
                        "Armature/Hips/Spine/Chest/sholder_R/Upperarm_R/Z_sode_1_R",
                        "Armature/Hips/Spine/Chest/Z_chest_string_root",
                        "Armature/Hips/Spine/outer",
                    },
                },
                new()
                {
                    condition = s => s.upperleg_collider1,
                    colliderNames = new[] { "UpperLeg_L_collider", "UpperLeg_R_collider" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/Spine/Chest/sholder_L/Upperarm_L/Z_sode_1_L",
                        "Armature/Hips/Spine/Chest/sholder_R/Upperarm_R/Z_sode_1_R",
                        "Armature/Hips/Spine/Chest/Z_chest_string_root",
                        "Armature/Hips/Spine/outer",
                    },
                },
                new()
                {
                    condition = s => s.plane_collider4,
                    colliderNames = new[] { "plane" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/String/string_L/string_L.004",
                        "Armature/Hips/String/string_R/string_R.004",
                        "Armature/Hips/Upperleg_L/Lowerleg_L/String_L",
                        "Armature/Hips/Upperleg_L/Pants_hook_L",
                        "Armature/Hips/Upperleg_L/Pants_string_L",
                        "Armature/Hips/Upperleg_R/Lowerleg_R/String_R",
                        "Armature/Hips/Upperleg_R/Pants_hook_R",
                        "Armature/Hips/Upperleg_R/Pants_string_R",
                    },
                },
                new()
                {
                    condition = s => s.upperleg_collider3,
                    colliderNames = new[] { "Upperleg_L", "Upperleg_R" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/String/string_L/string_L.004",
                        "Armature/Hips/String/string_R/string_R.004",
                        "Armature/Hips/Upperleg_L/Lowerleg_L/String_L",
                        "Armature/Hips/Upperleg_L/Pants_hook_L",
                        "Armature/Hips/Upperleg_L/Pants_string_L",
                        "Armature/Hips/Upperleg_R/Lowerleg_R/String_R",
                        "Armature/Hips/Upperleg_R/Pants_hook_R",
                        "Armature/Hips/Upperleg_R/Pants_string_R",
                    },
                },
                new()
                {
                    condition = s => s.lowerleg_collider1,
                    colliderNames = new[] { "Lowerleg_L", "Lowerleg_R" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/String/string_L/string_L.004",
                        "Armature/Hips/String/string_R/string_R.004",
                        "Armature/Hips/Upperleg_L/Lowerleg_L/String_L",
                        "Armature/Hips/Upperleg_L/Pants_hook_L",
                        "Armature/Hips/Upperleg_L/Pants_string_L",
                        "Armature/Hips/Upperleg_R/Lowerleg_R/String_R",
                        "Armature/Hips/Upperleg_R/Pants_hook_R",
                        "Armature/Hips/Upperleg_R/Pants_string_R",
                    },
                },
                new()
                {
                    condition = s => s.plane_collider3,
                    colliderNames = new[] { "plane" },
                    pbPaths = new[] { "Armature/Hips/tail/tail.001" },
                },
                new()
                {
                    condition = s => s.upperleg_collider2,
                    colliderNames = new[] { "Upperleg_L", "Upperleg_R" },
                    pbPaths = new[] { "Armature/Hips/tail/tail.001" },
                },
                new()
                {
                    condition = s => s.hip_collider1,
                    colliderNames = new[] { "Hips" },
                    pbPaths = new[] { "Armature/Hips/tail/tail.001" },
                },
                new()
                {
                    condition = s => s.chest_collider3,
                    colliderNames = new[] { "Chest" },
                    pbPaths = new[] { "Armature/Hips/tail/tail.001" },
                },
                new()
                {
                    condition = s => s.AFK_collider1,
                    colliderNames = new[] { "AFK head collider" },
                    pbPaths = new[] { "Armature/Hips/tail/tail.001" },
                },
                new()
                {
                    condition = s => s.plane_collider5,
                    colliderNames = new[] { "plane" },
                    pbPaths = new[]
                    {
                        "Armature/Hips/tail/tail.001/tail.002/tail.003/tail.004/tail.005/tail_belt_L",
                        "Armature/Hips/tail/tail.001/tail.002/tail.003/tail.004/tail.005/tail_belt_R",
                    },
                },
            };
        }

        public static void DelColliderSettingByPathArray(
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

        public static void DelPBByPathArray(VRCAvatarDescriptor descriptor, string[] paths)
        {
            foreach (var path in paths)
            {
                IllMaoParam.DestroyComponent<VRCPhysBoneBase>(descriptor.transform.Find(path));
            }
        }
    }
}
#endif
