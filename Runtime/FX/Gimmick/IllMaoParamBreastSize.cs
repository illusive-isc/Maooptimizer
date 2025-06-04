using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.MaoOptimizer
{
    [AddComponentMenu("")]
    internal class IllMaoParamBreastSize : IllMaoParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> MenuParameters = new() { "BreastSize" };

        public IllMaoParamBreastSize Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllMaoParamBreastSize DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllMaoParamBreastSize DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => CheckBT(c.motion, MenuParameters))
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public IllMaoParamBreastSize DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Breast_size")
                        {
                            expressionsSubMenu.controls.Remove(control2);
                            break;
                        }
                    }
                    control.subMenu = expressionsSubMenu;
                    break;
                }
            }
            return this;
        }

        public IllMaoParamBreastSize ChangeObj(
            bool BreastSizeFlg2,
            bool BreastSizeFlg3,
            bool BreastSizeFlg4
        )
        {
            var Chest = descriptor.transform.Find("Armature/Hips/Spine/Chest");
            Chest.Find("Breast_L").GetComponent<VRCPhysBoneBase>().enabled =
                BreastSizeFlg2 || BreastSizeFlg3;
            Chest.Find("Breast_R").GetComponent<VRCPhysBoneBase>().enabled =
                BreastSizeFlg2 || BreastSizeFlg3;

            Dictionary<string, int> dict = new()
            {
                { "acce", 4 },
                { "hair_long", 3 },
                { "body_b", 10 },
                { "outer", 2 },
                { "tanktop", 2 },
                { "Tsyatu", 3 },
            };

            foreach (var item in dict)
            {
                if (descriptor.transform.Find(item.Key) is Transform itemObj)
                {
                    itemObj
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(item.Value, BreastSizeFlg2 ? 100 : 0);
                }
            }

            Dictionary<string, int> dict1 = new()
            {
                { "body_b", 9 },
                { "outer", 1 },
                { "tanktop", 1 },
                { "Tsyatu", 2 },
            };
            foreach (var item in dict1)
            {
                if (descriptor.transform.Find(item.Key) is Transform itemObj)
                {
                    itemObj
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(item.Value, BreastSizeFlg4 ? 100 : 0);
                }
            }
            if (descriptor.transform.Find("body_b") is Transform body_b)
            {
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(11, BreastSizeFlg3 ? 100 : 0);
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(21, BreastSizeFlg4 ? 100 : 0);
            }
            return this;
        }
    }
}
#endif
