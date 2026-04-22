# Developer's guide

## Local steps

1. Install [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

    ```bash
    # makes sure .NET is installed and displays the version
    dotnet --version
    ```

2. Install [Powershell](https://learn.microsoft.com/en-us/powershell/scripting/install/install-powershell?view=powershell-7.6)

    ```bash
    winget install --id Microsoft.PowerShell --source winget
    ```

3. Build and test from the command line

    ```bash
    # builds the solution
    dotnet build
   
    # installs playwright browser
    pwsh test/DemoBlazorWebApp.PlaywrightTests/bin/Debug/net10.0/playwright.ps1 install chromium
   
    # runs the tests
    dotnet test
   
    # runs one specific test project (here Configuration.UnitTests) 
    dotnet test --project test\Configuration.UnitTests
   
    # creates the packages
    dotnet pack
    ```

4. Develop in an IDE (Rider - recommended, Visual Studio 2026 or Visual Studio Code)

    From the IDE, you can build the solution, run and debug the tests.

5. Quality checks

    ```bash
    # review .NET code against .editorconfig
    dotnet format
   
    # ensures EOL is correct
    find . -type f \( -name "*.cs" -o -name "*.csproj" -o -name "*.slnx" -o -name "*.js" -o -name "*.razor" -o -name "*.css" \) -exec sed -i 's/\r$//' {} +
    
    # ensures container build is successful
    docker build . -t withywoods-demoblazorwebapp:local -f samples/DemoBlazorWebApp/Dockerfile
    docker build . -t withywoods-demoblazorwebapp:local -f samples/DemoWebApi/Dockerfile
    ```

6. Demo containers images

    - Start containers

       ```bash
       docker compose up
       ```

    - Open the web app in a browser: [DemoBlazorWebApp](http://localhost:9001), [DemoWebApi](http://localhost:9002/scalar)

    - Clean-up

       ```bash
       docker compose rm
       ```
