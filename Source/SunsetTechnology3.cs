using HarmonyLib;
using Multiplayer.API;
using Multiplayer.Compat;
using Verse;

namespace AstroMultiplayerCompability.Patches
{
    [MpCompatFor("sunsetmoderteam.sunsettechnology3")]
    public class SunsetPatch
    {
        public SunsetPatch(ModContentPack mod)
        {
            MpCompat.RegisterLambdaMethod("SUNSET3.Building_TurretGun_cy", "GetGizmos", 0);
        }
    }
}
