using System.Reflection;
using HarmonyLib;
using Multiplayer.API;
using Multiplayer.Compat; // Подключаем mpcompat
using Verse;

namespace AstroMultiplayerCompability
{
    [MpCompatFor("sunsetmoderteam.sunsettechnology3")] // ID мода из About.xml
    public class SunsetPatch
    {
        public SunsetPatch(ModContentPack mod)
        {
            MpCompat.RegisterLambdaMethod("SUNSET3.Building_TurretGun_cy", "GetGizmos", 0);

            AstroMpCompatUtils.IsolateNRG("SUNSET3.Building_TurretGun_cy:Tick");
            AstroMpCompatUtils.IsolateNRG("SUNSET3.Building_TurretGunHasSpeed_cy:Tick");
            AstroMpCompatUtils.IsolateNRG("SUNSET3.TailBullet_cy:MakeTail");
            AstroMpCompatUtils.IsolateNRG("SUNSET3.ExpansionModule_Turret_cy:TryAttack");

            AstroMpCompatUtils.IsolateNRG("SUNSET3.HediffComp_ConcentrationExplosion_cy:CompPostTick");
            AstroMpCompatUtils.IsolateNRG("SUNSET3.HediffComp_ExecuteOnFailure_cy:CompPostTick");
            AstroMpCompatUtils.IsolateNRG("SUNSET3.HediffComp_WeaponFlash_cy:CompPostTick");

            AstroMpCompatUtils.IsolateNRG("SUNSET3.HediffComp_ConcentrationExplosion_cy:CompPostPostRemoved");
            AstroMpCompatUtils.IsolateNRG("SUNSET3.HediffComp_ExecuteOnFailure_cy:CompPostPostAdd");
        }
    }
}
