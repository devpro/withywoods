# Developer's guide

## Local steps

1. Install [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

    ```bash
    # makes sure .NET is installed and displays the version
    dotnet --version
    ```

2. Build and test from the command line

    ```bash
    # builds the solution
    dotnet build
   
    # runs the tests
    dotnet test
   
    # run one specific test project (here Configuration.UnitTests) 
    dotnet test --project test\Configuration.UnitTests
    ```

3. Develop in an IDE (Rider - recommended, Visual Studio 2026 or Visual Studio Code)

    From the IDE, you can build the solution, run and debug the tests.

4. Quality checks

    ```bash
    # review .NET code against .editorconfig
    dotnet format
   
    # lints YAML files
    docker run --rm -v "$(pwd)":/data cytopia/yamllint .
    
    # lints Markdown files
    docker run --rm -v "$(pwd)":/workdir davidanson/markdownlint-cli2 "**/*.md"
    ```
