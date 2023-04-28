# EasyConfig
An extendable library for creating and managing config files with ease.

Created with Unity in mind, designed with flexibility in mind.

## Features
* Simple and managable config implementation
* Extendable support for custom serializers
* Modular and documented interface
* Native JSON and XML (de)serialization support

## Installation
Clone the repository, or download the DLL/UnityPackage.

### Cloning the repository
1. Locate your destination folder
2. Open your folder in your CLI of choice
3. `git clone https://github.com/Cruxial0/EasyConfig`
4. [Restore Nuget Packages]
5. Build the project

### Setting up for Unity
1. Download the UnityPackage from [Releases]
2. Import preferred package into Unity
3. Open the Package Manager (Window > Package Manager)
4. Click the '+', then click 'Add from Git URL'
5. Add `com.unity.nuget.newtonsoft-json`

## Overview
EasyConfig is built to be generic, and skip a lot of serializer boilerplate. It should be able to be dropped into any project, and be taken into use with minimal config. It's especially useful for those who don't like the strictness of Unity's PlayerPrefs, but are tired of all the preparation work that goes into setting up a serializer system.

### Quick Start
After everything has been imported into your project, you can initialize a Config like this:
```csharp
using EasyConfig.Types;

public class MyConfig : EasyConfig
{
    // Properties Here
    public void Load() => base.Load<MyConfig>(this);
}
```
As of now, the `Load()` method will have to be defined as shown above, but in future versions I'll be looking to remove that part.

Once you declared a class, all public properties will be serialized.
```csharp
public class MyConfig : EasyConfig
{
    // Properties Here
    public string MyString { get; set; }
    public int MyInt { get; set; }
    
    public void Load() => base.Load<MyConfig>(this);
}
```
That's all there is to creating a Config structure. It can be used in the code as following:
```csharp
private void Start()
{
    MyConfig config = new MyConfig();
    config.Load();
    
    config.MyString = "SomeValue";
    
    config.Save();
}
```

Some important notes to make about the current state of EasyConfig. 
* Do **not** call the `base.Load<T>()` method inside your class' constructor. Doing so will result in a recursive mess that crashes your program! :D
* If you wish to add public properties and not have them be Serialized, you can prepend the `[field: NonSerialized]` attribute.
* If using a DLL, the settings will have to be changed before building. This should be fixed in future versions. (sorry about that lol)
* Environment settings can be found at [../EasyConfig/ConfigSettings.cs]. This will be abstracted to a seperate config file in future versions. **Changing the RootFolder is encouraged!**

### Adding custom Serializers
Adding custom serializers to EasyConfig is quite the simple process.

Firstly, create a new class, and inherit from ISerializer:
```csharp
public class MySerializer : ISerializer
{
    public SerializeFormat Format => SerializeFormat.Json;

        public void Save(Types.EasyConfig config) {
            // Serializion logic here
        }

        T ISerializer.Load<T>(string path) {
            // Deserializion logic here
        }
}
```
Once implemented, you can simply implement the logic needed for (de)serialization. Note that `Load<T>()` is a generic method, so make sure your deserializer returns type T. A serializer also requires a SerializeFormat, which unfortunately has to be added manually.

Head over to `../EasyConfig/Types/SerializeFormat.cs`, then add your new format as shown below. Note that only a single Serializer can be associated with a SerializeFormat, an error will be thrown if multiple Serializers are targeting the same SerializeFormat.
```csharp
public enum SerializeFormat 
{
    Json,
    Xml,
    // New values here
}
```

## Future plans
* Reduced boilerplate
* Environment config file
* More failsafes (infinite recursion, type safety etc.)

## Contributing
Feel free to contribute! :D

[Restore Nuget Packages]: https://learn.microsoft.com/en-us/nuget/consume-packages/package-restore
[Releases]: https://github.com/Cruxial0/EasyConfig/releases
[../EasyConfig/ConfigSettings.cs]: https://github.com/Cruxial0/EasyConfig/blob/main/EasyConfig/ConfigSettings.cs
