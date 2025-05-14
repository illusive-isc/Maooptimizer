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
    internal class IllMaoParamGam : IllMaoParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> MenuParameters = new() { "gam" };

        public IllMaoParamGam Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllMaoParamGam DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllMaoParamGam DeleteFx()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name is "LipSynk"))
            {
                layer.stateMachine.states = layer
                    .stateMachine.states.Where(state =>
                        !(state.state.name is "mouthgam" or "mouthgam loop" or "mouthgam 0")
                    )
                    .ToArray();
                var states = layer.stateMachine.states;

                foreach (var state in states)
                {
                    if (state.state.name is "off" or "mouth0")
                    {
                        state.state.transitions = state
                            .state.transitions.Where(transition =>
                                transition.destinationState.name is not "mouthgam"
                            )
                            .ToArray();
                    }
                }
                layer.stateMachine.states = states;
            }
            foreach (var layer in animator.layers.Where(layer => layer.name is "Blink_Control"))
            {
                layer.stateMachine.states = layer
                    .stateMachine.states.Where(s => s.state.name != "mouth1")
                    .ToArray();

                var stateMachine = layer.stateMachine;
                stateMachine.anyStateTransitions = stateMachine
                    .anyStateTransitions.Where(t => t.destinationState.name != "mouth1")
                    .ToArray();
            }
            return this;
        }

        public IllMaoParamGam DeleteVRCExpressions(
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
                        if (control2.name == "food")
                        {
                            var expressionsSub2Menu = control2.subMenu;

                            foreach (var control3 in expressionsSub2Menu.controls)
                            {
                                if (control3.name is "gam")
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
