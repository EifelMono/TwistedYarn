var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var csproj="./src/EifelMono.TwistedYarn/EifelMono.TwistedYarn.csproj";

Task("Build")
.Does(()=> {
    DotNetBuild(csproj, new DotNetBuildSettings{
        Configuration= configuration 
    });
});

Task("Benchmark")
.Does(()=> {
    DotNetRun("./benchmarks/Benchmarks.csproj", new DotNetRunSettings{
        Configuration= configuration 
    });
});

Task("Default")
.IsDependentOn("Build")
.Does(() => {
   Information("Build");
});

RunTarget(target);
