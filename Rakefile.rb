require 'rake'
require 'albacore'

$projectSolution = 'DotNetExtensions.sln'
$artifactsPath = "build"

task :teamcity => [:build_release]

task :build => [:build_release]

msbuild :build_release => [:clean] do |msb|
  msb.properties :configuration => :Release
  msb.targets :Build
  msb.solution = $projectSolution
end

task :clean do
    puts "Cleaning"
    FileUtils.rm_rf $artifactsPath
end
