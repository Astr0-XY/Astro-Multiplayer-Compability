using HarmonyLib;
using Multiplayer.API;
using Multiplayer.Compat; // Подключаем mpcompat
using Verse;

namespace AstroMultiplayerCompability
{
    [MpCompatFor("sunsetmoderteam.sunsettechnology3")]
    public class SunsetPatch
    {
        public SunsetPatch(ModContentPack mod)
        {
            MpCompat.RegisterLambdaMethod("SUNSET3.Building_TurretGun_cy", "GetGizmos", 0);
            MpCompat.RegisterLambdaMethod("SUNSET3.Building_Excavator_cy", "GetGizmos", 0);

            AstroMpCompatUtils.IsolateNRG("SUNSET3.Building_TurretGunHasSpeed_cy:Tick_Patch1");
            AstroMpCompatUtils.IsolateNRG("SUNSET3.Building_TurretGun_cy:Tick");
            AstroMpCompatUtils.IsolateNRG("SUNSET3.TailBullet_cy_cy:MakeTail");

        }
    }
}
