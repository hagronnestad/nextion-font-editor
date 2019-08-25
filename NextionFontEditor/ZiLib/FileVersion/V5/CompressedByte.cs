using System.Drawing;
using System;
using System.Collections.Generic;

namespace ZiLib.FileVersion.V5
{
    internal class CompressedByte
    {
        /* Helper class that encodes multiple pixels into a single compressed byte */
        /* For the 6 drawing modes, please refer to the v5 spec */

        //  YZ = 00 1xxxxx : Repeat opaque pixel xxxxx times
        public static byte RepeatedBlacks(uint times)
        {
            var drawmode = (byte)0;
            times = (times & 0x1F);   // max 31 repetitions  
            return (byte)((drawmode << 6) | (1 << 5) | (byte)times);
        }

        // YZ = 00 0xxxxx : Repeat transparent pixel xxxxx times
        public static byte RepeatedWhites(uint times)
        {
            var drawmode = (byte)0;
            times = (times & 0x1F);   // max 31 repetitions  
            return (byte)((drawmode << 6) | (0 << 5) | (byte)times);
        }

        //  YZ = 01 0xxxxx : Repeat transparent pixel xxxxx times, followed by 1 or 2 opaque pixels
        public static byte RepeatedWhites(uint times, uint additionalblackpixels)
        {
            var drawmode = (byte)1;
            times = (times & 0x1F);   // max 31 repetitions 

            switch (additionalblackpixels)
            {
                case 0:
                    return RepeatedWhites(times);
                case 1:
                    return (byte)((drawmode << 6) | (0 << 5) | (byte)times);
                case 2:
                    return (byte)((drawmode << 6) | (1 << 5) | (byte)times);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid number of additional blacks", additionalblackpixels), "blacks");
            } // switch
        }

        //  YZ = 10 xxxccc : Repeat transparent pixel xxx times, followed by an extra colored pixel
        public static byte RepeatedWhites(uint times, byte additionalcolor)
        {
            var drawmode = (byte)2;
            times = (times & 0b111); // max 7 repetitions  
            additionalcolor = (byte)(additionalcolor & 0b111);
            return (byte)((drawmode << 6) | ((byte)times << 3) | additionalcolor);
        }

        //  YZ = 11 cccddd : 2 different alpha pixels, each 3 bits
        public static byte AlphaColors(byte color1, byte color2)
        {
            var drawmode = (byte)3;
            color1 = (byte)(color1 & 0b111);
            color2 = (byte)(color2 & 0b111);
            return (byte)((drawmode << 6) | (color1 << 3) | color2);
        }

        // convert 3bpp back to ARGB Color
        public static Color ToColor(byte c)
        {
            switch (c)
            {
                case 0:
                    return Color.White;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    var rgb = 250 - c * 36;
                    return Color.FromArgb(255, rgb, rgb, rgb);
                case 7:
                    return Color.Black;
                default:
                    return Color.Red;
            }
        }

        public static Color[] InflateToPixels(byte b)
        {
            var size = b & 0b00011111;
            var colors = new List<Color>();

            switch (b >> 5)
            {
                case 0b000:
                    for (int i = 0; i < size; i++)
                    {
                        colors.Add(Color.White);
                    }
                    break;

                case 0b001:
                    for (int i = 0; i < size; i++)
                    {
                        colors.Add(Color.Black);
                    }
                    break;

                case 0b010:
                    for (int i = 0; i < size; i++)
                    {
                        colors.Add(Color.White);
                    }
                    colors.Add(Color.Black);
                    break;

                case 0b011:
                    for (int i = 0; i < size; i++)
                    {
                        colors.Add(Color.White);
                    }
                    colors.Add(Color.Black);
                    colors.Add(Color.Black);
                    break;

                case 0b100:
                case 0b101:
                    size = (b & 0b111000) >> 3;
                    var color = b & 0b111;
                    for (int i = 0; i < size; i++)
                    {
                        colors.Add(Color.White);
                    }
                    colors.Add(ToColor((byte)color));
                    break;

                case 0b110:
                case 0b111:
                    var color1 = (b & 0b111000) >> 3;
                    var color2 = b & 0b111;
                    colors.Add(ToColor((byte)color1));
                    colors.Add(ToColor((byte)color2));
                    break;

                default:
                    throw new Exception("Invalid drawing mode encounterd.");
            }

            return colors.ToArray();
        }

