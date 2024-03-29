# UniBuild
Command based scriptable build pipeline for Unity 3D

## How To Install

### Unity Package Installation

**Odin Inspector Asset recommended to usage with this Package (https://odininspector.com)**

Add to your project manifiest by path [%UnityProject%]/Packages/manifiest.json these lines:

```json
{
  "scopedRegistries": [
    {
      "name": "Unity",
      "url": "https://package.unity.com",
      "scopes": [
        "com.unity"
      ]
    },
    {
      "name": "UniGame",
      "url": "http://package.unigame.pro:4873/",
      "scopes": [
        "com.unigame"
      ]
    }
  ],
}
```
Open window Package Manager in Unity and install UniBuild 

![](https://i.gyazo.com/724a7a8c10ad8876d1bdd99a4ab7c13f.png)


### Configurations SO

![](https://github.com/UnioGame/UniGame.UniBuild/blob/master/GitAssets/build_map_window.png)

### Menu Items

All Build menu items auto-generated by your build configurations

![](https://i.gyazo.com/042b84f72352c9282b2b244f8c0d7dc5.png)

![](https://i.gyazo.com/22e7c699847e046192b8c12225c046f3.png)


All exists build pipeline configurations window

![](https://github.com/UniGameTeam/UniBuild/blob/master/GitAssets/build_configs_windows.png)


Auto generated content stored in: "Assets\UniGame.Generated\UniBuild\Editor"

![](https://i.gyazo.com/b6e7796ce761e7d93677a3ec7d084904.png)


### Unity Cloud Build Methods

All Cloud methods auto-generated by your build configurations files

![](https://i.gyazo.com/45904cff034647c439c4d1acf76750b4.png)

![](https://i.gyazo.com/515c525d3722fcc11d5224424fecc8bb.png)

![](https://i.gyazo.com/33f0a9d1a11a024a3d60c7769ff0f6bf.png)

## Commands

All build commands realize two type of API:

- IUnityPreBuildCommand
- IUnityPostBuildCommand

You can create your own command with two ways: 

![](https://github.com/UniGameTeam/UniBuild/blob/master/GitAssets/commands1.png)

1. Unity ScriptableObject command

In that case inherit your SO from - UnityBuildCommand . 
Scriptable Object Commands can be helpful when you want to share command 
between different pipelines and modify command parameters from single source

2. Serializable Regular C# class 

![](https://github.com/UniGameTeam/UniBuild/blob/master/GitAssets/commands2.png)

If you choose this way, then just realise Interface API - IUnityBuildCommand , no addition actions required

### Additional commands

Some "ready to use" commands can be found at "UniGame Build Commands" package

![](https://github.com/UniGameTeam/UniBuild/blob/master/GitAssets/commands-package.png)

1. AddressableImporter Package commands (https://github.com/favoyang/unity-addressable-importer)
2. Unity Addressables Commands (FTP upload support, Rebuild e.t.c)
3. WebRequests Commands
4. Folder & File commands
5. FTP commands



