# VirtualDesktopSwitcher for Windows 10

Are you like me and have been using the mouse scroll to switch between virtual desktops on GNU/Linux for years? Do you like to keep your hand in your pants while still being able to easily switch between virtual desktops? Then this program might be for you.

Finally virtual desktops have come to Windows! Hallelujah! But, virtual desktops in Windows 10 are somewhat lacking in functionality compared to the well established virtual desktops in the GNU/Linux world. Sooner or later Microsoft will probably introduce more functionality but in the meantime this project attempts to amend some of what is lacking.

Also check out https://www.facebook.com/Win10VirtualDesktopSwitcher which might be more interesting for you.

## Download
Version 2, use at your own risk: [vds-v2.zip](https://github.com/fishie/VirtualDesktopSwitcher/releases/download/v2/vds-v2.zip)

## How to use:
1. Download zip-file.
2. Extract zip file somewhere of your choice (for example "My Documents\vds\").
3. Double click the exe-file.
4. A window should show up, if not, the program probably crashed for some reason. You should also have a new tray icon. Right -clicking on the tray icon will show/hide the main window.
5. Check the "Desktop scroll" checkbox if you want to be able to hover over your desktop background and scroll with your mouse to switch desktops. This has only been tested on US English version and could fail on other languages depending on if the desktop window title is still "FolderView" ([this code](VirtualDesktopSwitcher/Code/VirtualDesktopSwitcherForm.cs#L430)) in other versions 
6. Check the "Task View button scroll" checkbox if you want to be able to switch virtual desktops by scrolling while hovering over the Task View button.
7. Check the "Load on Windows startup" checkbox to automatically run this program when Windows starts.
8. Check the "Start hidden" checkbox to start the program minimised to the tray. Useful when using the "Load on Windows startup" option.

## Copyright
```
Copyright (C) 2015 Rishie Sharma

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
```
