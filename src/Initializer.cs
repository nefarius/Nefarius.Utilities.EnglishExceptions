﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using MonoMod.RuntimeDetour;
using MonoMod.Utils;

using Nefarius.Utilities.EnglishExceptions.Core;
using Nefarius.Utilities.EnglishExceptions.Util;

namespace Nefarius.Utilities.EnglishExceptions;

/// <summary>
///     Main logic (establishing hooks etc.) goes here.
/// </summary>
internal sealed unsafe class Initializer
{
    private static NativeHook? _formatMessageHook;
    private static readonly FormatMessageHookedCallback FormatMessageDelegate = FormatMessageHooked;
    private static readonly CultureInfo EnglishUsCulture = CultureInfo.GetCultureInfo("en-US");

    [SuppressMessage("ReSharper", "IdentifierTypo")]
    private static int FormatMessageHooked(
        FormatMessageCallback orig,
        FormatMessageFlags dwflags,
        IntPtr lpsource,
        int dwmessageid,
        int dwlanguageid,
        IntPtr lpbuffer,
        int nsize,
        IntPtr* arguments
    )
    {
        IEnumerable<CultureInfo> installedLanguages = LangUtils.InstalledInputLanguages;

        // English language pack is available
        if (installedLanguages.Any(c => c.Equals(EnglishUsCulture)))
        {
            // override whatever caller requested
            dwlanguageid = EnglishUsCulture.KeyboardLayoutId;
        }

        return orig(dwflags, lpsource, dwmessageid, dwlanguageid, lpbuffer, nsize, arguments);
    }

    /// <summary>
    ///     This libraries "Main" method, so the caller has to do nothing but reference our library.
    /// </summary>
    [ModuleInitializer]
    public static void Initialize()
    {
        IntPtr kernel32 = DynDll.OpenLibrary("Kernel32.dll");
        IntPtr formatMessageW = kernel32.GetExport("FormatMessageW");

        _formatMessageHook = new NativeHook(formatMessageW, FormatMessageDelegate);
    }

    ~Initializer()
    {
        _formatMessageHook?.Undo();
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int FormatMessageCallback(
        FormatMessageFlags dwFlags,
        IntPtr lpSource,
        int dwMessageId,
        int dwLanguageId,
        IntPtr lpBuffer,
        int nSize,
        IntPtr* arguments
    );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int FormatMessageHookedCallback(
        FormatMessageCallback orig,
        FormatMessageFlags dwFlags,
        IntPtr lpSource,
        int dwMessageId,
        int dwLanguageId,
        IntPtr lpBuffer,
        int nSize,
        IntPtr* arguments
    );
}