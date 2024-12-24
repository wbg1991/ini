# Usage

## 1. Default

### Save

```csharp
var ini = new IniFile();

ini["SYSTEM"]["LAST_MODELNAME"] = "HM800MZ1AK_V01"
ini["SYSTEM"]["LAST_PATTERNSCAN"] = 2.0

ini["OPE_CH_INFO"]["IS_CHECKED"] = true // or "TRUE"
ini["OPE_CH_INFO"]["CH1"] = 1
ini["OPE_CH_INFO"]["CH2"] = 2

ini.Save("Operation.ini");
```



### Load

```csharp
var ini = new IniFile();

ini.Load("Operation.ini");

Console.WriteLine(ini["SYSTEM"]["LAST_MODELNAME"].ToString());
Console.WriteLine(ini["SYSTEM"]["LAST_PATTERNSCAN"].ToDouble());

Console.WriteLine(ini["OPE_CH_INFO"]["IS_CHECKED"].ToBool());
Console.WriteLine(ini["OPE_CH_INFO"]["CH1"].ToInt());
Console.WriteLine(ini["OPE_CH_INFO"]["CH2"].ToInt());
```



## 2. Object Mapper

### Mapping

```csharp
using Ini;

// if not exist 'Section' attribute, Set default by class name
[Section("SYSTEM")] 
public class Settings 
{
    public enum Mode {
        Op = 0x01,
        Pm = 0x02,
        Ms = 0x04
    }

    [Key("LAST_MODELNAME")]
    public string LastModelName { get; set; }


    // if not exist 'Key' attribute, Set default by property's name
    public double LAST_PATTERNSCAN { get; set; }

    [Key("IS_CHECKED", "OPE_CH_INFO")] // Key, Section
    public bool IsChecked { get; set; }

    // EnumValue Save default = Number
    [EnumValue(EnumValueAttribute.Saves.Number)]
    public Mode OpMode { get; set; }

    [NotMapped] // exclusion
    public int neverSaved { get; set; }
}
```



### Save

```csharp
var settings = new Settings() 
{
    LastModelName = "HM800MZ1AK_V01",
    LAST_PATTERNSCAN = 2.0,
    IsChecked = true,
    OpMode = Settings.Mode.Pm,
    neverSaved = 123_456_789
};


IniMapper.Save(settings, "Operation.ini");

/* Operation.ini
[SYSTEM]
LAST_MODELNAME = HM800MZ1AK_V01
LAST_PATTERNSCAN = 2.0
OpMode = 2

[OPE_CH_INFO]
IS_CHECKED = true
*/
```



### Load

```csharp
var settings = IniMapper.Load<Settings>("Operation.ini");

Console.WriteLine(settings.ToString());
/*
LastModelName = "HM800MZ1AK_V01",
LAST_PATTERNSCAN = 2.0,
IsChecked = true,
OpMode = Settings.Mode.Pm,
neverSaved = 0
*/
```
