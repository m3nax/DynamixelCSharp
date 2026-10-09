# Use a suitable .NET SDK Image as a builder stage to install dependencies and compile code (if needed)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files first to leverage Docker caching
COPY src/*.csproj ./src/
COPY docs/docfx.json ./docs/
COPY docs/index.md ./docs/

# Restore dependencies for the solution
RUN dotnet restore $(find src -name "*.csproj") 

# Copy remaining source code and documentation assets
COPY . .

# Build the project (required by DocFX to compile API references)
# We target a standard build profile suitable for documentation generation.
RUN dotnet publish ./src/DynamixelCSharp/DynamixelCSharp.csproj -o /publish --configuration Release

# --- Stage 2: Documentation Generation and Serving ---
FROM nginx:alpine AS final_image
WORKDIR /usr/share/nginx/html

# Install DocFX CLI (assuming it's available via NuGet or direct install)
RUN dotnet tool install --global docfx --version <latest-stable> || echo "Warning: Could not install DocFX globally."
# Alternatively, if DocFX is managed as a package in the project structure, use that method.

# Copy documentation source files and configuration from the build stage or context root
COPY docs/ /usr/local/docfx_source/docs/
COPY src/ /src/ # Need access to compiled code for API reference generation
COPY publish/ ./publish/ # Assuming the built assemblies are required by DocFX

# Run the documentation build process to generate static site files. 
# This command needs to replicate: docfx docs/docfx.json -o:./app/docs/ 
# We adjust the output path relative to our container structure.
RUN docfx /usr/local/docfx_source/docs/docfx.json --output-dir ./static_site

# Copy generated static files into Nginx's default web root
COPY --from=build /publish/ /var/www/html/api 

# Expose the document site via Nginx
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]