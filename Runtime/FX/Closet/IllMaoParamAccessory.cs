using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.MaoOptimizer
{
    [AddComponentMenu("")]
    internal class IllMaoParamAccessory : IllMaoParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        bool accessoryFlg1;
        bool accessoryFlg2;
        bool accessoryFlg3;
        bool accessoryFlg4;
        bool accessoryFlg5;
        bool accessoryFlg6;
        bool accessoryFlg7;
        private static readonly List<string> MenuParameters = new()
        {
            "mao_necklace",
            "mao_choker",
            "mao_listband",
            "mao_Nitto",
            "mao_glasses",
            "megane up",
            "mask",
        };

        public IllMaoParamAccessory Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            IllMaoOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            accessoryFlg1 = optimizer.AccessoryFlg1;
            accessoryFlg2 = optimizer.AccessoryFlg2;
            accessoryFlg3 = optimizer.AccessoryFlg3;
            accessoryFlg4 = optimizer.AccessoryFlg4;
            accessoryFlg5 = optimizer.AccessoryFlg5;
            accessoryFlg6 = optimizer.AccessoryFlg6;
            accessoryFlg7 = optimizer.AccessoryFlg7;
            return this;
        }

        public IllMaoParamAccessory DeleteFxBT()
        {
            var targetLayer = animator.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
            if (targetLayer == null)
                return this;

            foreach (var state in targetLayer.stateMachine.states)
            {
                if (state.state.motion is not BlendTree rootTree)
                    continue;
                rootTree.children = rootTree
                    .children.Where(c => CheckBT(c.motion, MenuParameters))
                    .ToArray();
                var maoClosetTree = rootTree
                    .children.Select(c => c.motion)
                    .OfType<BlendTree>()
                    .FirstOrDefault(bt => bt.name == "mao closet");
                if (maoClosetTree != null)
                    maoClosetTree.children = maoClosetTree
                        .children.Where(c => CheckBT(c.motion, MenuParameters))
                        .ToArray();
            }

            return this;
        }

        public IllMaoParamAccessory DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllMaoParamAccessory DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();

            foreach (var control1 in menu.controls)
            {
                if (control1.name == "closet")
                {
                    var expressionsSubMenu1 = control1.subMenu;

                    foreach (var control2 in expressionsSubMenu1.controls)
                    {
                        if (control2.name == "Accessory")
                        {
                            expressionsSubMenu1.controls.Remove(control2);
                            break;
                        }
                    }
                    control1.subMenu = expressionsSubMenu1;
                    break;
                }
            }
            return this;
        }

        public IllMaoParamAccessory ChangeObj(
        )
        {
            if (descriptor.transform.Find("acce") is Transform accessoryObj1)
                accessoryObj1
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(3, accessoryFlg1 ? 0 : 100);
            if (descriptor.transform.Find("acce") is Transform accessoryObj2)
                accessoryObj2
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(2, accessoryFlg2 ? 0 : 100);
            if (descriptor.transform.Find("acce") is Transform accessoryObj3)
                accessoryObj3
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(1, accessoryFlg3 ? 0 : 100);
            if (descriptor.transform.Find("acce") is Transform accessoryObj7)
                accessoryObj7
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(5, accessoryFlg7 ? 0 : 100);
            if (descriptor.transform.Find("hat") is Transform accessoryObj4)
            {
                accessoryObj4.gameObject.SetActive(accessoryFlg4);
            }
            if (descriptor.transform.Find("ear") is Transform itemObj1)
            {
                itemObj1.gameObject.SetActive(!accessoryFlg4);
            }
            if (descriptor.transform.Find("hair_base") is Transform itemObj)
            {
                itemObj
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(3, accessoryFlg4 ? 100 : 0);
            }
            if (
                descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/glass")
                is Transform accessoryObj5
            )
            {
                accessoryObj5.gameObject.SetActive(accessoryFlg5);
                var PC = accessoryObj5.GetComponent<UnityEngine.Animations.ParentConstraint>();
                var Source0 = PC.GetSource(0);
                var Source1 = PC.GetSource(1);
                var Source2 = PC.GetSource(2);

                Source0.weight = accessoryFlg5
                    ? accessoryFlg6
                        ? 0.0f
                        : 1.0f
                    : 0.0f;
                Source1.weight = accessoryFlg6
                    ? accessoryFlg4
                        ? 0.0f
                        : 1.0f
                    : 0.0f;
                Source2.weight = accessoryFlg6
                    ? accessoryFlg4
                        ? 1.0f
                        : 0.0f
                    : 0.0f;
                PC.SetSource(0, Source0);
                PC.SetSource(1, Source1);
                PC.SetSource(2, Source2);
            }

            return this;
        }
    }
}
#endif
