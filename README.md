
# Build documentation site (Containerized)
```bash
# 1. Build the Docker image
docker build -t dynamixelcsharp-docs:latest .

# 2. Run the container, mapping port 80 to local port 8080
docker run -d -p 8080:80 --name doc_site dynamixelcsharp-docs:latest
```