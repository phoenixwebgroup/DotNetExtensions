require 'rake'
require 'albacore'

$projectSolution = 'DotNetExtensions.sln'
$artifactsPath = "build"
$nugetFeedPath = "%PAC%/nuget"

task :teamcity => [:build_release]

task :build => [:build_release]

msbuild :build_release => [:clean, :dep] do |msb|
  msb.properties :configuration => :Release
  msb.targets :Build
  msb.solution = $projectSolution
end

task :clean do
    puts "Cleaning"
    FileUtils.rm_rf $artifactsPath
end

task :nuget => [:build] do
	nugetFeedLocation = File.join($nugetFeedPath, 'DotNetExtensions-Base')
	sh "nuget pack src\\BclExtensionMethods\\BclExtensionMethods.csproj /OutputDirectory " + $nugetFeedPath + 
		" /Exclude \"*Dynamic Expressions.html\""
end

desc "Setup dependencies for nuget packages"
task :dep do
	package_folder = File.expand_path('Packages')
    packages = FileList["**/packages.config"].map{|f| File.expand_path(f)}
	packages.each do |file|
		sh %Q{nuget install #{file} /OutputDirectory #{package_folder}}
    end
end