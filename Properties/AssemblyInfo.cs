using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;
using AudicaModding;

[assembly: AssemblyTitle(CommandManager.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(CommandManager.BuildInfo.Company)]
[assembly: AssemblyProduct(CommandManager.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + CommandManager.BuildInfo.Author)]
[assembly: AssemblyTrademark(CommandManager.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(CommandManager.BuildInfo.Version)]
[assembly: AssemblyFileVersion(CommandManager.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(CommandManager), CommandManager.BuildInfo.Name, CommandManager.BuildInfo.Version, CommandManager.BuildInfo.Author, CommandManager.BuildInfo.DownloadLink)]
[assembly: MelonOptionalDependencies("ScoreOverlay", "TimingAttack")]

// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]