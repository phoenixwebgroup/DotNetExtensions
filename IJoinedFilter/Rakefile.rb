require 'rake'
require 'albacore'

$projectSolution = 'IJoinedFilter.sln'
$artifactsPath = "build"
$nugetFeedPath = ENV["NuGetDevFeed"]

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
	sh "nuget pack JoinedFilter\\JoinedFilter.csproj /OutputDirectory " + $nugetFeedPath
	sh "nuget pack JoinedFilter.Windsor\\JoinedFilter.Windsor.csproj /OutputDirectory " + $nugetFeedPath
end

desc "Setup dependencies for nuget packages"
task :dep do
	package_folder = File.expand_path('packages')
    packages = FileList["**/packages.config"].map{|f| File.expand_path(f)}
	packages.each do |file|
		sh %Q{nuget install #{file} /OutputDirectory #{package_folder}}
    end
end