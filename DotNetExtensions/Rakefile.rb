require 'rake'
require 'albacore'

$projectSolution = 'DotNetExtensions.sln'
$artifactsPath = "build"
$nugetPath = "nuget"
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

task :nuget => [:nuget_DotNetExtensions, :nuget_Financial]

task :nuget_DotNetExtensions => [:build] do
	source =  File.join($artifactsPath, 'BclExtensionMethods.*')
	destination = File.join($nugetPath, 'DotNetExtensions-Base/lib/net40')
	FileUtils.mkdir_p(destination) unless File.directory?(destination)
	FileUtils.cp_r(Dir.glob(source), destination)
	nugetFeedLocation = File.join($nugetFeedPath, 'DotNetExtensions-Base')
	sh "nuget pack " + File.join($nugetPath, 'DotNetExtensions-Base', 'DotNetExtensions-Base.nuspec') + " /OutputDirectory " + nugetFeedLocation
end

task :nuget_Financial => [:build] do
	source =  File.join($artifactsPath, 'Financial.*')
	destination = File.join($nugetPath, 'DotNetExtensions-Financial/lib/net40')
	FileUtils.mkdir_p(destination) unless File.directory?(destination)
	FileUtils.cp_r(Dir.glob(source), destination)
	nugetFeedLocation = File.join($nugetFeedPath, 'DotNetExtensions-Financial')
	sh "nuget pack " + File.join($nugetPath, 'DotNetExtensions-Financial', 'DotNetExtensions-Financial.nuspec') + " /OutputDirectory " + nugetFeedLocation
end

desc "Setup dependencies for nuget packages"
task :dep do
	package_folder = 'Packages'
    FileList["**/packages.config"].each do |file|
        sh %Q{nuget install #{file} /OutputDirectory #{package_folder}}
    end
end