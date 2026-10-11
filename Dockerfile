FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
# Keep in sync with env.DOCFX_VERSION in .github/workflows/docfx.yaml
ARG DOCFX_VERSION=2.78.4
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global docfx --version ${DOCFX_VERSION}
WORKDIR /repo
COPY global.json Directory.Build.props Directory.Packages.props DynamixelCSharp.slnx ./
COPY src ./src
COPY docs ./docs
RUN docfx docs/docfx.json -o:/output/docs/

FROM nginx:alpine AS final
COPY --from=build /output/docs/ /usr/share/nginx/html/
EXPOSE 80
