﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
namespace consoleInfo
{
    public class Menu
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name;
        public Func<bool> Func;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
    public class ConsoleInfo
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern short GetAsyncKeyState(int vKey);
        public static bool GetKey(ConsoleKey key)
        {
            short s = GetAsyncKeyState((int)key);
            return (s & 0x8000) > 0;
        }

        public string ExitSubString = "./";

        public ConsoleKey KeyUp = ConsoleKey.UpArrow;
        public ConsoleKey KeyDown = ConsoleKey.DownArrow;
        bool Up = false, Down = false, Enter = false;
        int CursorIndex = 0;
        int SubIndex = 0;
        readonly List<Menu> Menus = new();
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        List<List<Menu>> SubMenus;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        int EnterSubFrom = 0;
        bool InSub = false;
        public void StartMeun()
        {
            SubMenus = new List<List<Menu>>(5)
            {
                new List<Menu>(),
                new List<Menu>(),
                new List<Menu>(),
                new List<Menu>(),
                new List<Menu>()
            };

            SubMenus[0] = new();
            SubMenus[1] = new();
            SubMenus[2] = new();
            SubMenus[3] = new();
            SubMenus[4] = new();
            AddSub(ExitSubString, ExitSub, 0);
            AddSub(ExitSubString, ExitSub, 1);
            AddSub(ExitSubString, ExitSub, 2);
            AddSub(ExitSubString, ExitSub, 3);
            AddSub(ExitSubString, ExitSub, 4);
        }

        public void Update()
        {
            List<Menu> list = Menus;
            if (InSub == true)
                list = SubMenus[SubIndex];

            Console.CursorVisible = false;
            Console.SetCursorPosition(Console.WindowWidth / 2 - list[CursorIndex].Name.Length - 1, CursorIndex);
            Console.Write("@");
            Console.SetCursorPosition(Console.WindowWidth / 2, CursorIndex);
            Console.Write("@");

            Print(list);
            Input();
            if (Up == true)
            {
                Console.Clear();
                if (0 != CursorIndex)
                {
                    CursorIndex--;
                }
            }
            else if (Down == true)
            {
                Console.Clear();
                if (list.Count - 1 != CursorIndex)
                {
                    CursorIndex++;
                }
            }
            else if (Enter == true)
            {
                Console.Clear();
                list[CursorIndex].Func();
            }
            Thread.Sleep(100);
        }

        public bool ExitSub()
        {
            CursorIndex = EnterSubFrom;
            InSub = false;
            return false;
        }

        public void GetInSub(int subindex)
        {
            SubIndex = subindex;
            EnterSubFrom = CursorIndex;
            CursorIndex = 0;
            InSub = true;
        }
        public static void Print(List<Menu> menus)
        {
            for (int i = 0; i < menus.Count; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - menus[i].Name.Length, i);
                Console.WriteLine(menus[i].Name);
            }
        }

        void Input()
        {
            Up = GetKey(KeyUp);
            Down = GetKey(KeyDown);
            Enter = GetKey(ConsoleKey.Enter);
        }
        public void Add(string name, Func<bool> func)
        {
            Menu menu = new()
            {
                Name = name,
                Func = func
            };
            Menus.Add(menu);
        }
        public void AddSub(string name, Func<bool> func, int SubIndex)
        {
            Menu menu = new()
            {
                Name = name,
                Func = func
            };
            SubMenus[SubIndex].Add(menu);
        }
    }
}