# Developer's guide

## Setup

### IDE

- Rider (recommended)
- Visual Studio 2026
- Visual Studio Code

## Quality checks

```bash
dotnet format
docker run --rm -v "$(pwd)":/data cytopia/yamllint .
docker run --rm -v "$(pwd)":/workdir davidanson/markdownlint-cli2 "**/*.md"
```
