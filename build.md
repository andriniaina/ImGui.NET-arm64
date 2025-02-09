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

USER=root  LANGUAGE= SHLVL=2 HOME=/userdata/system OLDPWD=/userdata/system SSH_TTY=/dev/pts/0 DBUS_SESSION_BUS_ADDRESS=unix:path=/tmp/dbus-7wmMixh8R5,guid=6065a4ef6e987fdcc7afafb767a8bc7c LOGNAME=root SDL_NOMOUSE=1 _=/usr/bin/emulationstation TERM=xterm-256color PATH=/bin:/sbin:/usr/bin:/usr/sbin XDG_RUNTIME_DIR=/var/run LANG=en_US.UTF-8 SHELL=/bin/bash PWD=/userdata LC_ALL=en_US.UTF-8 SSH_CONNECTION=192.168.0.21  EDITOR=/bin/vi SDL_RENDER_VSYNC=1 SDL_GAMECONTROLLERCONFIG="030000005e0400008e02000014010000,TRIMUI Player1,platform:Linux,b:b1,a:b0,dpdown:h0.4,leftx:a0,lefty:a1,rightx:a3,righty:a4,lefttrigger:a2,dpleft:h0.8,rightshoulder:b5,leftshoulder:b4,righttrigger:a5,dpright:h0.2,back:b6,start:b7,dpup:h0.1,y:b3,x:b2,guide:b8," nice -n -4 /bin/bash /userdata/roms/tools/imgui.sh

