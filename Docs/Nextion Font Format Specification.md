# Nextion Font Format Specification
This is a reverse engineered specification of the Nextion font format. Use at own risk.

## General information

The Nextion Font Format is a proprietary font format used by the Nextion Editor HMI software. Nextion Editor has a built in "Font Generator"-tool which converts standard fonts into .zi files that is compatible with the Nextion HMI displays.

| Information           |                       |
|-----------------------|-----------------------|
| **Software**          | Nextion Editor V0.53  |
| **File extension**    | .zi                   |
| **Example file**      | Arial_40_ascii.zi     |

## Code pages / character encoding reference

| Code page / encoding  | Value                 | Number of characters  |
|-----------------------|-----------------------|-----------------------|
| ASCII                 | 0x01                  | 95                    |
| ISO-8859-1            | 0x03                  | 224                   |




## File format structure

### Fixed header

**Length:** 0x1C (28)

```
0x00000000: 04 FF 00 0A 01 00 14 28 00 00 20 7E 5F 00 00 00
0x00000010: 03 0E 0E 00 2A 25 00 00 00 00 00 00
```

| Offset     | Length | Data                                             | Type   | Value               | Description                                                                           |
|------------|-------:|--------------------------------------------------|--------|--------------------:|---------------------------------------------------------------------------------------|
| 0x00000000 | 4      | `04 FF 00 0A`                                    |        |                     | File signature / magic numbers                                                        |
| 0x00000000 | 2      | `03 00`                                          | uint16 | 3                   | Code page / character encoding (possible values listed in the reference table)        |
| 0x00000006 | 1      | `14`                                             | byte   | 20                  | Character width                                                                       |
| 0x00000007 | 1      | `28`                                             | byte   | 40                  | Character height                                                                      |
| 0x0000000C | 4      | `5F 00 00 00`                                    | uint32 | 95                  | Number of characters in file                                                          |
| 0x00000011 | 1      | `0E`                                             | byte   | 14                  | Length of font name                                                                   |
| 0x00000012 | 1      | `0E`                                             | byte   |                     | Also length of font name? Seems to always be the same as 0x11                         |
| 0x00000014 | 4      | `2A 25 00 00`                                    | uint32 | 9514                | Total length of font name and character data                                          |
| 0x00000018 | 4      | `00 00 00 00`                                    |        |                     | Unknown, font name comes after these                                                  |

### Font name
Variable length font name. In this case the font name is `0x0E (14)` bytes long as seen in offset `0x00000011`.

```
0x00000010: 03 0E 0E 00 2A 25 00 00 00 00 00 00 41 72 69 61
0x00000020: 6C 5F 34 30 5F 61 73 63 69 69 00 00 00 00 00 00
```

| Offset     | Length | Data                                             | Type   | Value               | Description                                                      |
|------------|-------:|--------------------------------------------------|--------|--------------------:|------------------------------------------------------------------|
| 0x0000001C | 14     | `41 72 69 61 6C 5F 34 30 5F 61 73 63 69 69`      | string | Arial_40_ascii      | Font name                                                        |

### Character data

The rest of the file contains the actual character data. The length of each character is calculated like this: `character width * character height / 8`. In our example we get; `20 * 40 / 8` which gives us `100` bytes per character. Characters are monochrome, which means that each bit in the `100` bytes gives us one pixel. `20 * 40 = 800 pixels`.

Example data for one character. This is the exclamation mark character (`!`), which is the second character in the file. I skipped the first one, beacuse that is the space character which is blank.

```
0x00000080:                                           00 00
0x00000090: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
0x000000a0: 00 00 00 00 01 E0 00 1E 00 01 E0 00 1E 00 01 E0
0x000000b0: 00 1E 00 00 E0 00 0E 00 00 E0 00 0E 00 00 E0 00
0x000000c0: 0E 00 00 E0 00 0E 00 00 E0 00 0E 00 00 00 00 00
0x000000d0: 00 01 E0 00 1E 00 01 E0 00 00 00 00 00 00 00 00
0x000000e0: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
0x000000f0: 00 00
```

The rest of the file contains the remaining characters supported by the font.

### Character data example for a 16 pixels tall exclamation mark (`!`) character

This example shows how to read the pixel data for a character. The height of character is `16` pixels, which means it is `8` pixels wide. This gives us a total of `128` pixels. This also means that each of the `16` bytes contains the data for one line of pixels.

```
0x00000030:                               00 00 00 00 60 60
0x00000040: 60 60 60 60 00 60 00 00 00 00
```

Let's look at the bits of each byte:

```
00: 0 0 0 0 0 0 0 0
00: 0 0 0 0 0 0 0 0
00: 0 0 0 0 0 0 0 0
00: 0 0 0 0 0 0 0 0
60: 0 1 1 0 0 0 0 0
60: 0 1 1 0 0 0 0 0
60: 0 1 1 0 0 0 0 0
60: 0 1 1 0 0 0 0 0
60: 0 1 1 0 0 0 0 0
60: 0 1 1 0 0 0 0 0
00: 0 0 0 0 0 0 0 0
60: 0 1 1 0 0 0 0 0
00: 0 0 0 0 0 0 0 0
00: 0 0 0 0 0 0 0 0
00: 0 0 0 0 0 0 0 0
00: 0 0 0 0 0 0 0 0
```

Now let's remove the zeroes (blank pixel) and replace the ones with an X:

```
00: 
00: 
00: 
00: 
60:   X X          
60:   X X          
60:   X X          
60:   X X          
60:   X X          
60:   X X          
00: 
60:   X X          
00: 
00: 
00: 
00: 
```

There's our exclamation mark! 😎
