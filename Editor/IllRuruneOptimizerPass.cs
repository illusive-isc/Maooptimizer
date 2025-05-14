using nadena.dev.ndmf;
using UnityEngine;

namespace jp.illusive_isc.MaoOptimizer
{
    public class IllMaoOptimizerPass : Pass<IllMaoOptimizerPass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (
                IllMaoOptimizer IllMaoOptimizer in context.AvatarRootObject.transform.GetComponents<IllMaoOptimizer>()
            )
            {
                Object.DestroyImmediate(IllMaoOptimizer.gameObject);
            }
        }
    }
}
