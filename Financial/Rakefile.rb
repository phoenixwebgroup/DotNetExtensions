require 'rake'
require 'albacore'

$projectSolution = 'Financial.sln'
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
	nugetFeedLocation = File.join($nugetFeedPath, 'DotNetExtensions-Financial')
	sh "nuget pack src\\Financial\\Financial.csproj /OutputDirectory " + $nugetFeedPath 
end

desc "Setup dependencies for nuget packages"
task :dep do
	package_folder = File.expand_path('Packages')
    packages = FileList["**/packages.config"].map{|f| File.expand_path(f)}
	packages.each do |file|
		sh %Q{nuget install #{file} /OutputDirectory #{package_folder}}
    end
end