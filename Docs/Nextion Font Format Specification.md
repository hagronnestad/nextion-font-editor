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
| ASCII                 | 0x0100                | 95                    |
| GB2312                | 0x0200                | 8273                  |
| ISO-8859-1            | 0x0300                | 224                   |
| ISO-8859-2            | 0x0400                | 224                   |
| ISO-8859-3            | 0x0500                | 224                   |
| ISO-8859-4            | 0x0600                | 224                   |
| ISO-8859-5            | 0x0700                | 224                   |
| ISO-8859-6            | 0x0800                | 224                   |
| ISO-8859-7            | 0x0900                | 224                   |
| BIG5                  | 0x1001                | 14225                 |
| ISO-8859-8            | 0x0A00                | 224                   |
| ISO-8859-9            | 0x0B00                | 224                   |
| ISO-8859-13           | 0x0C00                | 224                   |
| ISO-8859-15           | 0x0D00                | 224                   |
| ISO-8859-11           | 0x0E00                | 224                   |
| KS_C_5601-1987        | 0x0F00                | 3855                  |
| WINDOWS-1255          | 0x1100                | 224                   |
| WINDOWS-1256          | 0x1200                | 224                   |
| WINDOWS-1257          | 0x1300                | 224                   |
| WINDOWS-1258          | 0x1400                | 224                   |
| WINDOWS-874           | 0x1500                | 224                   |
| KOI8-R                | 0x1600                | 224                   |


## File format structure

### Fixed header

**Length:** 0x1C (28)

```
0x00000000: 04 FF 00 0A 01 00 14 28 00 00 20 7E 5F 00 00 00
0x00000010: 03 0E 0E 00 2A 25 00 00 00 00 00 00
```

| Offset     | Length | Data                                             | Type   | Value               | Description                                                                           |
|------------|-------:|--------------------------------------------------|--------|--------------------:|---------------------------------------------------------------------------------------|
| 0x00000000 | 4      | `04 FF 00 0A` *                                  | byte[] |                     | File signature / magic numbers                                                        |
| 0x00000004 | 2      | `03 00`                                          | uint16 | 3                   | Code page / character encoding (possible values listed in the reference table)        |
| 0x00000006 | 1      | `14`                                             | byte   | 20                  | Character width                                                                       |
| 0x00000007 | 1      | `28`                                             | byte   | 40                  | Character height                                                                      |
| 0x00000008 | 1      | `00`                                             | byte   | 0                   | Code page multibyte - first byte start                                                |
| 0x00000009 | 1      | `00`                                             | byte   | 0                   | Code page multibyte - first byte end                                                  |
| 0x0000000A | 1      | `20`                                             | byte   | 32, ' ' (ASCII)     | Code page start / multibyte second byte start                                         |
| 0x0000000B | 1      | `7E`                                             | byte   | 126, '~' (ASCII)    | Code page end / multibyte second byte end                                             |
| 0x0000000C | 4      | `5F 00 00 00`                                    | uint32 | 95                  | Number of characters in file                                                          |
| 0x00000010 | 1      | `03`                                             | byte   | 3                   | File Format Version?                                                                  |
| 0x00000011 | 1      | `0E`                                             | byte   | 14                  | Length of font name                                                                   |
| 0x00000012 | 1      | `0E`                                             | byte   | 14                  | Also length of font name? Always the same value as 0x11                               |
| 0x00000013 | 1      | `00`                                             | byte   | 0                   | Reserved                                                                              |
| 0x00000014 | 4      | `2A 25 00 00`                                    | uint32 | 9514                | Total length of font name and character data                                          |
| 0x00000018 | 4      | `00 00 00 00`                                    | uint32 | 0                   | Reserved                                                                              |

> **\* The file signature/magic bytes for a .zi file containing the `BIG5` code page is different from all other files. For `BIG5` the magic bytes are `04 7E 22 0A`. Which means that the second and third byte might be variable and have some meaning beyond being magic numbers.**

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

❗️ There's our exclamation mark ❗️
