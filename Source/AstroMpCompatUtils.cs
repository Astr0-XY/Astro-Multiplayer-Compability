using System;
using System.Linq;
using HarmonyLib;
using Multiplayer.API;
using Multiplayer.Compat;
using Verse;

namespace AstroMultiplayerCompability
{
    public class AstroMpCompatUtils
    {
        public static void IsolateNRG(string MethodName)
        {
            var IsolateMethod = AccessTools.Method(MethodName);
            if (IsolateMethod == null) return;

            var prefix = new HarmonyMethod(typeof(AstroMpCompatUtils), nameof(IsolatePrefix));
            var postfix = new HarmonyMethod(typeof(AstroMpCompatUtils), nameof(IsolatePostfix));

            MpCompat.harmony.Patch(IsolateMethod, prefix, postfix);
        }

        static void IsolatePrefix(Thing __instance)
        {
            if (MP.IsInMultiplayer && __instance != null)
            {
                Rand.PushState(__instance.thingIDNumber);
            }
        }

        static void IsolatePostfix()
        {
            if (MP.IsInMultiplayer)
            {
                Rand.PopState();
            }
        }

        // Чистый сканер-информатор
        public static void ScanLambdaIndices(string className, string targetMethodName)
        {
            Type type = AccessTools.TypeByName(className);
            if (type == null) return;

            var hiddenMethods = AccessTools.GetDeclaredMethods(type)
                .Where(m => m.Name.Contains($"<{targetMethodName}>b__"))
                .OrderBy(m => m.Name)
                .ToList();

            if (hiddenMethods.Count == 0)
            {
                var innerTypes = AccessTools.InnerTypes(type);
                foreach (var innerType in innerTypes)
                {
                    var innerMethods = AccessTools.GetDeclaredMethods(innerType)
                        .Where(m => m.Name.Contains($"<{targetMethodName}>b__"))
                        .OrderBy(m => m.Name)
                        .ToList();
                    hiddenMethods.AddRange(innerMethods);
                }
            }

            Log.Message($"=== [Astro] Анализ лямбд для {className} ===");
            for (int i = 0; i < hiddenMethods.Count; i++)
            {
                var method = hiddenMethods[i];
                // Вытаскиваем IL-код метода, чтобы увидеть, какие переменные он трогает
                var instructions = HarmonyLib.PatchProcessor.GetOriginalInstructions(method);
                string internals = string.Join(" ", instructions.Select(ins => ins.operand?.ToString() ?? ins.opcode.Name));

                Log.Message($"👉 ИНДЕКС ДЛЯ КОДА: [{i}] | Функция: {method.Name} \n Суть: {internals}, \n Атрибуты: {method.Attributes}");
            }
            Log.Message($"============================================\n");
        }
    }
}
