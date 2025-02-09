using System;
using System.Numerics;
using System.Runtime.InteropServices;
using SDL2;

namespace ImGuiNET
{
    public static unsafe partial class ImGuiNative
    {
        [DllImport("libcimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern bool igImGui_ImplSDL2_InitForSDLRenderer(nint window, nint renderer);
        [DllImport("libcimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern bool ImGui_ImplSDL2_InitForSDLRenderer(nint window, nint renderer);
        [DllImport("libcimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern bool ImGui_ImplSDLRenderer2_Init(nint renderer);
        [DllImport("libcimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern bool ImGui_ImplSDL2_ProcessEvent(ref SDL.SDL_Event e);
        [DllImport("libcimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern void ImGui_ImplSDLRenderer2_NewFrame();
        [DllImport("libcimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern void ImGui_ImplSDL2_NewFrame();
        [DllImport("libcimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern void ImGui_ImplSDLRenderer2_RenderDrawData(ImDrawDataPtr imDrawDataPtr, nint renderer);

    }
}
