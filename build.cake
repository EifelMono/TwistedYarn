var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

const string artifacts= "./artifacts";
const string src= "./src";

var csproj="./src/EifelMono.TwistedYarn/EifelMono.TwistedYarn.csproj";


Task("Clean-Artifacts")
.Does(()=> {
    Information($"Clear {artifacts}");
    EnsureDirectoryExists(artifacts);
    CleanDirectory(artifacts);
});

Task("Clean-Nugets")
.Does(()=> {
    var nugetPackagesDirectory=$"{Environment.GetEnvironmentVariable("USERPROFILE")}\\.nuget\\packages"; 
    var nugetDirectory=$"{nugetPackagesDirectory}\\{System.IO.Path.GetFileNameWithoutExtension(csproj)}";
        Information(nugetDirectory);
    if (DirectoryExists(nugetDirectory))
      CleanDirectory(nugetDirectory);
});

Task("Build-Nugets")
  .IsDependentOn("Clean-Nugets")
.Does(()=> {
        DotNetPack(csproj, new DotNetPackSettings{
            Configuration= configuration,
            OutputDirectory= artifacts
        });
});

Task("Run-Benchmark")
.Does(()=> {
    DotNetRun("./benchmarks/Benchmarks.csproj", new DotNetRunSettings{
        Configuration= configuration 
    });
});

Task("Build")
.Does(()=> {
    DotNetBuild(csproj, new DotNetBuildSettings{
        Configuration= configuration 
    });
});

Task("Default")
.IsDependentOn("Clean-Artifacts")
.IsDependentOn("Build-Nugets")
.Does(() => {
});


try 
{
    RunTarget(target);
}
catch(Exception ex)
{
    if (TeamCity.IsRunningOnTeamCity)
        throw;
 
    Error(new string('-', 40));
    Error($"Error in {target}");
    Error(new string('-', 40));
    Error(ex.Message);
    Error(new string('-', 40));
    Console.WriteLine("press enter...");
    Console.ReadLine();
}

