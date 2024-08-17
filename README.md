# Ni-Prefs-Fetcher

Ni-Prefs-Fetcher is a tool to get a list of all PlayerPrefs entries for most platforms.

## Requirements

* Unity 2019.3 or later

## Installation

### Manual

1. Clone this repository or download the source files.
2. Copy the `Ni-PlayerPrefsFetcher` folder into your Unity project's `Assets` directory.

### UPM

1. Open Package Manager from Window > Package Manager.
2. Click the "+" button > Add package from git URL.
3. Enter the following URL:

```
https://github.com/HoSHIZA/Ni-PlayerPrefsFetcher.git
```

### Manual with `manifest.json`

1. Open `manifest.json`.
2. Add the following line to the file:

```
"com.ni-games.ni_prefs_fetcher" : "https://github.com/HoSHIZA/Ni-PlayerPrefsFetcher.git"
```

## Usage

```csharp
// Gets all available PlayerPrefs entries on the current platform.
// Returns `null` if not available on the current platform.
var entries = NiPrefsFetcher.Retrieve();

// Checks for availability on the current platform.
var supported = NiPrefsFetcher.IsSupported;

// Alternative check.
var supported = NiPrefsFetcher.CheckSupport();
```

## Supported platforms

1. Windows (Standalone, Editor)
2. Osx (Standalone, Editor)
3. Android

## In development

1. iOS
2. WebGL
3. Linux

## Integrations

### Ni-Prefs (WIP)

> Automatically enabled if Ni-Prefs has been installed via UPM. Otherwise, you need to manually add `NIGAMES_PLAYER_PREFS_INTERGRATION` to Scripting Define Symbols.

This integration allows to retrieve keys and values encrypted with Ni-Prefs.

## TODO

* Ni-Prefs Integration

## License

This project is licensed under the [MIT License](LICENSE).