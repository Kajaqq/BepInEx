using System;
using System.IO;
using Xunit;

namespace BepInEx.Unity.IL2CPP.Tests;

public class Il2CppInteropManagerTests
{
    static Il2CppInteropManagerTests()
    {
        var rootPath = Path.Combine(Path.GetTempPath(), $"bepinex-il2cpp-tests-{Guid.NewGuid():N}");
        Directory.CreateDirectory(Path.Combine(rootPath, "Game_Data"));
        Paths.SetExecutablePath(Path.Combine(rootPath, "Game.exe"), Path.Combine(rootPath, "BepInEx"));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GetGameAssemblyPathForXrefScanning_UsesAssemblyMatchingDummySource(
        bool useRuntimeAssemblies)
    {
        var gameAssemblyPath = Path.Combine("game", "GameAssembly.dll");
        var runtimeBinariesPath = Path.Combine("bepinex", "runtime-bins");
        var expected = useRuntimeAssemblies
                           ? Path.Combine(runtimeBinariesPath, "GameAssembly.dll")
                           : gameAssemblyPath;

        var actual = Il2CppInteropManager.GetGameAssemblyPathForXrefScanning(useRuntimeAssemblies,
                                                                             gameAssemblyPath,
                                                                             runtimeBinariesPath);

        Assert.Equal(expected, actual);
    }
}
