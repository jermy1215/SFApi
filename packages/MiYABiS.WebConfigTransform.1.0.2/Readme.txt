
log4net.config setting
==========

When you use , please install the log4net from nuget.  

C#:Properties\WebConfigTransformAssemblyInfo.cs file Please cancel comment.
```
[assembly: log4net.Config.XmlConfigurator(ConfigFile=@"log4net.config", Watch=true)]
```

VB:My Project\WebConfigTransformAssemblyInfo.vb file Please cancel comment.
```
<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="log4net.config", Watch:=True)> 
```


Web.config Section Protection setting
==========

When you use , please install the Moca.NETConfiguration from nuget.  

C#:add Assembly property.
```
[assembly: Moca.Configuration.SectionProtection(Moca.Configuration.ProtectionProviderType.DPAPI, "Section Name")]
```

VB:add Assembly property.
```
<Assembly: Moca.Configuration.SectionProtection(Moca.Configuration.ProtectionProviderType.DPAPI, "Section Name")> 
```

Protection Provider Type DPAPI or RSA.

Program Startup execute method.
```
Moca.Configuration.SectionProtector.Protect()
```