        // OBSOLETE !! slow - Counts how many times the pixel color at the start position is repeated, given a maximum count
        private static uint GetRepetition(byte[] bytes, uint start = 0, uint max = 255)
        {
            // Check array boudaries
            if (start > bytes.Length)
            {
                return 0;
            }

            var count = (uint)1;
            var i = start + 1;

            // Check array boudaries
            // Byte must be the same as start byte
            // Maximum repetitions
            while ((i < bytes.Length) && (bytes[i] == bytes[start]) && (count < max))
            {
                count++;
                i++;
            }

            return count;
        }

        // Determine if the byte array only contains Black and White pixels
        private static bool IsBlackAndWhiteOnly(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                if ((bytes[i] > 0) && (bytes[i] < 7))
                {
                    return false;
                }
            }

            return true;
        }

        /* OBSOLETE !! slow logic that scans the 3bpp input pixel array and compresses it to a more compact size using the v5/6 font spec */
        public static byte[] Compress3bpp(byte[] bytes)
        {
            var length = bytes.Length;
            var data = new List<byte>();

            // Possible Optimization: strip all trailing transparent bytes as they contain no visible data
            /*while(length>0)
            {
                if (bytes[length - 1] != 0)
                {
                    break;
                }
                length--;
            }*/

            // No data to compress
            if (length < 1)
            {
                return new byte[0];
            }

            // first byte always contains bitdepth
            data.Add(3); // bitdepth

            // scan data
            var pos = (uint)0;
            while (pos < length)
            {
                var color = bytes[pos];

                /* Color is a shade of grey */
                if ((color != 0) && (color != 7))
                {
                    pos++;  // advance pos
                    if (pos < length)
                    {
                        // add 2 colors if this is not the last byte
                        // there are more bytes, encode 2 colors: 11 fff sss
                        data.Add(AlphaColors(color, bytes[pos++]));  // and advance extra position  
                    }
                    else
                    {
                        // its the last byte, so encode only 1 colors: 10 000 ccc
                        data.Add(RepeatedWhites((uint)0, color));
                    }
                    continue; // to the next byte
                }

                /* Color is Black or White */

                // Find repetitions, max 5 bits = 0-31
                var times = GetRepetition(bytes, pos, 31);

                // !! already advance the position with the number of repetitions !!
                pos += times;

                // Repeated blacks
                if (color == 7)
                {
                    data.Add(RepeatedBlacks(times));
                    continue; // to the next byte
                }

                /* Color is White */

                // Repeated whites
                if (pos >= length)
                {
                    // its the last byte, so encode only the transparencies
                    data.Add(RepeatedWhites(times));
                    continue; // to the next byte
                }

                // There are more pixels left
                var nextcolor = bytes[pos];

                /*if ($nextcolor -eq 0) {
                    // the next byte is also transparent so encode only the current transparencies and advance the position
                    $arr[$count++] = New-HMIchardata -transparents $times
                    continue # to the next byte
                }*/

                // The next byte is black so encode the current transparencies with 1 or 2 blacks
                if (nextcolor == 7)
                {
                    var blacks = GetRepetition(bytes, pos, 2);  // maximum 2
                    switch (blacks)
                    {
                        case 1:
                            data.Add(RepeatedWhites(times, blacks));
                            break;
                        case 2:
                            data.Add(RepeatedWhites(times, blacks));
                            break;
                        default:
                            throw new ArgumentException(String.Format("{0} is not a valid number of blacks", blacks), "blacks");
                    }

                    pos += blacks;
                    continue; // to the next byte
                }

                // Next color is an alpha color
                if (times < 8)
                {
                    // Only encode an extra color if repetition is less then 8 times (3-bits)
                    data.Add(RepeatedWhites(times, nextcolor));
                    pos += 1 /* extra color */ ;

                }
                else
                {
                    // Transparent repetition is 8 or more times
                    data.Add(RepeatedWhites(times));
                }

            } // while

            return data.ToArray();
        }
    }
}