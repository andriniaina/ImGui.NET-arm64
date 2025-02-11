# build
# dotnet build src/ImGui.NET -o release/x64 -c Debug -a x64
docker build --pull -t cross-build-arm64 -f Dockerfile.cross-build-x64-arm64 .
docker run --rm -v $(pwd)/src:/source -v $(pwd)/release:/release -v $(pwd)/deps:/deps -w /source cross-build-arm64 dotnet publish ImGui.NET.SampleProgram/ImGui.NET.SampleProgram.csproj -a arm64 -c Release -o ../release/arm64 -p:SysRoot=/crossrootfs/arm64 -p:LinkerFlavor=lld  && \
rsync -rv src/bin-arm64 root@$KNULLI:~/ImGui.NET
