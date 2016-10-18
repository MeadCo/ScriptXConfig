# MeadCo.ScriptXConfigurationHandler
A library to support a custom configuration section in ASP.NET applications that use the [ScriptX Add-on for Internet Explorer][6] to deliver controlled printing on client PCs. The configuration section describes the code downloads available and optional ScriptX licensing. 

The library is used by the ScriptX Helper ([MVC][1]) libraries and is also used by the [MeadCo ScriptX Samples][5] system to deliver appropriate code versions to users.

## Current Version
2.0.0

v2.0 is not compatible with v1. 

v1 was not published in source form but was included in the Nuget package for the [ScriptX 7 installers][2]. With v2 we have disconnected the binding between ScriptX releases and this code by making the [binary bits][3] Nuget package a dependency of this package.
## Nuget Gallery
[MeadCo.ScriptXConfigurationHandler][4]
## Configuration

A standard declaration is required in the config file:

```
  <configSections>
    <!-- meadco.scriptx -->
    <sectionGroup name="meadco">
      <section name="scriptx" type="MeadCo.ScriptX.ScriptXConfigurationHandler,MeadCo.ScriptXConfigurationHandler" />
    </sectionGroup>
  </configSections>
```

The section can then  be included in the config to describe the available code and license.
The section describes the version(s) of installer(s) available either as an auto installable (cab) file or a file suitable for download and manual install by the user and for which processor (x86 or x64) and scope (machine or user).

### Single version
For example, the [binary bits][3] package adds a description of the single version available from the package.

```
  <meadco>
    <scriptx>
      <!-- define the version supplied by this package -->
      <clientinstaller filename="~/content/meadco.scriptx/installers/smsx_8.0.0.cab" 
            manualfilename="~/content/meadco.scriptx/installers/ScriptX_8.0.0.msi" 
            version="8.0.0.39" scope="machine" processor="x86" />     
    </scriptx>
  </meadco>
```

### Multiple installers and versions
Installers for different processors or scopes or versions can be defined:
```
 <meadco>
    <scriptx>
      <clientinstallers installhelper="~/MeadCo.ScriptX/installHelper">
        <installer filename="~/content/meadco.scriptx/installers/smsx.cab" 
            manualfilename="~/content/meadco.scriptx/installers/ScriptX.msi" 
            version="8.0.0.0" />
        <installer filename="~/content/meadco.scriptx/installers/smsxUser.cab" 
            manualfilename="~/content/meadco.scriptx/installers/ScriptXUser.msi" 
            version="8.0.0.0" scope="user" />
        <installer filename="~/content/meadco.scriptx/installers/smsx64.cab" 
            manualfilename="~/content/meadco.scriptx/installers/ScriptXx64.msi" 
            version="8.0.0.0" scope="machine" processor="x64" />
        <installer filename="~/content/meadco.scriptx/installers/smsx64User.cab" 
            manualfilename="~/content/meadco.scriptx/installers/ScriptXx64User.msi" 
            version="8.0.0.0" scope="user" processor="x64" />
        <installer filename="~/content/meadco.scriptx/installers/smsx7.cab" 
            manualfilename="~/content/meadco.scriptx/installers/ScriptX7.msi" 
            version="7.7.0.20" scope="machine" />        
      </clientinstallers>
    </scriptx>
  </meadco>
```

### License
The license for the application can be defined 

```
    <meadco>
        <scriptx>
            <license guid="{55326F1D-876A-447F-BA96-1A68B8EFC288}" filename="~/content/sxlic.mlf" revision="10" peruser="true" />
        </scriptx>
    </meadco>
```

## Copyright
Copyright © 2016 [Mead & Co Ltd][6].

## License 
**MeadCo.ScriptXConfigurationHandler** is under MIT license - http://www.opensource.org/licenses/mit-license.php

[1]: https://github.com/MeadCo/ScriptXASPNETMVC
[2]: https://www.nuget.org/packages/MeadCoScriptXInstallers/
[3]: https://www.nuget.org/packages/MeadCoScriptXBinaryBits/
[4]: https://www.nuget.org/packages/MeadCoScriptXConfigurationHandler
[5]: http://scriptxsamples.v8.meadroid.com/
[6]: http://scriptx.meadroid.com
