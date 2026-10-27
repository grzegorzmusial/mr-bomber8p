# Windows x64 package

Build: Actions > Build Windows package > Run workflow. Download the
Mr-Bomber-Windows-x64 artifact after the job succeeds, then extract both ZIP layers.

Installation: open the generated *_Test directory, right-click Add-AppDevPackage.ps1
and select Run with PowerShell. Follow the Windows prompts for the development
certificate and dependencies. This package is self-signed, not Store-certified.
Only trust the certificate from a build you trust. Keep the Dependencies directory.

Release: every successful master push or manual run publishes a GitHub Release
using the manifest version (2.2.3.0 becomes v2.2.3). Increase the manifest version
before the next release; an existing release is never overwritten.

Signing: without secrets, each build creates a temporary development certificate
matching the manifest Publisher. For consistent signing across updates, configure
UWP_PFX_BASE64 and UWP_PFX_PASSWORD in repository Actions secrets, using a code-signing
PFX whose subject matches Package.appxmanifest (currently CN=Gregor). Never commit
that PFX or its password. With changing certificates, Windows may require removing
the previous installation and trusting the new certificate.

Local build requires Visual Studio 2022 with UWP C# tools, Windows SDK 10.0.19041.0,
.NET 6 for MonoGame 3.8.1 tools, and MSBuild.exe (not dotnet build).
The workflow uses Release x64 and .NET Native. The first CI run is needed to verify
compiler, content-pipeline and packaging compatibility; this repair has not yet
completed a build in GitHub Actions.
