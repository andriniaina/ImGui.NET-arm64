https://askubuntu.com/questions/430705/how-to-use-apt-get-to-download-multi-arch-library

sudo apt-get install libsdl2-2.0-0:arm64
sudo apt-get install libsdl2-dev:arm64
sudo apt-get install libsdl2-ttf-dev:arm64
sudo apt-get install libsdl2-image-dev:arm64

in libtool, change: CC="aarch64-unknown-linux-gnu-gcc"

in cimgui/backend/opengl, REMOVE everything related to opengl




	aarch64-unknown-linux-gnu-cc ./src/*.c -o./sdlterm -Wall -pedantic -Ilibvterm-0.3.3/include -Llibvterm-0.3.3/.libs -lSDL2 -lSDL2_ttf -lSDL2_image -l:libvterm.a -O3 -flto -I/usr/include -L/usr/lib/aarch64-linux-gnu
	gives
	ERROR /usr/xcc/aarch64-unknown-linux-gnu/lib/gcc/aarch64-unknown-linux-gnu/12.3.0/../../../../aarch64-unknown-linux-gnu/bin/ld.bfd: /usr/lib/aarch64-linux-gnu/libSDL2_image.so: undefined reference to `png_set_strip_16@PNG16_0'


https://github.com/Tyyppi77/imgui_sdl/blob/master/example.cpp
