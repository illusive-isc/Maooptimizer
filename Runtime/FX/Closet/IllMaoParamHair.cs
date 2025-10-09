using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.MaoOptimizer
{
    internal class IllMaoParamHair : IllMaoParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        bool HairFlg;
        bool HairFlg1;
        bool HairFlg2;
        private static readonly List<string> MenuParameters = new()
        {
            "mao_HairLong",
            "mao_sidehairL",
            "mao_sidehairR",
            "mao_ponpa",
            "Hair rotation",
        };

        public IllMaoParamHair Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            IllMaoOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            HairFlg = optimizer.HairFlg;
            HairFlg1 = optimizer.HairFlg1;
            HairFlg2 = optimizer.HairFlg2;
            return this;
        }

        public IllMaoParamHair DeleteFxBT()
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

                if (maoClosetTree == null)
                    continue;

                maoClosetTree.children = maoClosetTree
                    .children.Where(c => CheckBT(c.motion, MenuParameters))
                    .ToArray();
            }

            return this;
        }

        public IllMaoParamHair DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllMaoParamHair DeleteVRCExpressions(
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
                        if (control2.name == "Hair")
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

        public IllMaoParamHair ChangeObj()
        {
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
            {
                DestroyObj(descriptor.transform.Find("hair_base"));
                DestroyObj(descriptor.transform.Find("hair_long"));
                DestroyObj(descriptor.transform.Find("Advanced/Hair rotation"));
            }
            return this;
        }
    }
}
#endif
