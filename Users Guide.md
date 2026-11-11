# Bezier ScreenSaver: User's Guide



## Downloads & Runtimes

Download the latest release from the [Releases page](https://github.com/RemusRigo/BezierScreenSaver-VB.NET/releases).

Each release contains one folder per framework and architecture:

|Folder|Target Framework|Architecture|Required Runtime|
|-|-|-|-|
|`net48 x64`|.NET Framework 4.8|64-bit|Built into Windows 10/11 ([.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) for older systems)|
|`net48 x86`|.NET Framework 4.8|32-bit|Built into Windows 10/11 ([.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) for older systems)|
|`net8.0 x64`|.NET 8.0|64-bit|[.NET 8.0 Desktop Runtime (x64)](https://dotnet.microsoft.com/download/dotnet/8.0)|
|`net8.0 x86`|.NET 8.0|32-bit|[.NET 8.0 Desktop Runtime (x86)](https://dotnet.microsoft.com/download/dotnet/8.0)|
|`net10.0 x64`|.NET 10.0|64-bit|[.NET 10.0 Desktop Runtime (x64)](https://dotnet.microsoft.com/download/dotnet/10.0)|
|`net10.0 x86`|.NET 10.0|32-bit|[.NET 10.0 Desktop Runtime (x86)](https://dotnet.microsoft.com/download/dotnet/10.0)|

> **Which one do I need?** If you are not sure, use **`net48 x64`**: it runs on any 64-bit
> Windows 10/11 without installing anything.
> For the .NET 8.0 / 10.0 builds you need the **Desktop Runtime** of that version;
> the plain ".NET Runtime" is not enough.

To check which .NET runtimes are installed, open a command prompt and run:

```
dotnet --list-runtimes
```

Look for a `Microsoft.WindowsDesktop.App 8.x` or `10.x` line.



## Install

1. Download and extract the folder that matches your system (see the table above), e.g. `net48 x64`.
2. Copy the **whole folder** to a permanent location, for example:
   `D:\My Apps\Bezier ScreenSaver\`
   The folder must contain all of its files; the screensaver won't start if any of them is missing:
   * `net48` folders: `BezierSS.scr`, `BezierSS.scr.config`
   * `net8.0` / `net10.0` folders: `BezierSS.scr`, `BezierSS.dll`, `BezierSS.dll.config`,
     `BezierSS.deps.json`, `BezierSS.runtimeconfig.json`
3. Right-click `BezierSS.scr` and choose **Install**.
   The Windows **Screen Saver Settings** dialog opens with *BezierSS* selected.
4. Set the **Wait** time and click **OK**.

> If Windows blocks the file ("Windows protected your PC"), right-click `BezierSS.scr` →
> **Properties** → check **Unblock** → **OK**, then try again.



## Test it

* In **Screen Saver Settings**, click **Preview**, or
* right-click `BezierSS.scr` and choose **Test**.

A Bézier curve bounces around the screen, leaving a trail of older curves behind it
that slowly cycles through the colors of the rainbow and fades out.
Press any key, click, or move the mouse to exit.

The small monitor in **Screen Saver Settings** shows a live, scaled-down preview.



## Settings

Open **Screen Saver Settings** (Start → type *screen saver*), select *BezierSS*, and click **Settings...**
(double-clicking `BezierSS.scr` also opens the settings).

|Setting|Range|Default|Description|
|-|-|-|-|
|**Trail length**|5 - 150 curves|35|How many previous curves stay on screen, fading from bright (newest) to dark (oldest)|
|**Speed**|1 - 10|5|How fast the curve's points move (each point gets a slightly random speed around this value)|

Click **OK** to save the settings; **Cancel**, **Esc** or the **X** button closes the dialog without saving.
They are stored per user in the registry under `HKEY_CURRENT_USER\Software\Remus RIGO\BezierSS.VB-NET`.
Right-click the title bar → **About...** for version information.

> A long trail at high speed means many curves are redrawn every frame,
> which uses more CPU on slower computers.



## Multi-monitor

Each monitor gets its own animated curve, including monitors with different resolutions or scaling.



## Uninstall

1. In **Screen Saver Settings**, choose another screensaver (or *None*) and click **OK**.
2. Delete the folder you copied in step 2 of *Install*.
3. Optional: delete the registry key `HKEY_CURRENT_USER\Software\Remus RIGO\BezierSS.VB-NET`.
