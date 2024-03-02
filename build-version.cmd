@echo off
set sln_file=Nop.Plugin.Widgets.SpecialOrder

cd %1
dotnet build %sln_file% --configuration=Debug
dotnet build %sln_file% --configuration=Release

cd ..