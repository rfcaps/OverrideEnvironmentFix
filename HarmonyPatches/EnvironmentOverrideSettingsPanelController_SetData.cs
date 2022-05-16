using HarmonyLib;
using JetBrains.Annotations;

namespace OverrideEnvironmentFix.HarmonyPatches
{
    [HarmonyPatch(typeof(EnvironmentOverrideSettingsPanelController), nameof(EnvironmentOverrideSettingsPanelController.SetData))]
    internal class EnvironmentOverrideSettingsPanelController_SetData
    {
        [UsedImplicitly]
        internal static void Prefix(OverrideEnvironmentSettings overrideEnvironmentSettings) => overrideEnvironmentSettings.overrideEnvironments = true;
    }
}
