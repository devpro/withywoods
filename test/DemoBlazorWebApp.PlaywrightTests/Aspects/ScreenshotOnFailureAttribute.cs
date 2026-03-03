using System;
using System.Threading.Tasks;
using Metalama.Framework.Aspects;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Aspects;

[CompileTime]
internal class ScreenshotOnFailureAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        throw new NotSupportedException("Sync override not implemented; use async methods.");
    }

    public override async Task<dynamic?> OverrideAsyncMethod()
    {
        try
        {
            return await meta.ProceedAsync();
        }
        catch
        {
            var test = (PlaywrightTestBase)meta.This;
            await test.TakeScreenshotAsync();
            throw;
        }
    }
}
