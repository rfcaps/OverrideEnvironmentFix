using HarmonyLib;
using JetBrains.Annotations;

namespace OverrideEnvironmentFix.HarmonyPatches
{
	[HarmonyPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.LoadFromCurrentVersion))]
	internal class LoadPatch
	{
		public static bool hasLoaded = false;

		private static void Postfix(ref PlayerSaveData playerSaveData, ref PlayerData __result, ref EnvironmentTypeSO ____normalEnvironmentType, ref EnvironmentTypeSO ____a360DegreesEnvironmentType, ref EnvironmentsListSO ____allEnvironmentInfos)
		{
			__result.overrideEnvironmentSettings.overrideEnvironments = PluginConfig.Instance.overrideEnvironments;
			if (PluginConfig.Instance.environmentInfoBySerializedName != null)
			{
				var environment = ____allEnvironmentInfos.GetEnvironmentInfoBySerializedName(PluginConfig.Instance.environmentInfoBySerializedName);
				__result.overrideEnvironmentSettings.SetEnvironmentInfoForType(____normalEnvironmentType, environment);
			}
			if (PluginConfig.Instance.environmentInfoBySerializedName2 != null)
            {
				var environment = ____allEnvironmentInfos.GetEnvironmentInfoBySerializedName(PluginConfig.Instance.environmentInfoBySerializedName2);
				__result.overrideEnvironmentSettings.SetEnvironmentInfoForType(____a360DegreesEnvironmentType, environment);
			}
			hasLoaded = true;
		}
	}

	[HarmonyPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.Save))]
	internal class SavePatch
    {
		private static void Postfix(PlayerData playerData, ref EnvironmentTypeSO ____normalEnvironmentType, ref EnvironmentTypeSO ____a360DegreesEnvironmentType)
        {
			if (LoadPatch.hasLoaded)
			{
				PluginConfig.Instance.overrideEnvironments = playerData.overrideEnvironmentSettings.overrideEnvironments;
				PluginConfig.Instance.environmentInfoBySerializedName = playerData.overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(____normalEnvironmentType).serializedName;
				PluginConfig.Instance.environmentInfoBySerializedName2 = playerData.overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(____a360DegreesEnvironmentType).serializedName;
			}
		}
    }
}
