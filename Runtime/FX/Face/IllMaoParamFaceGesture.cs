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
    internal class IllMaoParamFaceGesture : IllMaoParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        HashSet<string> paramList = new();

        public bool FaceGestureFlg = false;
        public bool FaceLockFlg = false;
        private static readonly List<string> Layers = new() { "LeftHand", "RightHand" };
        private static readonly List<string> MenuParameters = new()
        {
            "FaceLock",
            "Face_variation",
        };

        public IllMaoParamFaceGesture Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            bool FaceGestureFlg,
            bool FaceLockFlg
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            this.FaceGestureFlg = FaceGestureFlg;
            this.FaceLockFlg = FaceLockFlg;
            return this;
        }

        public IllMaoParamFaceGesture DeleteFx()
        {
            if (FaceGestureFlg || FaceLockFlg)
                foreach (
                    var layer in animator.layers.Where(layer =>
                        layer.name is "LeftHand" or "RightHand" or "Blink_Control"
                    )
                )
                {
                    if (layer.name is "Blink_Control")
                    {
                        var states = layer.stateMachine.states;

                        foreach (var state in states)
                        {
                            if (state.state.name == "blinkctrl")
                            {
                                foreach (var t in state.state.transitions)
                                    t.conditions = t
                                        .conditions.Where(c => c.parameter != "FaceLock")
                                        .ToArray();
                            }
                        }
                        layer.stateMachine.states = states;
                    }

                    var stateMachine = layer.stateMachine;
                    foreach (var t in stateMachine.anyStateTransitions)
                        t.conditions = t.conditions.Where(c => c.parameter != "FaceLock").ToArray();
                }

            if (!FaceGestureFlg)
                return this;
            var removedLayers = animator
                .layers.Where(layer => Layers.Contains(layer.name))
                .ToList();

            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();
            return this;
        }

        public IllMaoParamFaceGesture DeleteParam()
        {
            if (FaceGestureFlg || FaceLockFlg)
                animator.parameters = animator
                    .parameters.Where(p => !(p.name == "FaceLock"))
                    .ToArray();
            return this;
        }

        public IllMaoParamFaceGesture DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            if (FaceGestureFlg || FaceLockFlg)
                param.parameters = param.parameters.Where(p => !(p.name == "FaceLock")).ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Face")
                        {
                            var expressionsSub2Menu = control2.subMenu;
                            if (FaceGestureFlg || FaceLockFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "FaceLock")
                                    {
                                        expressionsSub2Menu.controls.Remove(control3);
                                        break;
                                    }
                                }
                            control2.subMenu = expressionsSub2Menu;
                            break;
                        }
                    }
                    control.subMenu = expressionsSubMenu;
                    break;
                }
            }
            return this;
        }
    }
}
#endif
