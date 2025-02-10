using System;
using System.Numerics;
using System.Runtime.InteropServices;
using SDL2;

namespace ImGuiNET
{
    public static unsafe partial class ImGuiNative
    {
        [DllImport("cimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern bool igImGui_ImplSDL2_InitForSDLRenderer(nint window, nint renderer);
        [DllImport("cimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern bool ImGui_ImplSDL2_InitForSDLRenderer(nint window, nint renderer);
        [DllImport("cimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern bool ImGui_ImplSDLRenderer2_Init(nint renderer);
        [DllImport("cimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern bool ImGui_ImplSDL2_ProcessEvent(ref SDL.SDL_Event e);
        [DllImport("cimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern void ImGui_ImplSDLRenderer2_NewFrame();
        [DllImport("cimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern void ImGui_ImplSDL2_NewFrame();
        [DllImport("cimgui_sdl", CallingConvention = CallingConvention.Cdecl)] public static extern void ImGui_ImplSDLRenderer2_RenderDrawData(ImDrawDataPtr imDrawDataPtr, nint renderer);

    }
}
