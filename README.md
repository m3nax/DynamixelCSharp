# DynamixelCSharp

[![CodeQL](https://github.com/m3nax/DynamixelCSharp/actions/workflows/codeql.yml/badge.svg)](https://github.com/m3nax/DynamixelCSharp/actions/workflows/codeql.yml)
[![.NET](https://github.com/m3nax/DynamixelCSharp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/m3nax/DynamixelCSharp/actions/workflows/dotnet.yml)

Unofficial cross-platform cross-arch library for the communication with dynamixel devices.

# Releases

https://github.com/m3nax/DynamixelCSharp/releases

| Release  | Version | Type   | Release Note (Changelog)                                       | .NET Targeting |
|----------|---------|--------|----------------------------------------------------------------|----------------|
| **NOT RELEASED**  | 7.0.0 | DEV |                                                            |7.0            |

# Supported platforms

| OS         | Arch        | Support  | Package Version |
|------------|-------------|:--------:|-----------------|
| linux      | arm32       | ❔      | >= 7.0.0        |
| linux      | arm64       | ✅      | >= 7.0.0        |
| linux      | amd64 (x64) | ✅      | >= 7.0.0        |
| windows    | arm64       | ❔      | >= 7.0.0        |
| windows    | amd64 (x64) | ✅      | >= 7.0.0        |
| macOS      | arm64       | ❔      | >= 7.0.0        |
| macOS      | amd64 (x64) | ❔      | >= 7.0.0        |

<div>❔ = not tested</div>
<div>✅ = supported</div>
<div>❌ = not supported</div>

# Build documentation site
```bash
# Install docfx
dotnet tool update -g docfx

# Build documentation site and serve it on localhost:8080
 docfx docs/docfx.json -o:./app/docs/ --serve
```
