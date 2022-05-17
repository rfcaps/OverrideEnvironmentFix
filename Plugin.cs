using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using JetBrains.Annotations;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;
using IPAConfig = IPA.Config.Config;

namespace OverrideEnvironmentFix
{
    [NoEnableDisable, Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        private const string HarmonyId = "com.BeatSaber.OverrideEnvironmentFix";
        private static readonly Harmony HarmonyInstance = new Harmony(HarmonyId);

        [UsedImplicitly]
        [Init]
        public Plugin(IPALogger logger, IPAConfig config)
        {
            Logger.log = logger;
            logger.Info("OverrideEnvironmentFix initialized");

            PluginConfig.Instance = config.Generated<PluginConfig>();
        }

        [UsedImplicitly]
        [OnEnable]
        public void OnEnable() => HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

        [UsedImplicitly]
        [OnDisable]
        public void OnDisable() => HarmonyInstance.UnpatchSelf();
    }
}