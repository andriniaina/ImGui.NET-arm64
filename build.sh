# build
mv deps/cimgui/linux-x64 deps/cimgui/linux-x64.real
mv deps/cimgui/linux-arm64 deps/cimgui/linux-x64
docker build --pull -t cross-build-arm64 -f Dockerfile.cross-build-x64-arm64 .
docker run --rm -v $(pwd)/src:/source -v $(pwd)/deps:/deps -w /source cross-build-arm64 dotnet publish -a arm64 -c Release -o bin-arm64 -p:SysRoot=/crossrootfs/arm64 -p:LinkerFlavor=lld

sudo rm src/bin-arm64/CodeGenerator*
sudo rm src/bin-arm64/ImGuiNET.SampleProgram.XNA*
rsync -rv src/bin-arm64 root@192.168.0.10:~/ImGui.NET
