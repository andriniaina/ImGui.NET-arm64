using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using ImPlotNET;
using System.Runtime.CompilerServices;
using TestDotNetStandardLib;

using SDL2;
using static ImGuiNET.ImGuiNative;
using System.Security.Principal;

namespace ImGuiNET
{
    class Program
    {
        // private static MemoryEditor _memoryEditor;

        // UI state
        private static float _f = 0.0f;
        private static int _counter = 0;
        private static int _dragInt = 0;
        private static Vector3 _clearColor = new Vector3(0.45f, 0.55f, 0.6f);
        private static bool _showImGuiDemoWindow = true;
        private static bool _showAnotherWindow = false;
        private static bool _showMemoryEditor = false;
        private static byte[] _memoryEditorData;
        private static uint s_tab_bar_flags = (uint)ImGuiTabBarFlags.Reorderable;
        static bool[] s_opened = { true, true, true, true }; // Persistent user state

        static void SetThing(out float i, float val) { i = val; }

        static void Main(string[] args)
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine("KO SDL could not be initialized!"); return;
            }
            Console.WriteLine("Very good");
            var window = SDL.SDL_CreateWindow("Basic C SDL project",
                                          SDL.SDL_WINDOWPOS_UNDEFINED,
                                          SDL.SDL_WINDOWPOS_UNDEFINED,
                                          1280, 720,
                                          SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (window == 0)
            {
                Console.WriteLine(" Window could not be created!\n SDL_Error: %s\n", SDL.SDL_GetError());
            }

            // Create renderer
            var renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (renderer == 0)
                Console.WriteLine("Renderer could not be created!\n SDL_Error: %s\n", SDL.SDL_GetError());
            try
            {
                // https://github.com/ocornut/imgui/blob/master/examples/example_sdl2_sdlrenderer2/main.cpp

                const int SCREEN_WIDTH = 1280;
                const int SCREEN_HEIGHT = 720;

                // Declare rect of square
                var w = Math.Min(SCREEN_WIDTH, SCREEN_HEIGHT) / 2;
                var h = Math.Min(SCREEN_WIDTH, SCREEN_HEIGHT) / 2;
                var squareRect = new SDL.SDL_Rect()
                {
                    w = w,
                    h = h,
                    x = SCREEN_WIDTH / 2 - w / 2,
                    y = SCREEN_HEIGHT / 2 - h / 2
                };
                ImGui.CreateContext();
                ImGui.GetCurrentContext();
                var io = ImGui.GetIO();
                io.ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard | ImGuiConfigFlags.NavEnableGamepad;
                io.Fonts.Flags |= ImFontAtlasFlags.NoBakedLines;


                ImGui_ImplSDL2_InitForSDLRenderer(window, renderer);
                ImGui_ImplSDLRenderer2_Init(renderer);

                var _scaleFactor = Vector2.One;
                io.DisplaySize = new Vector2(
                    SCREEN_WIDTH / _scaleFactor.X,
                    SCREEN_HEIGHT / _scaleFactor.Y);
                io.DisplayFramebufferScale = _scaleFactor;
                io.DeltaTime = 1f / 60f; // DeltaTime is in seconds.
                var show_demo_window = true;
                var done = false;
                // Event loop
                while (!done)
                {
                    // Wait indefinitely for the next available event
                    int has_events;
                    do
                    {
                        // TODO: wait event???
                        has_events = SDL.SDL_PollEvent(out var e);
                        if (has_events != 0)
                        {
                            ImGui_ImplSDL2_ProcessEvent(ref e);
                            if (e.type == SDL.SDL_EventType.SDL_QUIT)
                                return;// TODO done = true;
                            if (e.type == SDL.SDL_EventType.SDL_WINDOWEVENT && e.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE && e.window.windowID == SDL.SDL_GetWindowID(window))
                                return;// TODO done = true;
                        }
                    } while (has_events != 0);


                    ImGui_ImplSDLRenderer2_NewFrame();
                    ImGui_ImplSDL2_NewFrame();
                    ImGui.NewFrame();
                    ImGui.ShowDemoWindow(ref show_demo_window);

                    ImGui.Render();
                    SDL.SDL_RenderSetScale(renderer, io.DisplayFramebufferScale.X, io.DisplayFramebufferScale.Y);
                    //SDL. SDL_SetRenderDrawColor(renderer, (Uint8)(clear_color.x * 255), (Uint8)(clear_color.y * 255), (Uint8)(clear_color.z * 255), (Uint8)(clear_color.w * 255));
                    SDL.SDL_SetRenderDrawColor(renderer, 0xFF, 0xFF, 0x00, 0xFF);
                    SDL.SDL_RenderClear(renderer);
                    ImGui_ImplSDLRenderer2_RenderDrawData(ImGui.GetDrawData(), renderer);
                    SDL.SDL_RenderPresent(renderer);
                }
            }
            finally
            {
                Console.WriteLine("Destroying");
                // Destroy renderer
                if (renderer != 0)
                    SDL.SDL_DestroyRenderer(renderer);

                // Destroy window
                if (window != 0)
                    SDL.SDL_DestroyWindow(window);
                SDL.SDL_Quit();
                Console.WriteLine("Exit success");
            }
        }

