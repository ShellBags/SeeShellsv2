﻿#region copyright
// SeeShells Copyright (c) 2019-2020 Aleksandar Stoyanov, Bridget Woodye, Klayton Killough, 
// Richard Leinecker, Sara Frackiewicz, Yara As-Saidi
// SeeShells is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// SeeShells is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with this program;
// if not, see <https://www.gnu.org/licenses>
#endregion
namespace SeeShellsV2.Data
{
    public class ShellItem0x21 : ShellItem0x20, IShellItem
    {
        public ShellItem0x21() { }

        public ShellItem0x21(byte[] buf) : base(buf)
        {
            fields["Name"] = Block.unpack_string(buf, 0x03);
            fields["TypeName"] = "Volume - Named";
        }
    }
}