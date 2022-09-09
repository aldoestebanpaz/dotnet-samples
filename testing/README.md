# Samples of testing on dotnet

NOTE: This project was created using dotnet 6.

## How I created this project

1. Create a new solution in the current directory:

```sh
dotnet new sln --name testing-using-dotnet -o .
```

2. Create a new class library project in the 'MyLibrary' directory and include it in the solution:

```sh
dotnet new classlib -o MyLibrary
mv ./MyLibrary/Class1.cs ./MyLibrary/MySutService.cs
sed -i 's/Class1/MySutService/' ./MyLibrary/MySutService.cs
dotnet sln add ./MyLibrary/MyLibrary.csproj
```

3. Add the Nuget dependencies of the library project:

```sh
dotnet add ./MyLibrary/MyLibrary.csproj package Microsoft.AspNet.WebApi.Client
```

4. Create a new testing project using xunit in the 'MyLibrary.Tests' directory, add the reference to MyLibrary, and include it in the solution:

```sh
dotnet new xunit -o MyLibrary.Tests
dotnet add ./MyLibrary.Tests/MyLibrary.Tests.csproj reference ./MyLibrary/MyLibrary.csproj
dotnet sln add ./MyLibrary.Tests/MyLibrary.Tests.csproj
```

1. Add the Nuget dependencies of the testing project:

```sh
dotnet add ./MyLibrary.Tests/MyLibrary.Tests.csproj package moq
```

FInally I added the test methods. The `[Fact]` attribute declares a test method that's run by the test runner.

## How to run the tests

### Run all tests

Move to the tests folder and run `dotnet test`. This command builds both projects and runs the tests.

```sh
dotnet test
```

### Run single test case

Run the following command in this top directory:

```sh
dotnet test --filter "FullyQualifiedName=ProjectNamespace.TestClass.TestMethhod"
```

Example:

```sh
dotnet test --filter "FullyQualifiedName=MyLibrary.Tests.Api.TodosApiServiceTests.FetchTodos_WhenInvoked_ShouldReturnAListOfTodos"
```
