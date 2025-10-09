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
    internal class IllMaoParamEarTail : IllMaoParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        bool EarTailFlg1 = false;

        bool EarTailFlg2 = false;

        bool EarTailFlg3 = false;

        bool EarTailFlg4 = false;
        private static readonly List<string> MenuParameters = new()
        {
            "mao_TailBelt",
            "mao_ear",
            "mao_tail",
        };

        private static readonly List<string> Layers = new() { "mao_tailrotation" };

        public IllMaoParamEarTail Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            IllMaoOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            EarTailFlg1 = optimizer.EarTailFlg1;
            EarTailFlg2 = optimizer.EarTailFlg2;
            EarTailFlg3 = optimizer.EarTailFlg3;
            EarTailFlg4 = optimizer.EarTailFlg4;
            return this;
        }

        public IllMaoParamEarTail DeleteFx()
        {
            if (!EarTailFlg3)
                return this;
            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();

            return this;
        }

        public IllMaoParamEarTail DeleteFxBT()
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

        public IllMaoParamEarTail DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllMaoParamEarTail DeleteVRCExpressions(
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
                        if (control2.name == "ear tail")
                        {
                            expressionsSubMenu1.controls.Remove(control2);
                            break;
                        }
                    }
                    control1.subMenu = expressionsSubMenu1;
                    break;
                }
            }
            if (EarTailFlg3)
                foreach (var control1 in menu.controls)
                {
                    if (control1.name == "Gimmick")
                    {
                        var expressionsSubMenu1 = control1.subMenu;

                        foreach (var control2 in expressionsSubMenu1.controls)
                        {
                            if (control2.name == "tail")
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

        public IllMaoParamEarTail ChangeObj()
        {
            if (EarTailFlg1)
                DestroyObj(descriptor.transform.Find("ear"));

            if (EarTailFlg4)
            {
                if (descriptor.transform.Find("tail") is Transform Obj)
                    Obj.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                DestroyObj(descriptor.transform.Find("tail_belt"));
            }
            if (EarTailFlg2)
            {
                if (descriptor.transform.Find("outer") is Transform outer)
                    outer
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(3, 100);
                DestroyObj(descriptor.transform.Find("tail"));
            }
            return this;
        }
    }
}
#endif
