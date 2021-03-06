
# MeadCo.ScriptXConfigurationHandler
A library to support a custom configuration section in ASP.NET applications that use the [ScriptX Add-on for Internet Explorer][6] or [ScriptX.Services][7] to deliver controlled printing on client PCs. The configuration section describes the code downloads available and optional ScriptX licensing. 

The library is used by the ScriptX Helper ([MVC][1]) libraries and is also used by the [MeadCo ScriptX Samples][5] system to deliver appropriate code versions and/or javascript to user devices.

## Current Version
4.0.0

v1 was not published in source form but was included in the Nuget package for the [ScriptX 7 installers][2]. With v2 we have disconnected the binding between ScriptX releases and this code by making the [binary bits][3] Nuget package a dependency of this package.

v3 added support for defining the use of the ScriptX.Print service (on cloud or on-premise) for non IE browsers. References to MeadCo.ScriptX.Library must be changed to MeadCo.ScriptX.Helpers (and probably to .AgentParser).

v4 added support for defining the use of the ScriptX.Print service on Workstation for non IE browsers and refined support for on cloud or on premise services. References to MeadCo.ScriptX.Library must be changed to MeadCo.ScriptX.Helpers.

## Nuget Gallery
[MeadCo.ScriptXConfigurationHandler][4]
## Configuration

A standard declaration is required in the config file:

```
  <configSections>
    <!-- meadco.scriptx -->
    <sectionGroup name="meadco">
      <section name="scriptx" 
           type="MeadCo.ScriptX.ScriptXConfigurationHandler,MeadCo.ScriptXConfigurationHandler" />
    </sectionGroup>
  </configSections>
```

The section can then  be included in the config to describe the available code and license for the Internet Explorer Add-on and service endpoints and license for using ScriptX.Print.
For the add-on, the section describes the version(s) of installer(s) available either as an auto installable (cab) file or a file suitable for download and manual install by the user and for which processor (x86 or x64) and scope (machine or user).

### ScriptX.Print Service

```
    <meadco>
        <scriptx>
            <printservice 
                server="https://scriptxservices.meadroid.com" apiversion="1"
                guid="{13598d2f-8724-467b-ae64-6e53e9e9f644}"
                filename="~/content/sxlic.mlf" revision="10" />
        </scriptx>
    </meadco>
```
ApiVersion signifies the API version supported by your client code (this is coded into the API endpoints).

Filename and Revision only apply when using the For Windows PC service and you want to use the service to install the license. 
For the On Premise Service, no subscription/license GUID is required as a service license is installed with the server.
For MeadCo's Cloud service the guid will be taken as the subscription id and filename and revision are not required.

### Add-on

#### Single version
For example, the [ScriptX Add-on binary bits][3] package adds a description of the single version available from the package.

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

#### Multiple installers and versions
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

#### License
The license to be used by the add-on can be defined 

```
    <meadco>
        <scriptx>
            <license guid="{55326F1D-876A-447F-BA96-1A68B8EFC288}" 
                filename="~/content/sxlic.mlf" revision="10" peruser="true" />
        </scriptx>
    </meadco>
```

Note that filename may be a url. 

## Copyright
Copyright © 2016-2017 [Mead & Co Ltd][6].

## License 
**MeadCo.ScriptXConfigurationHandler** is under MIT license - http://www.opensource.org/licenses/mit-license.php

[1]: https://github.com/MeadCo/ScriptXASPNETMVC
[2]: https://www.nuget.org/packages/MeadCoScriptXInstallers/
[3]: https://www.nuget.org/packages/MeadCoScriptXBinaryBits/
[4]: https://www.nuget.org/packages/MeadCoScriptXConfigurationHandler
[5]: http://scriptxprintsamples.meadroid.com/
[6]: https://www.meadroid.com/Features/ScriptXAddOn
[7]: https://scriptxservices.meadroid.com