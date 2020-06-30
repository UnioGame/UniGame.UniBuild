# UniBuild
Command based scriptable build pipeline for Unity 3D

## How To Install

### Unity Package Installation

Add to your project manifiest by path [%UnityProject%]/Packages/manifiest.json these lines:

```json
{
  "scopedRegistries": [
    {
      "name": "Unity",
      "url": "https://packages.unity.com",
      "scopes": [
        "com.unity"
      ]
    },
    {
      "name": "UniGame",
      "url": "http://packages.unigame.pro:4873/",
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

![](https://i.gyazo.com/dbd9eba7427a8f85f2d63d8f0b057f7b.png)

### Menu Items

![](https://i.gyazo.com/22e7c699847e046192b8c12225c046f3.png)

### Unity Cloud Build Methods

![](https://i.gyazo.com/45904cff034647c439c4d1acf76750b4.png)


