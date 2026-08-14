using HarmonyLib;
using Multiplayer.API;
using Verse;
using System;

namespace AstroMultiplayerCompability
{
    // Инициализация при старте игры
    [StaticConstructorOnStartup]
    public static class MpInit
    {
        static MpInit()
        {
            // Проверяем, запущен ли мультиплеер
            if (!MP.enabled) return;

            // Регистрируем гармонию для нашего патча
            var harmony = new Harmony("com.astro.mpcompat.puppeteer");
            
            Log.Message("[Astro MpCompat] Мод-патч успешно загружен и готов к работе!");
        }
    }
}