        private static unsafe void SubmitUI()
        {
            // Demo code adapted from the official Dear ImGui demo program:
            // https://github.com/ocornut/imgui/blob/master/examples/example_win32_directx11/main.cpp#L172

            // 1. Show a simple window.
            // Tip: if we don't call ImGui.Begin(string) / ImGui.End() the widgets automatically appears in a window called "Debug".
            {
                ImGui.Text("");
                ImGui.Text(string.Empty);
                ImGui.Text("Hello, world!");                                        // Display some text (you can use a format string too)
                ImGui.SliderFloat("float", ref _f, 0, 1, _f.ToString("0.000"));  // Edit 1 float using a slider from 0.0f to 1.0f    
                                                                                 //ImGui.ColorEdit3("clear color", ref _clearColor);                   // Edit 3 floats representing a color

                ImGui.Text($"Mouse position: {ImGui.GetMousePos()}");

                ImGui.Checkbox("ImGui Demo Window", ref _showImGuiDemoWindow);                 // Edit bools storing our windows open/close state
                ImGui.Checkbox("Another Window", ref _showAnotherWindow);
                ImGui.Checkbox("Memory Editor", ref _showMemoryEditor);
                if (ImGui.Button("Button"))                                         // Buttons return true when clicked (NB: most widgets return true when edited/activated)
                    _counter++;
                ImGui.SameLine(0, -1);
                ImGui.Text($"counter = {_counter}");

                ImGui.DragInt("Draggable Int", ref _dragInt);

                float framerate = ImGui.GetIO().Framerate;
                ImGui.Text($"Application average {1000.0f / framerate:0.##} ms/frame ({framerate:0.#} FPS)");
            }

            // 2. Show another simple window. In most cases you will use an explicit Begin/End pair to name your windows.
            if (_showAnotherWindow)
            {
                ImGui.Begin("Another Window", ref _showAnotherWindow);
                ImGui.Text("Hello from another window!");
                if (ImGui.Button("Close Me"))
                    _showAnotherWindow = false;
                ImGui.End();
            }

            // 3. Show the ImGui demo window. Most of the sample code is in ImGui.ShowDemoWindow(). Read its code to learn more about Dear ImGui!
            if (_showImGuiDemoWindow)
            {
                // Normally user code doesn't need/want to call this because positions are saved in .ini file anyway.
                // Here we just want to make the demo initial state a bit more friendly!
                ImGui.SetNextWindowPos(new Vector2(650, 20), ImGuiCond.FirstUseEver);
                ImGui.ShowDemoWindow(ref _showImGuiDemoWindow);
            }

            if (ImGui.TreeNode("Tabs"))
            {
                if (ImGui.TreeNode("Basic"))
                {
                    ImGuiTabBarFlags tab_bar_flags = ImGuiTabBarFlags.None;
                    if (ImGui.BeginTabBar("MyTabBar", tab_bar_flags))
                    {
                        if (ImGui.BeginTabItem("Avocado"))
                        {
                            ImGui.Text("This is the Avocado tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        if (ImGui.BeginTabItem("Broccoli"))
                        {
                            ImGui.Text("This is the Broccoli tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        if (ImGui.BeginTabItem("Cucumber"))
                        {
                            ImGui.Text("This is the Cucumber tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        ImGui.EndTabBar();
                    }
                    ImGui.Separator();
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Advanced & Close Button"))
                {
                    // Expose a couple of the available flags. In most cases you may just call BeginTabBar() with no flags (0).
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_Reorderable", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.Reorderable);
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_AutoSelectNewTabs", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.AutoSelectNewTabs);
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_NoCloseWithMiddleMouseButton", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.NoCloseWithMiddleMouseButton);
                    if ((s_tab_bar_flags & (uint)ImGuiTabBarFlags.FittingPolicyMask) == 0)
                        s_tab_bar_flags |= (uint)ImGuiTabBarFlags.FittingPolicyDefault;
                    if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyResizeDown", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.FittingPolicyResizeDown))
                        s_tab_bar_flags &= ~((uint)ImGuiTabBarFlags.FittingPolicyMask ^ (uint)ImGuiTabBarFlags.FittingPolicyResizeDown);
                    if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyScroll", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.FittingPolicyScroll))
                        s_tab_bar_flags &= ~((uint)ImGuiTabBarFlags.FittingPolicyMask ^ (uint)ImGuiTabBarFlags.FittingPolicyScroll);

                    // Tab Bar
                    string[] names = { "Artichoke", "Beetroot", "Celery", "Daikon" };

                    for (int n = 0; n < s_opened.Length; n++)
                    {
                        if (n > 0) { ImGui.SameLine(); }
                        ImGui.Checkbox(names[n], ref s_opened[n]);
                    }

                    // Passing a bool* to BeginTabItem() is similar to passing one to Begin(): the underlying bool will be set to false when the tab is closed.
                    if (ImGui.BeginTabBar("MyTabBar", (ImGuiTabBarFlags)s_tab_bar_flags))
                    {
                        for (int n = 0; n < s_opened.Length; n++)
                            if (s_opened[n] && ImGui.BeginTabItem(names[n], ref s_opened[n]))
                            {
                                ImGui.Text($"This is the {names[n]} tab!");
                                if ((n & 1) != 0)
                                    ImGui.Text("I am an odd tab.");
                                ImGui.EndTabItem();
                            }
                        ImGui.EndTabBar();
                    }
                    ImGui.Separator();
                    ImGui.TreePop();
                }
                ImGui.TreePop();
            }

            ImGuiIOPtr io = ImGui.GetIO();
            SetThing(out io.DeltaTime, 2f);

            if (_showMemoryEditor)
            {
                ImGui.Text("Memory editor currently supported.");
                // _memoryEditor.Draw("Memory Editor", _memoryEditorData, _memoryEditorData.Length);
            }

            // ReadOnlySpan<char> and .NET Standard 2.0 tests
            TestStringParameterOnDotNetStandard.Text(); // String overloads should always be available.

            // On .NET Standard 2.1 or greater, you can use ReadOnlySpan<char> instead of string to prevent allocations.
            long allocBytesStringStart = GC.GetAllocatedBytesForCurrentThread();
            ImGui.Text($"Hello, world {Random.Shared.Next(100)}!");
            long allocBytesStringEnd = GC.GetAllocatedBytesForCurrentThread() - allocBytesStringStart;
            Console.WriteLine("GC (string): " + allocBytesStringEnd);

            long allocBytesSpanStart = GC.GetAllocatedBytesForCurrentThread();
            ImGui.Text($"Hello, world {Random.Shared.Next(100)}!".AsSpan()); // Note that this call will STILL allocate memory due to string interpolation, but you can prevent that from happening by using an InterpolatedStringHandler.
            long allocBytesSpanEnd = GC.GetAllocatedBytesForCurrentThread() - allocBytesSpanStart;
            Console.WriteLine("GC (span): " + allocBytesSpanEnd);

            ImGui.Text("Empty span:");
            ImGui.SameLine();
            ImGui.GetWindowDrawList().AddText(ImGui.GetCursorScreenPos(), uint.MaxValue, ReadOnlySpan<char>.Empty);
            ImGui.NewLine();
            ImGui.GetWindowDrawList().AddText(ImGui.GetCursorScreenPos(), uint.MaxValue, $"{ImGui.CalcTextSize("h")}");
            ImGui.NewLine();
            ImGui.TextUnformatted("TextUnformatted now passes end ptr but isn't cut off");
        }
    }
}
