# DynamixelCSharp

[![CodeQL](https://github.com/m3nax/DynamixelCSharp/actions/workflows/codeql.yml/badge.svg)](https://github.com/m3nax/DynamixelCSharp/actions/workflows/codeql.yml)
[![.NET](https://github.com/m3nax/DynamixelCSharp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/m3nax/DynamixelCSharp/actions/workflows/dotnet.yml)
[![Docfx](https://github.com/m3nax/DynamixelCSharp/actions/workflows/docfx.yaml/badge.svg)](https://github.com/m3nax/DynamixelCSharp/actions/workflows/docfx.yaml)

Unofficial cross-platform cross-arch library for the communication with dynamixel devices.

# Releases

https://github.com/m3nax/DynamixelCSharp/releases

| Release  | Version | Type   | Release Note (Changelog)                                       | .NET Targeting |
|----------|---------|--------|----------------------------------------------------------------|----------------|
| **NOT RELEASED**   | 8.0.0  | DEV |                                                          |7.0, 8.0        |

# Supported platforms

| OS         | Arch        | Support  | Package Version |
|------------|-------------|:--------:|-----------------|
| linux      | arm32       | ❔      | >= 8.0.0        |
| linux      | arm64       | ✅      | >= 8.0.0        |
| linux      | amd64 (x64) | ✅      | >= 8.0.0        |
| windows    | arm64       | ❔      | >= 8.0.0        |
| windows    | amd64 (x64) | ✅      | >= 8.0.0        |
| macOS      | arm64       | ❔      | >= 8.0.0        |
| macOS      | amd64 (x64) | ❔      | >= 8.0.0        |

<div>❔ = not tested</div>
<div>✅ = supported</div>
<div>❌ = not supported</div>

# Supported devices/protocols
## Protocol 1.0
| Device | Support | Tested |
|--------|:-------:|:------:|
| AX-12A | ✅      | ✅     |
| AX-18A | ✅      | ❌     |

# Build documentation site
```bash
# Install docfx
dotnet tool update -g docfx

# Build documentation site and serve it on localhost:8080
docfx docs/docfx.json -o:./app/docs/ --serve
```
