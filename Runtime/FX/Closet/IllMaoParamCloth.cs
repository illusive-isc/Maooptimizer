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
    internal class IllMaoParamCloth : IllMaoParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> MenuParameters = new()
        {
            "mao_Outer",
            "mao_Tsyatu",
            "mao_armcover",
            "mao_gloves",
            "mao_Pants",
            "mao_boots",
        };

        public IllMaoParamCloth Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllMaoParamCloth DeleteFxBT()
        {
            // 1. MainCtrlTree レイヤーを取得
            var targetLayer = animator.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
            if (targetLayer == null)
                return this;

            // 2. 各ステートをループ
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

                // 4. 見つかった "mao closet" の子 children をフィルタ
                maoClosetTree.children = maoClosetTree
                    .children.Where(c => CheckBT(c.motion, MenuParameters))
                    .ToArray();
            }

            return this;
        }

        public IllMaoParamCloth DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllMaoParamCloth DeleteVRCExpressions(
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
                        if (control2.name == "cloth")
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

        public IllMaoParamCloth DestroyObj()
        {
            var underwear = descriptor.transform.Find("underwear");
            if (underwear)
            {
                underwear.gameObject.SetActive(true);
            }
            var tanktop = descriptor.transform.Find("tanktop");
            if (tanktop)
            {
                tanktop.gameObject.SetActive(true);
            }
            DestroyObj(descriptor.transform.Find("outer"));
            DestroyObj(descriptor.transform.Find("Tsyatu"));
            DestroyObj(descriptor.transform.Find("arm cover"));
            DestroyObj(descriptor.transform.Find("gloves"));
            DestroyObj(descriptor.transform.Find("Pants"));
            DestroyObj(descriptor.transform.Find("Boots"));

            return this;
        }
    }
}
#endif
