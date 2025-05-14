using System.Collections.Generic;
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
        private static readonly List<string> MenuParameters = new()
        {
            "mao_necklace",
            "mao_choker",
            "mao_listband",
            "mao_Nitto",
            "mao_glasses",
            "megane up",
            "mask",
            "mao mask con",
        };

        public IllMaoParamAccessory Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
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

        public IllMaoParamAccessory DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("acce"));
            DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/glass"));
            DestroyObj(descriptor.transform.Find("hat"));

            return this;
        }
    }
}
#endif
