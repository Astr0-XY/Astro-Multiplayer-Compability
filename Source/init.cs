using System;
using System.Linq;
using HarmonyLib;
using Multiplayer.API;
using Verse;

namespace AstroMultiplayerCompability
{
    [StaticConstructorOnStartup]
    public static class MpInit
    {
        public static readonly Harmony harmony = new Harmony("rimworld.astro.multiplayer.compat");
        static MpInit()
        {
            if (!MP.enabled)
            {
                Log.Warning("[Astro MpCompat] Multiplayer отключен, патчи не применились");
                return;
            }
     
            harmony.PatchAll();

            InitializeModPatches();

            Log.Message("[Astro MpCompat] Архитектурная система успешно запущена!");
        }

        private static void InitializeModPatches()
        {
            var allTypes = AccessTools.GetTypesFromAssembly(typeof(MpInit).Assembly);

            if (allTypes == null)
            {
                Log.Warning("[Astro MpCompat] не обнаружено патчей для применения");
                return;
            }
            

            foreach (var type in allTypes)
            {
                var compatAttribute = type.GetCustomAttributes(typeof(MpCompatForAttribute), false).FirstOrDefault() as MpCompatForAttribute;
                
                if (compatAttribute == null) continue;

                string targetPackageId = compatAttribute.PackageId;

                ModContentPack foundMod = LoadedModManager.RunningModsListForReading
                    .Find(m => m.PackageIdPlayerFacing.ToLower() == targetPackageId);

                if (foundMod != null)
                {
                    try
                    {
                        Activator.CreateInstance(type, new object[] { foundMod });
                        Log.Message($"[Astro MpCompat] Успешно применен патч для мода: {targetPackageId}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"[Astro MpCompat] Ошибка патча для {targetPackageId}: {ex.InnerException?.Message ?? ex.Message}");
                    }
                }
                else
                {
                    Log.Warning($"[Astro MpCompat] мод {targetPackageId} не включен, патч не применен");
                }
            }
        }
    }
}

