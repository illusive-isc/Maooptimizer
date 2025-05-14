using jp.illusive_isc.MaoOptimizer;
using nadena.dev.ndmf;

[assembly: ExportsPlugin(typeof(IllMaoOptimizerDifinition))]

namespace jp.illusive_isc.MaoOptimizer
{
    public class IllMaoOptimizerDifinition : Plugin<IllMaoOptimizerDifinition>
    {
        public override string QualifiedName => "IllusoryOverride.IllMaoOptimizer";
        public override string DisplayName => "MaoOptimizer";

        protected override void Configure()
        {
            InPhase(BuildPhase.Resolving)
                .BeforePlugin("com.anatawa12.avatar-optimizer")
                .Run(IllMaoOptimizerPass.Instance);
        }
    }
}
