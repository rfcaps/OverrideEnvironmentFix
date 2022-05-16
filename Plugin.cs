using HarmonyLib;
using IPA;
using JetBrains.Annotations;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace OverrideEnvironmentFix
{
    [NoEnableDisable, Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        private const string HarmonyId = "com.BeatSaber.OverrideEnvironmentFix";
        private static readonly Harmony HarmonyInstance = new Harmony(HarmonyId);

        [UsedImplicitly]
        [Init]
        public Plugin(IPALogger logger)
        {
            Logger.log = logger;
            logger.Info("OverrideEnvironmentFix initialized");
        }

        [UsedImplicitly]
        [OnEnable]
        public void OnEnable() => HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

        [UsedImplicitly]
        [OnDisable]
        public void OnDisable() => HarmonyInstance.UnpatchSelf();
    }
}