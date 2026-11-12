# DynamixelCSharp

[![CodeQL](https://github.com/m3nax/DynamixelCSharp/actions/workflows/codeql.yml/badge.svg)](https://github.com/m3nax/DynamixelCSharp/actions/workflows/codeql.yml)
[![.NET](https://github.com/m3nax/DynamixelCSharp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/m3nax/DynamixelCSharp/actions/workflows/dotnet.yml)
[![Docfx](https://github.com/m3nax/DynamixelCSharp/actions/workflows/docfx.yaml/badge.svg)](https://github.com/m3nax/DynamixelCSharp/actions/workflows/docfx.yaml)

Unofficial cross-platform cross-arch library for the communication with dynamixel devices.

# Releases

https://github.com/m3nax/DynamixelCSharp/releases

| Release  | Version | Type    | Release Note (Changelog)                                       | .NET Targeting |
|----------|---------|---------|----------------------------------------------------------------|----------------|
| **NOT RELEASED**   | 10.0.0  | DEV |                                                          | 10.0           |

# Development packages

Development builds of the library are published to GitHub Packages on every push to `main`.

NuGet source: `https://nuget.pkg.github.com/m3nax/index.json`

Reading from this source requires a personal access token (classic) with the `read:packages` scope.

```bash
dotnet nuget add source --username <github-username> --password <personal-access-token> --store-password-in-clear-text --name github-m3nax "https://nuget.pkg.github.com/m3nax/index.json"
dotnet add package DynamixelCSharp --prerelease
```

Development versions look like `0.1.0-dev.N`, where `N` is the workflow run number.

# Supported platforms

| OS         | Arch        | Support  | Package Version |
|------------|-------------|:--------:|-----------------|
| linux      | arm32       | ❔      | >= 10.0.0        |
| linux      | arm64       | ✅      | >= 10.0.0        |
| linux      | amd64 (x64) | ✅      | >= 10.0.0        |
| windows    | arm64       | ❔      | >= 10.0.0        |
| windows    | amd64 (x64) | ✅      | >= 10.0.0        |
| macOS      | arm64       | ❔      | >= 10.0.0        |
| macOS      | amd64 (x64) | ❔      | >= 10.0.0        |

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

# Documentation site (Docker)
```bash
# Build the image (builds the documentation site with docfx and serves it with nginx)
docker build -t dynamixelcsharp-docs .

# Run the container
docker run --rm -p 8080:80 dynamixelcsharp-docs
```

The site is then available at http://localhost:8080.

The docfx version can be overridden with `--build-arg DOCFX_VERSION=<version>`.
