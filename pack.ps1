$outputDir = "D:\NuGetLocal\"
$build = "Release"
$version = "1.0.0"

nuget.exe pack .\src\Geta.Commerce.Payments.PayPal\Geta.Commerce.Payments.PayPal.csproj -IncludeReferencedProjects -properties Configuration=$build -Version $version -OutputDirectory $outputDir
nuget.exe pack .\src\Geta.Commerce.Payments.PayPal.Manager\Geta.Commerce.Payments.PayPal.Manager.csproj -IncludeReferencedProjects -properties Configuration=$build -Version $version -OutputDirectory $outputDir
nuget.exe pack .\src\Geta.PayPal\Geta.PayPal.csproj -IncludeReferencedProjects -properties Configuration=$build -Version $version -OutputDirectory $outputDir
